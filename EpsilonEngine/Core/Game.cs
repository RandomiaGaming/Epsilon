namespace EpsilonEngine
{
    public class Game
    {
        #region Public Variables
        public bool Initialized { get; private set; }
        public bool MarkedForExit { get; private set; }
        public bool Exited { get; private set; }

        public bool KillProcessOnExit = true;
        public bool DestroyChildrenOnExit = false;

        public readonly bool OverridesUpdate;

        public readonly bool OverridesDraw;

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;

                _XNABackgroundColorCache = new Microsoft.Xna.Framework.Color(value.R, value.G, value.B, value.A);
            }
        }

        public int ViewportWidth { get; private set; } = 1920;
        public int ViewportHeight { get; private set; } = 1080;
        public double AspectRatio { get; private set; } = 1.77777777777778;
        public int CurrentFPS { get; private set; }

        public double TimeSinceStart { get; private set; }
        public long TicksSinceStart { get; private set; }
        public double DeltaTime { get; private set; }
        public long DeltaTicks { get; private set; }

        public double TargetFPS
        {
            get
            {
                return _targetFPS;
            }
            set
            {
                if (value <= 0)
                {
                    throw new System.Exception("TargetFPS must be greater than 0.");
                }
                if (value is double.NaN)
                {
                    throw new System.Exception("TargetFPS must be a real number or infinity.");
                }
                if (value is double.PositiveInfinity)
                {
                    GameInterface.IsFixedTimeStep = false;
                    _targetTPF = 0;
                    GameInterface.TargetElapsedTime = new System.TimeSpan(1);
                    _targetFPS = double.PositiveInfinity;
                    return;
                }
                if (value > 1000000.0)
                {
                    throw new System.Exception("TargetFPS must less than 1000000 unless TargetFPS is infinity.");
                }
                GameInterface.IsFixedTimeStep = true;
                _targetTPF = 10000000 / (long)value;
                GameInterface.TargetElapsedTime = new System.TimeSpan(_targetTPF);
                _targetFPS = value;
            }
        }
        public long TargetTPF
        {
            get
            {
                return _targetTPF;
            }
            set
            {
                if (value < 0)
                {
                    throw new System.Exception("TargetTPF must be greater than or equal to 0.");
                }
                if (value == 0)
                {
                    GameInterface.IsFixedTimeStep = false;
                    _targetTPF = 0;
                    GameInterface.TargetElapsedTime = new System.TimeSpan(1);
                    _targetFPS = double.PositiveInfinity;
                    return;
                }
                GameInterface.IsFixedTimeStep = true;
                _targetTPF = value;
                GameInterface.TargetElapsedTime = new System.TimeSpan(_targetTPF);
                _targetFPS = 10000000 / _targetTPF;
            }
        }

        public bool IsFullScreen
        {
            get
            {
                return _isFullScreen;
            }
            set
            {
                if (value == _isFullScreen)
                {
                    return;
                }
                _isFullScreen = value;
                GameInterface.XNAGraphicsDeviceManager.IsFullScreen = value;


                GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferWidth = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Width;
                GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferHeight = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Height;

                GameInterface.XNAGraphicsDeviceManager.ApplyChanges();
            }
        }
        #endregion
        #region Internal Variables
        internal GameInterface GameInterface = null;

        internal SingleRunPump CreationPump = new SingleRunPump();
        internal SingleRunPump InitializationPump = new SingleRunPump();

        internal OrderedPump PhysicsUpdatePump = new OrderedPump();
        internal OrderedPump UpdatePump = new OrderedPump();
        internal UnorderedPump RenderPump = new UnorderedPump();
        internal InverseOrderedPump DrawPump = new InverseOrderedPump();

        internal SingleRunPump OnDestroyPump = new SingleRunPump();
        internal SingleRunPump DestructionPump = new SingleRunPump();

        internal Microsoft.Xna.Framework.Graphics.SpriteBatch XNASpriteBatch = null;
        #endregion
        #region Private Variables
        private System.Diagnostics.Stopwatch _gameTimer = new System.Diagnostics.Stopwatch();
        private long _ticksSinceStartLastFrame;

        private System.Collections.Generic.List<GameManager> _gameManagers = new System.Collections.Generic.List<GameManager>();

        private System.Collections.Generic.List<Canvas> _canvases = new System.Collections.Generic.List<Canvas>();

        private System.Collections.Generic.List<Scene> _scenes = new System.Collections.Generic.List<Scene>();

        private Microsoft.Xna.Framework.Color _XNABackgroundColorCache;
        private Color _backgroundColor;

        private Microsoft.Xna.Framework.Rectangle _XNAViewportRect;

        private double _targetFPS = double.PositiveInfinity;
        private long _targetTPF = 0;

        private bool _isFullScreen;

        private static bool _gameCreatedAlready;
        private bool Rendering = false;
        #endregion
        #region Public Constructors
        public Game(int updatePriority, int drawPriority)
        {
            if (_gameCreatedAlready)
            {
                throw new System.Exception("cannot create another game on this process.");
            }
            _gameCreatedAlready = true;

            Profiler.InitializeStart();

            GameInterface = new GameInterface(this);

            System.Type thisType = GetType();

            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Game))
            {
                UpdatePump.RegisterPumpEventUnsafe(Update, updatePriority);
                OverridesUpdate = true;
            }

            System.Reflection.MethodInfo drawMethod = thisType.GetMethod("Draw", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (drawMethod.DeclaringType != typeof(Game))
            {
                DrawPump.RegisterPumpEventUnsafe(Draw, drawPriority);
                OverridesDraw = true;
            }

            InitializationPump.RegisterPumpEventUnsafe(Initialize);

            XNASpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GameInterface.XNAGraphicsDevice);

            ResizeCallback();
        }
        #endregion
        #region Public Methods
        public void Run()
        {
            _gameTimer.Restart();

            GameInterface.Run();
        }
        public void MarkForExit()
        {
            if (MarkedForExit)
            {
                throw new System.Exception("game has already been marked for destruction.");
            }

            if (Exited)
            {
                throw new System.Exception("game has already been destroyed.");
            }

            int gameManagerCount = _gameManagers.Count;
            for (int i = 0; i < gameManagerCount; i++)
            {
                _gameManagers[i].MarkForDestruction();
            }

            int canvasCount = _canvases.Count;
            for (int i = 0; i < canvasCount; i++)
            {
                //_canvases[i].MarkForDestruction();
            }

            int sceneCount = _scenes.Count;
            for (int i = 0; i < sceneCount; i++)
            {
                _scenes[i].MarkForDestruction();
            }

            OnDestroyPump.RegisterPumpEvent(OnDestroy);

            DestructionPump.RegisterPumpEvent(Destroy);

            MarkedForExit = true;
        }

        public GameManager GetGameManager(int index)
        {
            if (index < 0 || index >= _gameManagers.Count)
            {
                throw new System.Exception("index was out of range.");
            }

            return _gameManagers[index];
        }
        public GameManager GetGameManager(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new System.Exception("type must be equal to GameManager or be assignable from GameManager.");
            }

            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    return gameManager;
                }
            }

            return null;
        }
        public T GetGameManager<T>() where T : GameManager
        {
            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameManager;
                }
            }

            return null;
        }
        public System.Collections.Generic.List<GameManager> GetGameManagers()
        {
            return new System.Collections.Generic.List<GameManager>(_gameManagers);
        }
        public System.Collections.Generic.List<GameManager> GetGameManagers(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new System.Exception("type must be equal to GameManager or be assignable from GameManager.");
            }

            System.Collections.Generic.List<GameManager> output = new System.Collections.Generic.List<GameManager>();

            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameManager);
                }
            }

            return output;
        }
        public System.Collections.Generic.List<T> GetGameManagers<T>() where T : GameManager
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();

            foreach (GameManager gameManager in _gameManagers)
            {
                if (gameManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)gameManager);
                }
            }

            return output;
        }
        public int GetGameManagerCount()
        {
            return _gameManagers.Count;
        }

        public Canvas GetCanvas(int index)
        {
            if (index < 0 || index >= _canvases.Count)
            {
                throw new System.Exception("index was out of range.");
            }

            return _canvases[index];
        }
        public Canvas GetCanvas(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Canvas)))
            {
                throw new System.Exception("type must be equal to Canvas or be assignable from Canvas.");
            }

            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    return canvas;
                }
            }

            return null;
        }
        public T GetCanvas<T>() where T : Canvas
        {
            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)canvas;
                }
            }

            return null;
        }
        public System.Collections.Generic.List<Canvas> GetCanvases()
        {
            return new System.Collections.Generic.List<Canvas>(_canvases);
        }
        public System.Collections.Generic.List<Canvas> GetCanvases(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Canvas)))
            {
                throw new System.Exception("type must be equal to Canvas or be assignable from Canvas.");
            }

            System.Collections.Generic.List<Canvas> output = new System.Collections.Generic.List<Canvas>();

            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvas);
                }
            }

            return output;
        }
        public System.Collections.Generic.List<T> GetCanvases<T>() where T : Canvas
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();

            foreach (Canvas canvas in _canvases)
            {
                if (canvas.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)canvas);
                }
            }

            return output;
        }
        public int GetCanvasCount()
        {
            return _canvases.Count;
        }

        public Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Count)
            {
                throw new System.Exception("index was out of range.");
            }

            return _scenes[index];
        }
        public Scene GetScene(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Scene)))
            {
                throw new System.Exception("type must be equal to Scene or be assignable from Scene.");
            }

            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    return scene;
                }
            }

            return null;
        }
        public T GetScene<T>() where T : Scene
        {
            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)scene;
                }
            }

            return null;
        }
        public System.Collections.Generic.List<Scene> GetScenes()
        {
            return new System.Collections.Generic.List<Scene>(_scenes);
        }
        public System.Collections.Generic.List<Scene> GetScenes(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Scene)))
            {
                throw new System.Exception("type must be equal to Scene or be assignable from Scene.");
            }

            System.Collections.Generic.List<Scene> output = new System.Collections.Generic.List<Scene>();

            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    output.Add(scene);
                }
            }

            return output;
        }
        public System.Collections.Generic.List<T> GetScenes<T>() where T : Scene
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();

            foreach (Scene scene in _scenes)
            {
                if (scene.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)scene);
                }
            }

            return output;
        }
        public int GetSceneCount()
        {
            return _scenes.Count;
        }

        public void DrawTexture(Texture texture)
        {
            if (!Rendering)
            {
                throw new System.Exception("cannot draw texture because game is not drawing.");
            }

            if (texture is null)
            {
                throw new System.Exception("texture cannot be null.");
            }

            DrawTextureUnsafe(texture._xnaTexture);
        }
        #endregion
        #region Internal Methods
        private bool _profilerInitialized = false;
        internal void UpdateCallback()
        {
            Profiler.UpdateStart();

            TicksSinceStart = _gameTimer.ElapsedTicks;

            DeltaTicks = TicksSinceStart - _ticksSinceStartLastFrame;

            TimeSinceStart = TicksSinceStart / 10000000.0;

            DeltaTime = DeltaTicks / 10000000.0;

            CurrentFPS = (int)(10000000 / DeltaTicks);

            _ticksSinceStartLastFrame = TicksSinceStart;

            CreationPump.Invoke();

            InitializationPump.Invoke();

            PhysicsUpdatePump.Invoke();

            UpdatePump.Invoke();

            Profiler.UpdateEnd();

            Profiler.RenderStart();

            RenderPump.Invoke();

            Rendering = true;

            GameInterface.XNAGraphicsDevice.Clear(_XNABackgroundColorCache);

            XNASpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            DrawPump.Invoke();

            XNASpriteBatch.End();

            Rendering = false;

            OnDestroyPump.Invoke();

            DestructionPump.Invoke();

            Profiler.RenderEnd();

            if (!_profilerInitialized)
            {
                Profiler.InitializeEnd();
                _profilerInitialized = true;
            }
            else
            {
                Profiler.Print(CurrentFPS);
            }
        }
        internal void ResizeCallback()
        {
            if (_isFullScreen)
            {
                ViewportWidth = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Width;
                ViewportHeight = GameInterface.XNAGraphicsDevice.Adapter.CurrentDisplayMode.Height;
            }
            else
            {
                ViewportWidth = GameInterface.XNAGraphicsDevice.Viewport.Width;
                ViewportHeight = GameInterface.XNAGraphicsDevice.Viewport.Height;
            }

            if (GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferWidth != ViewportWidth || GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferHeight != ViewportHeight)
            {
                GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferWidth = ViewportWidth;
                GameInterface.XNAGraphicsDeviceManager.PreferredBackBufferHeight = ViewportHeight;
                GameInterface.XNAGraphicsDeviceManager.ApplyChanges();
            }

            AspectRatio = ViewportWidth / (double)ViewportHeight;

            _XNAViewportRect = new Microsoft.Xna.Framework.Rectangle(0, 0, ViewportWidth, ViewportHeight);

            foreach (Canvas canvas in _canvases)
            {
                canvas.OnScreenResize();
            }
        }
        internal void ExitCallback()
        {
            if (KillProcessOnExit)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        internal void RemoveGameManager(GameManager gameManager)
        {
            _gameManagers.Remove(gameManager);
        }
        internal void AddGameManager(GameManager gameManager)
        {
            _gameManagers.Add(gameManager);
        }

        internal void RemoveCanvas(Canvas canvas)
        {
            _canvases.Remove(canvas);
        }
        internal void AddCanvas(Canvas canvas)
        {
            _canvases.Add(canvas);
        }

        internal void RemoveScene(Scene scene)
        {
            _scenes.Remove(scene);
        }
        internal void AddScene(Scene scene)
        {
            _scenes.Add(scene);
        }

        internal void DrawTextureUnsafe(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            XNASpriteBatch.Draw(texture, _XNAViewportRect, Microsoft.Xna.Framework.Color.White);
        }
        #endregion
        #region Private Methods
        private void Destroy()
        {
            GameInterface.Dispose();

            GameInterface = null;

            InitializationPump = null;
            UpdatePump = null;
            RenderPump = null;
            DrawPump = null;
            OnDestroyPump = null;
            DestructionPump = null;

            Exited = true;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.Game()";
        }
        #endregion
        #region Overridable Methods
        protected virtual void Initialize()
        {

        }
        protected virtual void Update()
        {

        }
        protected virtual void Draw()
        {

        }
        protected virtual void OnDestroy()
        {

        }
        #endregion
    }
}