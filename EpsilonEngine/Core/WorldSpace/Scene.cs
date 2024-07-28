namespace EpsilonEngine
{
    public class Scene
    {
        #region Public Variables
        public Game Game { get; private set; } = null;
        public bool MarkedForDestruction { get; private set; } = false;
        public bool Destroyed { get; private set; } = false;
        public bool Rendering { get; private set; } = false;
        public bool OverridesUpdate { get; private set; } = false;
        public int UpdatePriority { get; private set; } = 0;
        public bool OverridesRender { get; private set; } = false;
        public int RenderPriority { get; private set; } = 0;
        public int CameraPositionX
        {
            get { return _cameraPositionX; }
            set
            {
                _cameraPositionX = value;
                _cameraPosition.X = value;
                CameraMovePump.Invoke();
            }
        }
        public int CameraPositionY
        {
            get { return _cameraPositionY; }
            set
            {
                _cameraPositionY = value;
                _cameraPosition.Y = value;
                CameraMovePump.Invoke();
            }
        }
        public Point CameraPosition
        {
            get { return _cameraPosition; }
            set
            {
                _cameraPositionX = value.X;
                _cameraPositionY = value.Y;
                _cameraPosition.X = value.X;
                _cameraPosition.Y = value.Y;
                CameraMovePump.Invoke();
            }
        }
        public ushort RenderWidth { get; private set; } = 256;
        public ushort RenderHeight { get; private set; } = 144;
        public double RenderAspectRatio { get; private set; } = 16f / 9f;
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                _XNABackgroundColorCache = new Microsoft.Xna.Framework.Color(_backgroundColor.R, _backgroundColor.G, _backgroundColor.B, _backgroundColor.A);
            }
        }
        #endregion
        #region Internal Variables
        internal InverseOrderedPump RenderPump = new InverseOrderedPump();
        internal UnorderedPump CameraMovePump = new UnorderedPump();
        internal Microsoft.Xna.Framework.Graphics.SpriteBatch XNASpriteBatch = null;
        internal Microsoft.Xna.Framework.Graphics.RenderTarget2D XNARenderTarget = null;
        #endregion
        #region Private Variables
        private System.Collections.Generic.List<GameObject> _gameObjects = new System.Collections.Generic.List<GameObject>();
        private System.Collections.Generic.List<SceneManager> _sceneManagers = new System.Collections.Generic.List<SceneManager>();
        private int _cameraPositionX = 0;
        private int _cameraPositionY = 0;
        private Point _cameraPosition = new Point(0, 0);
        private Microsoft.Xna.Framework.Color _XNABackgroundColorCache = new Microsoft.Xna.Framework.Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue);
        private Color _backgroundColor = new Color(byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue);
        private Microsoft.Xna.Framework.Vector2 _XNAReusableDrawPosition = new Microsoft.Xna.Framework.Vector2(0, 0);
        private Microsoft.Xna.Framework.Color _XNAReusableDrawColor = new Microsoft.Xna.Framework.Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        #endregion
        #region Constructors
        public Scene(Game game, ushort renderWidth, ushort renderHeight, int updatePriority, int renderPriority, int drawPriority)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (renderWidth <= 0)
            {
                throw new System.Exception("renderWidth must be greater than 0.");
            }
            RenderWidth = renderWidth;
            if (renderHeight <= 0)
            {
                throw new System.Exception("renderHeight must be greater than 0.");
            }
            RenderHeight = renderHeight;
            RenderAspectRatio = RenderWidth / (double)RenderHeight;
            XNARenderTarget = new Microsoft.Xna.Framework.Graphics.RenderTarget2D(Game.GameInterface.XNAGraphicsDevice, renderWidth, renderHeight);
            XNASpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(Game.GameInterface.XNAGraphicsDevice);
            Game.AddScene(this);
            System.Type thisType = GetType();
            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Scene))
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, updatePriority);
                OverridesUpdate = true;
            }
            System.Reflection.MethodInfo renderMethod = thisType.GetMethod("Render", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Scene))
            {
                RenderPump.RegisterPumpEventUnsafe(Render, renderPriority);
                OverridesRender = true;
            }
            Game.InitializationPump.RegisterPumpEventUnsafe(Initialize);
            Game.RenderPump.RegisterPumpEventUnsafe(RenderScene);
            Game.DrawPump.RegisterPumpEventUnsafe(Draw, drawPriority);
        }
        #endregion
        #region Public Methods
        public void DrawTextureWorldSpace(Texture texture, Point position, Color color)
        {
            if (!Rendering)
            {
                throw new System.Exception("cannot draw texture because scene is not rendering.");
            }
            if (texture is null)
            {
                throw new System.Exception("texture cannot be null.");
            }
            DrawTextureWorldSpaceUnsafe(texture._xnaTexture, position.X, position.Y, color.R, color.B, color.B, color.A);
        }
        public void DrawTextureScreenSpace(Texture texture, Point position, Color color)
        {
            if (!Rendering)
            {
                throw new System.Exception("cannot draw texture because scene is not rendering.");
            }
            if (texture is null)
            {
                throw new System.Exception("texture cannot be null.");
            }
            DrawTextureScreenSpaceUnsafe(texture._xnaTexture, new Microsoft.Xna.Framework.Vector2(position.X, position.Y), new Microsoft.Xna.Framework.Color(color.R, color.B, color.B, color.A));
        }
        public void MarkForDestruction()
        {
            if (MarkedForDestruction)
            {
                throw new System.Exception("scene has already been marked for destruction.");
            }
            if (Destroyed)
            {
                throw new System.Exception("scene has already been destroyed.");
            }
            int sceneManagerCount = _sceneManagers.Count;
            for (int i = 0; i < sceneManagerCount; i++)
            {
                //_sceneManagers[i].MarkForDestruction();
            }
            int gameObjectCount = _gameObjects.Count;
            for (int i = 0; i < gameObjectCount; i++)
            {
                //_gameObjects[i].MarkForDestruction();
            }
            Game.OnDestroyPump.RegisterPumpEvent(OnDestroy);
            Game.DestructionPump.RegisterPumpEvent(Destroy);
            MarkedForDestruction = true;
        }
        public SceneManager GetSceneManager(int index)
        {
            if (index < 0 || index >= _sceneManagers.Count)
            {
                throw new System.Exception("index was out of range.");
            }
            return _sceneManagers[index];
        }
        public SceneManager GetSceneManager(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new System.Exception("type must be equal to SceneManager or be assignable from SceneManager.");
            }
            foreach (SceneManager sceneManager in _sceneManagers)
            {
                if (sceneManager.GetType().IsAssignableFrom(type))
                {
                    return sceneManager;
                }
            }
            return null;
        }
        public T GetSceneManager<T>() where T : SceneManager
        {
            foreach (SceneManager sceneManager in _sceneManagers)
            {
                if (sceneManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)sceneManager;
                }
            }
            return null;
        }
        public System.Collections.Generic.List<SceneManager> GetSceneManagers()
        {
            return new System.Collections.Generic.List<SceneManager>(_sceneManagers);
        }
        public SceneManager[] GetSceneManagers(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new System.Exception("type must be equal to SceneManager or be assignable from SceneManager.");
            }
            System.Collections.Generic.List<SceneManager> output = new System.Collections.Generic.List<SceneManager>();
            foreach (SceneManager sceneManager in _sceneManagers)
            {
                if (sceneManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(sceneManager);
                }
            }
            return output.ToArray();
        }
        public T[] GetSceneManagers<T>() where T : SceneManager
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();
            foreach (SceneManager sceneManager in _sceneManagers)
            {
                if (sceneManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)sceneManager);
                }
            }
            return output.ToArray();
        }
        public int GetSceneManagerCount()
        {
            return _sceneManagers.Count;
        }
        public GameObject GetGameObject(int index)
        {
            if (index < 0 || index >= _gameObjects.Count)
            {
                throw new System.Exception("index was out of range.");
            }
            return _gameObjects[index];
        }
        public GameObject GetGameObject(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(GameObject)))
            {
                throw new System.Exception("type must be equal to GameObject or be assignable from GameObject.");
            }
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.GetType().IsAssignableFrom(type))
                {
                    return gameObject;
                }
            }
            return null;
        }
        public T GetGameObject<T>() where T : GameObject
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameObject;
                }
            }
            return null;
        }
        public System.Collections.Generic.List<GameObject> GetGameObjects()
        {
            return new System.Collections.Generic.List<GameObject>(_gameObjects);
        }
        public System.Collections.Generic.List<GameObject> GetGameObjects(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(GameObject)))
            {
                throw new System.Exception("type must be equal to GameObject or be assignable from GameObject.");
            }
            System.Collections.Generic.List<GameObject> output = new System.Collections.Generic.List<GameObject>();
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameObject);
                }
            }
            return output;
        }
        public System.Collections.Generic.List<T> GetGameObjects<T>() where T : GameObject
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)gameObject);
                }
            }
            return output;
        }
        public int GetGameObjectCount()
        {
            return _gameObjects.Count;
        }
        #endregion
        #region Internal Methods
        internal void DrawTextureWorldSpaceUnsafe(Microsoft.Xna.Framework.Graphics.Texture2D texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            _XNAReusableDrawPosition.X = x - _cameraPositionX;
            _XNAReusableDrawPosition.Y = RenderHeight - y + _cameraPositionY - texture.Height;
            _XNAReusableDrawColor.R = r;
            _XNAReusableDrawColor.G = g;
            _XNAReusableDrawColor.B = b;
            _XNAReusableDrawColor.A = a;
            XNASpriteBatch.Draw(texture, _XNAReusableDrawPosition, _XNAReusableDrawColor);
        }
        internal void DrawTextureScreenSpaceUnsafe(Microsoft.Xna.Framework.Graphics.Texture2D texture, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color)
        {
            position.Y = RenderHeight - position.Y - texture.Height;
            XNASpriteBatch.Draw(texture, position, color);
        }
        internal void RemoveSceneManager(SceneManager sceneManager)
        {
            _sceneManagers.Remove(sceneManager);
        }
        internal void AddSceneManager(SceneManager sceneManager)
        {
            _sceneManagers.Add(sceneManager);
        }
        internal void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }
        internal void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }
        #endregion
        #region Private Methods
        private void Draw()
        {
            /*int colorDataLength = XNARenderTarget.Width * XNARenderTarget.Height;

             Microsoft.Xna.Framework.Color[] colorData = new Microsoft.Xna.Framework.Color[colorDataLength];

             XNARenderTarget.GetData(colorData);

             for (int i = 0; i < colorDataLength; i++)
             {
                 Microsoft.Xna.Framework.Color color = colorData[i];

                 byte brightness = (byte)((color.R + color.G + color.B) / 3);

                 colorData[i] = new Microsoft.Xna.Framework.Color(brightness, brightness, brightness, byte.MaxValue);
             }

             XNARenderTarget.SetData(colorData);*/
            Game.DrawTextureUnsafe(XNARenderTarget);
        }
        private void RenderScene()
        {
            Rendering = true;
            Game.GameInterface.GraphicsDevice.SetRenderTarget(XNARenderTarget);
            Game.GameInterface.GraphicsDevice.Clear(_XNABackgroundColorCache);
            XNASpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);
            RenderPump.Invoke();
            XNASpriteBatch.End();
            Game.GameInterface.GraphicsDevice.SetRenderTarget(null);
            Rendering = false;
        }
        private void Destroy()
        {
            Game.RemoveScene(this);
            _sceneManagers = null;
            _gameObjects = null;
            Game = null;
            Destroyed = true;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.Scene()";
        }
        #endregion
        #region Overridable Methods
        protected virtual void Initialize()
        {
        }
        protected virtual void Update()
        {
        }
        protected virtual void Render()
        {
        }
        protected virtual void OnDestroy()
        {
        }
        #endregion
    }
}