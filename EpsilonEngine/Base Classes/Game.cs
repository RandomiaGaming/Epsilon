using System;
using System.Reflection;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Game
    {
        #region Constants
        public static readonly Color DefaultBackgroundColor = new(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        #endregion
        #region Variables
        private List<GameManager> _gameManagers = new List<GameManager>();
        private GameManager[] _gameManagerCache = new GameManager[0];
        private bool _gameManagerCacheValid = true;

        private List<Canvas> _canvases = new List<Canvas>();
        private Canvas[] _canvasCache = new Canvas[0];
        private bool _canvasCacheValid = true;

        private List<Scene> _scenes = new List<Scene>();
        private Scene[] _sceneCache = new Scene[0];
        private bool _sceneCacheValid = true;

        private List<PumpEvent> _updatePump = new List<PumpEvent>();
        private PumpEvent[] _updatePumpCache = new PumpEvent[0];
        private bool _updatePumpCacheValid = true;

        private List<PumpEvent> _renderPump = new List<PumpEvent>();
        private PumpEvent[] _renderPumpCache = new PumpEvent[0];
        private bool _renderPumpCacheValid = true;

        private List<PumpEvent> _singleRunPump = new List<PumpEvent>();
        private bool _singleRunPumpClear = true;

        private GameInterface _gameInterface = null;
        #endregion
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Color BackgroundColor { get; set; } = DefaultBackgroundColor;
        public ushort Width { get; private set; } = 1920;
        public ushort Height { get; private set; } = 1080;
        public float AspectRatio { get; private set; } = 1f;

        public int MousePositionX { get; private set; } = 0;
        public int MousePositionY { get; private set; } = 0;
        public Point MousePosition => new Point(MousePositionX, MousePositionY);

        public float CurrentFPS { get; private set; } = 0f;
        public TimeSpan TimeSinceStart { get; private set; } = new TimeSpan(0);
        public TimeSpan DeltaTime { get; private set; } = new TimeSpan(0);

        public Microsoft.Xna.Framework.Game XNAGame
        {
            get
            {
                return _gameInterface;
            }
        }
        public Microsoft.Xna.Framework.GraphicsDeviceManager GraphicsDeviceManager { get; private set; } = null;
        public Microsoft.Xna.Framework.Graphics.GraphicsDevice GraphicsDevice { get; private set; } = null;
        public Microsoft.Xna.Framework.GameWindow GameWindow { get; private set; } = null;
        public Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch { get; private set; } = null;
        #endregion
        #region Constructors
        public Game()
        {
            _gameInterface = new GameInterface(this);

            GraphicsDeviceManager = new Microsoft.Xna.Framework.GraphicsDeviceManager(_gameInterface);
            GraphicsDeviceManager.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.Reach;
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            GraphicsDeviceManager.HardwareModeSwitch = true;
            GraphicsDeviceManager.IsFullScreen = false;
            GraphicsDeviceManager.PreferHalfPixelOffset = false;
            GraphicsDeviceManager.PreferredBackBufferFormat = Microsoft.Xna.Framework.Graphics.SurfaceFormat.Color;
            GraphicsDeviceManager.SupportedOrientations = Microsoft.Xna.Framework.DisplayOrientation.LandscapeLeft | Microsoft.Xna.Framework.DisplayOrientation.LandscapeRight | Microsoft.Xna.Framework.DisplayOrientation.Portrait | Microsoft.Xna.Framework.DisplayOrientation.PortraitDown | Microsoft.Xna.Framework.DisplayOrientation.Unknown | Microsoft.Xna.Framework.DisplayOrientation.Default;
            GraphicsDeviceManager.ApplyChanges();

            GraphicsDevice = _gameInterface.GraphicsDevice;

            GraphicsDevice.BlendState = Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = Microsoft.Xna.Framework.Graphics.DepthStencilState.None;
            GraphicsDevice.RasterizerState = Microsoft.Xna.Framework.Graphics.RasterizerState.CullNone;

            GameWindow = _gameInterface.Window;

            GameWindow.AllowAltF4 = true;
            GameWindow.AllowUserResizing = true;
            GameWindow.IsBorderless = false;
            GameWindow.Position = new Point(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, GraphicsDevice.Adapter.CurrentDisplayMode.Height / 4).ToXNA();
            GameWindow.Title = "Game";

            _gameInterface.InactiveSleepTime = new TimeSpan(0);
            _gameInterface.TargetElapsedTime = new TimeSpan(10000000 / 60);
            _gameInterface.MaxElapsedTime = new TimeSpan(10000000 / 60);
            _gameInterface.IsFixedTimeStep = false;
            _gameInterface.IsMouseVisible = true;

            SpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            SpriteBatch.Name = "Main SpriteBatch";
            SpriteBatch.Tag = null;

            GameWindow.ClientSizeChanged += ResizeCallback;

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Game))
            {
                RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Game))
            {
                RegisterForRender(Render);
            }

            ResizeCallback(null, null);
        }
        private void ResizeCallback(object sender, EventArgs e)
        {
            if (GraphicsDeviceManager.IsFullScreen)
            {
                Width = (ushort)GraphicsDevice.Adapter.CurrentDisplayMode.Width;
                Height = (ushort)GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            }
            else
            {
                Width = (ushort)GraphicsDevice.Viewport.Width;
                Height = (ushort)GraphicsDevice.Viewport.Height;
            }

            AspectRatio = Width / (float)Height;

            foreach (Canvas canvas in _canvasCache)
            {
                canvas.OnScreenResize();
            }
        }
        #endregion
        #region Methods
        public void DrawTexture(Texture texture, Rectangle rect, Color color)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            DrawTextureUnsafe(texture, rect.MinX, rect.MinY, rect.MaxX, rect.MaxY, color.R, color.B, color.B, color.A);
        }
        public void DrawTexture(Texture texture, int minX, int minY, int maxX, int maxY, byte r, byte g, byte b, byte a)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            if (maxX < minX)
            {
                throw new Exception("maxX must be greater than minX.");
            }
            if (maxY < minY)
            {
                throw new Exception("maxY must be greater than minY.");
            }
            DrawTextureUnsafe(texture, minX, minY, maxX, maxY, r, g, b, a);
        }
        public void DrawTextureUnsafe(Texture texture, int minX, int minY, int maxX, int maxY, byte r, byte g, byte b, byte a)
        {
            int width = maxX - minX;
            int height = maxY - minY;
            minY = Height - minY - maxY + minY;
            SpriteBatch.Draw(texture.XNABase, new Microsoft.Xna.Framework.Rectangle(minX, minY, width, height), new Microsoft.Xna.Framework.Color(r, g, b, a));
        }
        public void Run()
        {
            _gameInterface.Run();
        }
        public void Destroy()
        {
            foreach (GameManager gameManager in _gameManagerCache)
            {
                gameManager.Destroy();
            }

            foreach (Canvas canvas in _canvasCache)
            {
                canvas.Destroy();
            }

            foreach (Scene scene in _sceneCache)
            {
                scene.Destroy();
            }

            _gameInterface.Exit();
            _gameInterface = null;

            GraphicsDeviceManager.Dispose();
            GraphicsDeviceManager = null;

            GraphicsDevice.Dispose();
            GraphicsDevice = null;

            SpriteBatch.Dispose();
            SpriteBatch = null;

            GameWindow = null;

            _gameManagers = null;
            _gameManagerCache = null;
            _scenes = null;
            _sceneCache = null;

            IsDestroyed = true;
        }
        public GameManager GetGameManager(int index)
        {
            if (index < 0 || index >= _gameManagerCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _gameManagerCache[index];
        }
        public GameManager GetGameManager(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new Exception("type must be equal to GameManager or be assignable from GameManager.");
            }

            foreach (GameManager gameManager in _gameManagerCache)
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
            foreach (GameManager gameManager in _gameManagerCache)
            {
                if (gameManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameManager;
                }
            }

            return null;
        }
        public List<GameManager> GetGameManagers()
        {
            return new List<GameManager>(_gameManagerCache);
        }
        public List<GameManager> GetGameManagers(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new Exception("type must be equal to GameManager or be assignable from GameManager.");
            }

            List<GameManager> output = new List<GameManager>();

            foreach (GameManager gameManager in _gameManagerCache)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameManager);
                }
            }

            return output;
        }
        public List<T> GetGameManagers<T>() where T : GameManager
        {
            List<T> output = new List<T>();

            foreach (GameManager gameManager in _gameManagerCache)
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
            return _gameManagerCache.Length;
        }
        public GameManager GetGameManagerUnsafe(int index)
        {
            return _gameManagerCache[index];
        }
        public GameManager GetGameManagerUnsafe(Type type)
        {
            foreach (GameManager gameManager in _gameManagerCache)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    return gameManager;
                }
            }

            return null;
        }
        public List<GameManager> GetGameManagersUnsafe(Type type)
        {
            List<GameManager> output = new List<GameManager>();

            foreach (GameManager gameManager in _gameManagerCache)
            {
                if (gameManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameManager);
                }
            }

            return output;
        }
        public Canvas GetCanvas(int index)
        {
            if (index < 0 || index >= _canvasCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _canvasCache[index];
        }
        public Canvas GetCanvas(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Canvas)))
            {
                throw new Exception("type must be equal to Canvas or be assignable from Canvas.");
            }

            foreach (Canvas canvas in _canvasCache)
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
            foreach (Canvas canvas in _canvasCache)
            {
                if (canvas.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)canvas;
                }
            }

            return null;
        }
        public List<Canvas> GetCanvases()
        {
            return new List<Canvas>(_canvasCache);
        }
        public List<Canvas> GetCanvases(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Canvas)))
            {
                throw new Exception("type must be equal to Canvas or be assignable from Canvas.");
            }

            List<Canvas> output = new List<Canvas>();

            foreach (Canvas canvas in _canvasCache)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvas);
                }
            }

            return output;
        }
        public List<T> GetCanvases<T>() where T : Canvas
        {
            List<T> output = new List<T>();

            foreach (Canvas canvas in _canvasCache)
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
            return _canvasCache.Length;
        }
        public Canvas GetCanvasUnsafe(int index)
        {
            return _canvasCache[index];
        }
        public Canvas GetCanvasUnsafe(Type type)
        {
            foreach (Canvas canvas in _canvasCache)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    return canvas;
                }
            }

            return null;
        }
        public List<Canvas> GetCanvasesUnsafe(Type type)
        {
            List<Canvas> output = new List<Canvas>();

            foreach (Canvas canvas in _canvasCache)
            {
                if (canvas.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvas);
                }
            }

            return output;
        }
        public Scene GetScene(int index)
        {
            if (index < 0 || index >= _sceneCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _sceneCache[index];
        }
        public Scene GetScene(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Scene)))
            {
                throw new Exception("type must be equal to Scene or be assignable from Scene.");
            }

            foreach (Scene scene in _sceneCache)
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
            foreach (Scene scene in _sceneCache)
            {
                if (scene.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)scene;
                }
            }

            return null;
        }
        public List<Scene> GetScenes()
        {
            return new List<Scene>(_sceneCache);
        }
        public List<Scene> GetScenes(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Scene)))
            {
                throw new Exception("type must be equal to Scene or be assignable from Scene.");
            }

            List<Scene> output = new List<Scene>();

            foreach (Scene scene in _sceneCache)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    output.Add(scene);
                }
            }

            return output;
        }
        public List<T> GetScenes<T>() where T : Scene
        {
            List<T> output = new List<T>();

            foreach (Scene scene in _sceneCache)
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
            return _sceneCache.Length;
        }
        public Scene GetSceneUnsafe(int index)
        {
            return _sceneCache[index];
        }
        public Scene GetSceneUnsafe(Type type)
        {
            foreach (Scene scene in _sceneCache)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    return scene;
                }
            }

            return null;
        }
        public List<Scene> GetScenesUnsafe(Type type)
        {
            List<Scene> output = new List<Scene>();

            foreach (Scene scene in _sceneCache)
            {
                if (scene.GetType().IsAssignableFrom(type))
                {
                    output.Add(scene);
                }
            }

            return output;
        }
        #endregion
        #region Internals
        internal void RegisterForUpdate(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new Exception("pumpEvent cannot be null.");
            }

            _updatePump.Add(pumpEvent);
            _updatePumpCacheValid = false;
        }
        internal void RegisterForRender(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new Exception("pumpEvent cannot be null.");
            }

            _renderPump.Add(pumpEvent);
            _renderPumpCacheValid = false;
        }
        internal void RegisterForSingleRun(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new Exception("pumpEvent cannot be null.");
            }

            _singleRunPump.Add(pumpEvent);
            _singleRunPumpClear = false;
        }
        internal void UpdateCallback()
        {
            DebugProfiler.UpdateStart();

            Microsoft.Xna.Framework.Input.MouseState XNAMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            MousePositionX = XNAMouseState.X;
            MousePositionY = Height - XNAMouseState.Y;

            if (!_gameManagerCacheValid)
            {
                _gameManagerCache = _gameManagers.ToArray();
                _gameManagerCacheValid = true;
            }

            if (!_canvasCacheValid)
            {
                _canvasCache = _canvases.ToArray();
                _canvasCacheValid = true;
            }

            if (!_sceneCacheValid)
            {
                _sceneCache = _scenes.ToArray();
                _sceneCacheValid = true;
            }

            if (!_singleRunPumpClear)
            {
                int initializationPumpLength = _singleRunPump.Count;
                for (int i = 0; i < initializationPumpLength; i++)
                {
                    _singleRunPump[i].Invoke();
                }
                _singleRunPump = new List<PumpEvent>();
                _singleRunPumpClear = true;
            }

            if (!_updatePumpCacheValid)
            {
                _updatePumpCache = _updatePump.ToArray();
                _updatePumpCacheValid = true;
            }

            foreach (PumpEvent updatePumpEvent in _updatePumpCache)
            {
                updatePumpEvent.Invoke();
            }

            DebugProfiler.UpdateEnd();

            DebugProfiler.RenderStart();

            int sceneCacheLength = _sceneCache.Length;
            for (int i = 0; i < sceneCacheLength; i++)
            {
                _sceneCache[i].RenderStart();
            }

            if (!_renderPumpCacheValid)
            {
                _renderPumpCache = _renderPump.ToArray();
                _renderPumpCacheValid = true;
            }



            foreach (PumpEvent renderPumpEvent in _renderPumpCache)
            {
                renderPumpEvent.Invoke();
            }

            for (int i = 0; i < sceneCacheLength; i++)
            {
                _sceneCache[i].RenderEnd();
            }

            GraphicsDevice.Clear(BackgroundColor.ToXNA());

            SpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            for (int i = 0; i < sceneCacheLength; i++)
            {
                Microsoft.Xna.Framework.Graphics.RenderTarget2D sceneRenderTarget = _sceneCache[i]._renderTarget;
                SpriteBatch.Draw(sceneRenderTarget, new Microsoft.Xna.Framework.Rectangle(0, 0, Width, Height), new Microsoft.Xna.Framework.Color(255, 255, 255, 255));
            }

            SpriteBatch.End();

            DebugProfiler.RenderEnd();

            DebugProfiler.FrameEnd();

            DebugProfiler.Print();

            DebugProfiler.FrameStart();
        }
        internal void RemoveGameManager(GameManager gameManager)
        {
            _gameManagers.Remove(gameManager);

            _gameManagerCacheValid = false;
        }
        internal void AddGameManager(GameManager gameManager)
        {
            _gameManagers.Add(gameManager);

            _gameManagerCacheValid = false;
        }
        internal void RemoveCanvas(Canvas canvas)
        {
            _canvases.Remove(canvas);

            _canvasCacheValid = false;
        }
        internal void AddCanvas(Canvas canvas)
        {
            _canvases.Add(canvas);

            _canvasCacheValid = false;
        }
        internal void RemoveScene(Scene scene)
        {
            _scenes.Remove(scene);

            _sceneCacheValid = false;
        }
        internal void AddScene(Scene scene)
        {
            _scenes.Add(scene);

            _sceneCacheValid = false;
        }
        #endregion
        #region Overridables
        protected virtual void Update()
        {

        }
        protected virtual void Render()
        {

        }
        #endregion
    }
}