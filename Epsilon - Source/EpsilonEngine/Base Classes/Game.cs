using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Game
    {
        #region Constants
        public static readonly Color DefaultBackgroundColor = new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        #endregion
        #region Variables
        private Microsoft.Xna.Framework.GraphicsDeviceManager _graphicsDeviceManager = null;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch = null;

        private GameInterface _gameInterface = null;

        private List<Scene> _scenes = new List<Scene>();
        private Scene[] _sceneCache = new Scene[0];
        private bool _sceneCacheValid = true;

        private List<GameManager> _gameManagers = new List<GameManager>();
        private GameManager[] _gameManagerCache = new GameManager[0];
        private bool _gameManagerCacheValid = true;
        #endregion
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Color BackgroundColor { get; set; } = DefaultBackgroundColor;

        public float CurrentFPS { get; private set; } = 0f;
        public TimeSpan TimeSinceStart { get; private set; } = new TimeSpan(0);
        public TimeSpan DeltaTime { get; private set; } = new TimeSpan(0);
        #endregion
        #region Constructors
        public Game()
        {
            _gameInterface = new GameInterface(this);

            _graphicsDeviceManager = new Microsoft.Xna.Framework.GraphicsDeviceManager(_gameInterface);
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(_gameInterface.GraphicsDevice);
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
            if(minX > maxX)
            {
                throw new Exception("minX must be less than or equal to maxX.");
            }
            if (minY > maxY)
            {
                throw new Exception("minX must be less than or equal to maxX.");
            }
            DrawTextureUnsafe(texture, minX, minY, maxX, maxY, r, g, b, a);
        }
        public void DrawTextureUnsafe(Texture texture, int minX, int minY, int maxX, int maxY, byte r, byte g, byte b, byte a)
        {
            //Still have to invert y axis.
            _spriteBatch.Draw(texture.ToXNA(), new Microsoft.Xna.Framework.Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1), new Microsoft.Xna.Framework.Rectangle(0, 0, texture.Width, texture.Height), new Microsoft.Xna.Framework.Color(), 0, Microsoft.Xna.Framework.Vector2.Zero, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }
        public void Destroy()
        {
            foreach (GameManager gameManager in _gameManagerCache)
            {
                gameManager.Destroy();
            }

            foreach (Scene scene in _sceneCache)
            {
                scene.Destroy();
            }

            GameInterface.Destroy();

            _gameManagers = null;
            _gameManagerCache = null;
            _scenes = null;
            _sceneCache = null;

            _graphicsDeviceManager = null;
            _spriteBatch = null;

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
        internal void InvokeUpdate()
        {
            if (!_gameManagerCacheValid)
            {
                _gameManagerCache = _gameManagers.ToArray();
                _gameManagerCacheValid = true;
            }

            if (!_sceneCacheValid)
            {
                _sceneCache = _scenes.ToArray();
                _sceneCacheValid = true;
            }

            Update();

            foreach (GameManager gameManager in _gameManagerCache)
            {
                gameManager.InvokeUpdate();
            }

            foreach (Scene scene in _sceneCache)
            {
                scene.InvokeUpdate();
            }
        }
        internal void InvokeRender()
        {
            _gameInterface.GraphicsDevice.Clear(BackgroundColor.ToXNA());

            _spriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            Render();

            foreach (GameManager gameManager in _gameManagerCache)
            {
                gameManager.InvokeRender();
            }

            foreach (Scene scene in _sceneCache)
            {
                scene.InvokeRender();
            }

            _spriteBatch.End();
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