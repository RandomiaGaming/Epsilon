using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Scene
    {
        #region Constants
        public const ushort DefaultWidth = 256;
        public const ushort DefaultHeight = 144;
        #endregion
        #region Variables
        private List<GameObject> _gameObjects = new List<GameObject>();
        private GameObject[] _gameObjectCache = new GameObject[0];
        private bool _gameObjectCacheValid = true;

        private List<SceneManager> _sceneManagers = new List<SceneManager>();
        private SceneManager[] _sceneManagerCache = new SceneManager[0];
        private bool _sceneManagerCacheValid = true;
        #endregion
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;

        public int CameraPositionX { get; set; } = 0;
        public int CameraPositionY { get; set; } = 0;
        public Point CameraPosition
        {
            get
            {
                return new Point(CameraPositionX, CameraPositionY);
            }
            set
            {
                CameraPositionX = value.X;
                CameraPositionY = value.Y;
            }
        }

        public ushort Width { get; private set; } = 1;
        public ushort Height { get; private set; } = 1;
        #endregion
        #region Constructors
        public Scene(Game game)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }

            Game = game;

            Width = DefaultWidth;
            Height = DefaultHeight;

            Game.AddScene(this);
        }
        public Scene(Game game, ushort width, ushort height)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }

            Game = game;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            Width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            Height = height;

            Game.AddScene(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Scene()";
        }
        #endregion
        #region Methods
        public void DrawTextureWorldSpace(Texture texture, Point position, Color color)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            DrawTextureWorldSpaceUnsafe(texture, position.X, position.Y, color.R, color.B, color.B, color.A);
        }
        public void DrawTextureWorldSpace(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            DrawTextureWorldSpaceUnsafe(texture, x, y, r, g, b, a);
        }
        public void DrawTextureWorldSpaceUnsafe(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            DrawTextureScreenSpaceUnsafe(texture, x - CameraPositionX, y - CameraPositionY, r, g, b, a);
        }
        public void DrawTextureScreenSpace(Texture texture, Point position, Color color)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            DrawTextureScreenSpaceUnsafe(texture, position.X, position.Y, color.R, color.B, color.B, color.A);
        }
        public void DrawTextureScreenSpace(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            DrawTextureScreenSpaceUnsafe(texture, x, y, r, g, b, a);
        }
        public void DrawTextureScreenSpaceUnsafe(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            //Still have to calculate screenrect from world rect.
            Game.DrawTextureUnsafe(texture, x - CameraPositionX, y - CameraPositionY, r, g, b, a);
        }
        public void Destroy()
        {
            foreach(SceneManager sceneManager in _sceneManagerCache)
            {
                sceneManager.Destroy();
            }

            foreach (GameObject gameObject in _gameObjectCache)
            {
                gameObject.Destroy();
            }

            Game.RemoveScene(this);

            _sceneManagers = null;
            _sceneManagerCache = null;
            _gameObjects = null;
            _gameObjectCache = null;
            Game = null;

            IsDestroyed = true;
        }
        public SceneManager GetSceneManager(int index)
        {
            if (index < 0 || index >= _sceneManagerCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _sceneManagerCache[index];
        }
        public SceneManager GetSceneManager(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new Exception("type must be equal to SceneManager or be assignable from SceneManager.");
            }

            foreach (SceneManager sceneManager in _sceneManagerCache)
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
            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)sceneManager;
                }
            }

            return null;
        }
        public List<SceneManager> GetSceneManagers()
        {
            return new List<SceneManager>(_sceneManagerCache);
        }
        public List<SceneManager> GetSceneManagers(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new Exception("type must be equal to SceneManager or be assignable from SceneManager.");
            }

            List<SceneManager> output = new List<SceneManager>();

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(sceneManager);
                }
            }

            return output;
        }
        public List<T> GetSceneManagers<T>() where T : SceneManager
        {
            List<T> output = new List<T>();

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)sceneManager);
                }
            }

            return output;
        }
        public int GetSceneManagerCount()
        {
            return _sceneManagerCache.Length;
        }
        public SceneManager GetSceneManagerUnsafe(int index)
        {
            return _sceneManagerCache[index];
        }
        public SceneManager GetSceneManagerUnsafe(Type type)
        {
            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(type))
                {
                    return sceneManager;
                }
            }

            return null;
        }
        public List<SceneManager> GetSceneManagersUnsafe(Type type)
        {
            List<SceneManager> output = new List<SceneManager>();

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                if (sceneManager.GetType().IsAssignableFrom(type))
                {
                    output.Add(sceneManager);
                }
            }

            return output;
        }
        public GameObject GetGameObject(int index)
        {
            if (index < 0 || index >= _gameObjectCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _gameObjectCache[index];
        }
        public GameObject GetGameObject(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameObject)))
            {
                throw new Exception("type must be equal to GameObject or be assignable from GameObject.");
            }

            foreach (GameObject gameObject in _gameObjectCache)
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
            foreach (GameObject gameObject in _gameObjectCache)
            {
                if (gameObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameObject;
                }
            }

            return null;
        }
        public List<GameObject> GetGameObjects()
        {
            return new List<GameObject>(_gameObjectCache);
        }
        public List<GameObject> GetGameObjects(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameObject)))
            {
                throw new Exception("type must be equal to GameObject or be assignable from GameObject.");
            }

            List<GameObject> output = new List<GameObject>();

            foreach (GameObject gameObject in _gameObjectCache)
            {
                if (gameObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameObject);
                }
            }

            return output;
        }
        public List<T> GetGameObjects<T>() where T : GameObject
        {
            List<T> output = new List<T>();

            foreach (GameObject gameObject in _gameObjectCache)
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
            return _gameObjectCache.Length;
        }
        public GameObject GetGameObjectUnsafe(int index)
        {
            return _gameObjectCache[index];
        }
        public GameObject GetGameObjectUnsafe(Type type)
        {
            foreach (GameObject gameObject in _gameObjectCache)
            {
                if (gameObject.GetType().IsAssignableFrom(type))
                {
                    return gameObject;
                }
            }

            return null;
        }
        public List<GameObject> GetGameObjectsUnsafe(Type type)
        {
            List<GameObject> output = new List<GameObject>();

            foreach (GameObject gameObject in _gameObjectCache)
            {
                if (gameObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(gameObject);
                }
            }

            return output;
        }
        #endregion
        #region Internals
        internal void InvokeUpdate()
        {
            if (!_sceneManagerCacheValid)
            {
                _sceneManagerCache = _sceneManagers.ToArray();
                _sceneManagerCacheValid = true;
            }

            if (!_gameObjectCacheValid)
            {
                _gameObjectCache = _gameObjects.ToArray();
                _gameObjectCacheValid = true;
            }

            Update();

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                sceneManager.InvokeUpdate();
            }

            foreach(GameObject gameObject in _gameObjectCache)
            {
                gameObject.InvokeUpdate();
            }
        }
        internal void InvokeRender()
        {
            Render();

            foreach (SceneManager sceneManager in _sceneManagerCache)
            {
                sceneManager.InvokeRender();
            }

            foreach (GameObject gameObject in _gameObjectCache)
            {
                gameObject.InvokeRender();
            }
        }
        internal void RemoveSceneManager(SceneManager sceneManager)
        {
            _sceneManagers.Remove(sceneManager);

            _sceneManagerCacheValid = false;
        }
        internal void AddSceneManager(SceneManager sceneManager)
        {
            _sceneManagers.Add(sceneManager);

            _sceneManagerCacheValid = false;
        }
        internal void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);

            _gameObjectCacheValid = false;
        }
        internal void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);

            _gameObjectCacheValid = false;
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