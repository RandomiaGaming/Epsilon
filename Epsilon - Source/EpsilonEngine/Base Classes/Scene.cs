using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace EpsilonEngine
{
    public class Scene
    {
        #region Variables
        private Engine _engine = null;
        private bool _markedForDestruction = false;
        private bool _destroyed = false;
        private bool _renderring = false;

        private RenderTarget2D _renderTarget = null;
        private SpriteBatch _spriteBatch = null;
        private Texture2D _pixelTexture = null;
        private Point _cameraPosition = new Point(0, 0);

        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<GameObject> _gameObjectAddQue = new List<GameObject>();
        private List<GameObject> _gameObjectRemoveQue = new List<GameObject>();

        private List<SceneManager> _sceneManagers = new List<SceneManager>();
        private List<SceneManager> _sceneManagerAddQue = new List<SceneManager>();
        private List<SceneManager> _sceneManagerRemoveQue = new List<SceneManager>();
        #endregion
        #region Properties
        public Engine Engine
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Scene has been destroyed.");
                }

                return _engine;
            }
        }
        public Point CameraPosition
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Scene has been destroyed.");
                }

                return _cameraPosition;
            }
            set
            {
                if (_destroyed)
                {
                    throw new Exception("Scene has been destroyed.");
                }

                _cameraPosition = value;
            }
        }
        public Point ViewPortSize
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Scene has been destroyed.");
                }

                return new Point(_renderTarget.Width, _renderTarget.Height);
            }
        }
        public Rectangle ViewPortRect
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Scene has been destroyed.");
                }

                return new Rectangle(CameraPosition, ViewPortSize);
            }
        }
        public float AspectRatio
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Scene has been destroyed.");
                }

                return (float)ViewPortSize.Y / (float)ViewPortSize.X;
            }
        }
        public bool Destroyed
        {
            get
            {
                return _destroyed;
            }
        }
        public bool MarkedForDestruction
        {
            get
            {
                return _markedForDestruction;
            }
        }
        #endregion
        #region Constructors
        public Scene(Engine engine, ushort viewPortWidth, ushort viewPortHeight)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }

            _engine = engine;

            if (viewPortWidth <= 0)
            {
                throw new Exception("viewPortWidth must be greater than 0.");
            }
            if (viewPortHeight <= 0)
            {
                throw new Exception("viewPortHeight must be greater than 0.");
            }

            _renderTarget = new RenderTarget2D(_engine.GraphicsDevice, viewPortWidth, viewPortHeight, false, SurfaceFormat.Color, DepthFormat.None);

            _spriteBatch = new SpriteBatch(_engine.GraphicsDevice);
            _spriteBatch.Name = "Stage SpriteBatch";
            _spriteBatch.Tag = null;

            _pixelTexture = new Texture2D(_engine.GraphicsDevice, 1, 1);
            _pixelTexture.SetData(new Microsoft.Xna.Framework.Color[] { new Microsoft.Xna.Framework.Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue) });
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Scene()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            if (_destroyed)
            {
                throw new Exception("Scene has already been destroyed.");
            }

            if (_markedForDestruction)
            {
                throw new Exception("Scene has already been marked for destruction.");
            }

            foreach (SceneManager sceneManager in _sceneManagers)
            {
                sceneManager.Destroy();
            }

            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Destroy();
            }

            _engine.ChangeStage(null);

            _markedForDestruction = true;
        }
        public void DrawTexture(Texture texture, Point worldSpacePosition, Color color)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (!_renderring)
            {
                throw new Exception("Scene is not currently renderring.");
            }

            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }

            worldSpacePosition = worldSpacePosition - _cameraPosition;
            worldSpacePosition = new Point(worldSpacePosition.X, ViewPortSize.Y - worldSpacePosition.Y);
            worldSpacePosition = new Point(worldSpacePosition.X, worldSpacePosition.Y - texture.Height);

            if (worldSpacePosition.X + texture.Width < 0 || worldSpacePosition.Y + texture.Height < 0 || worldSpacePosition.X > ViewPortSize.X || worldSpacePosition.Y > ViewPortSize.Y)
            {
                return;
            }

            _spriteBatch.Draw(texture.ToXNA(), new Microsoft.Xna.Framework.Rectangle(worldSpacePosition.X, worldSpacePosition.Y, texture.Width, texture.Height), new Microsoft.Xna.Framework.Rectangle(0, 0, texture.Width, texture.Height), color.ToXNA(), 0, new Vector2(0, 0), SpriteEffects.None, 0); ;
        }
        public void DrawTextureOverlay(Texture texture, Point screenSpacePosition, Color color)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (!_renderring)
            {
                throw new Exception("Scene is not currently renderring.");
            }

            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }

            screenSpacePosition = new Point(screenSpacePosition.X, ViewPortSize.Y - screenSpacePosition.Y);
            screenSpacePosition = new Point(screenSpacePosition.X, screenSpacePosition.Y - texture.Height);

            if (screenSpacePosition.X + texture.Width < 0 || screenSpacePosition.Y + texture.Height < 0 || screenSpacePosition.X > ViewPortSize.X || screenSpacePosition.Y > ViewPortSize.Y)
            {
                return;
            }

            _spriteBatch.Draw(texture.ToXNA(), new Rectangle(screenSpacePosition.X, screenSpacePosition.Y, texture.Width, texture.Height).ToXNA(), new Rectangle(0, 0, texture.Width, texture.Height).ToXNA(), color.ToXNA(), 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }
        public void DrawRect(Rectangle rectangle, Color color)
        {
            Rectangle screenRect = new Rectangle(CameraPosition, CameraPosition + ViewPortSize);
            if (!rectangle.Overlaps(screenRect))
            {
                return;
            }

            Point worldSpacePosition = new Point(rectangle.MinX, rectangle.MinY) - _cameraPosition;
            worldSpacePosition = new Point(worldSpacePosition.X, ViewPortSize.Y - worldSpacePosition.Y);
            worldSpacePosition = new Point(worldSpacePosition.X, worldSpacePosition.Y - rectangle.Height);

            _spriteBatch.Draw(_pixelTexture, new Microsoft.Xna.Framework.Rectangle(worldSpacePosition.X, worldSpacePosition.Y, rectangle.Width, rectangle.Height), new Microsoft.Xna.Framework.Rectangle(0, 0, 1, 1), color.ToXNA(), 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }
        internal void InvokeInitialize()
        {
            foreach (SceneManager sceneManagerToAdd in _sceneManagerAddQue)
            {
                _sceneManagers.Add(sceneManagerToAdd);
            }

            foreach (GameObject gameObjectToAdd in _gameObjectAddQue)
            {
                _gameObjects.Add(gameObjectToAdd);
            }

            Initialize();

            foreach (SceneManager addedSceneManager in _sceneManagerAddQue)
            {
                addedSceneManager.InvokeInitialize();
            }

            _sceneManagerAddQue = new List<SceneManager>();

            foreach (GameObject addedGameObject in _gameObjectAddQue)
            {
                addedGameObject.InvokeInitialize();
            }

            _gameObjectAddQue = new List<GameObject>();
        }
        public void InvokeUpdate()
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            foreach (SceneManager sceneManagerToRemove in _sceneManagerRemoveQue)
            {
                sceneManagerToRemove.InvokeOnDestroy();
            }

            foreach (GameObject gameObjectToRemove in _gameObjectRemoveQue)
            {
                gameObjectToRemove.InvokeOnDestroy();
            }

            foreach (SceneManager removedSceneManager in _sceneManagerRemoveQue)
            {
                _sceneManagers.Remove(removedSceneManager);
            }

            _sceneManagerRemoveQue = new List<SceneManager>();

            foreach (GameObject removedGameObject in _gameObjectRemoveQue)
            {
                _gameObjects.Remove(removedGameObject);
            }

            _gameObjectRemoveQue = new List<GameObject>();

            foreach (SceneManager sceneManagerToAdd in _sceneManagerAddQue)
            {
                _sceneManagers.Add(sceneManagerToAdd);
            }

            foreach (GameObject gameObjectToAdd in _gameObjectAddQue)
            {
                _gameObjects.Add(gameObjectToAdd);
            }

            foreach (SceneManager addedSceneManager in _sceneManagerAddQue)
            {
                addedSceneManager.InvokeInitialize();
            }

            _sceneManagerAddQue = new List<SceneManager>();

            foreach (GameObject addedGameObject in _gameObjectAddQue)
            {
                addedGameObject.InvokeInitialize();
            }

            _gameObjectAddQue = new List<GameObject>();

            Update();

            foreach (SceneManager sceneManager in _sceneManagers)
            {
                sceneManager.InvokeUpdate();
            }

            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.InvokeUpdate();
            }
        }
        public Texture2D InvokeRender()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            _renderring = true;

            _engine.GraphicsDevice.SetRenderTarget(_renderTarget);

            _engine.GraphicsDevice.Clear(Color.Transparent.ToXNA());

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            Render();

            foreach (SceneManager sceneManager in _sceneManagers)
            {
                sceneManager.InvokeRender();
            }

            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.InvokeRender();
            }

            _spriteBatch.End();

            _engine.GraphicsDevice.SetRenderTarget(null);

            _renderring = false;

            return _renderTarget;
        }
        internal void InvokeOnDestroy()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has alread been destroyed.");
            }

            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.InvokeOnDestroy();
            }

            OnDestroy();

            _gameObjects = null;
            _gameObjectAddQue = null;
            _gameObjectRemoveQue = null;
            _engine = null;

            _destroyed = true;
        }
        #endregion
        #region GameObject Management
        public GameObject GetGameObject(int index)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (index < 0 || index >= _gameObjects.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _gameObjects[index];
        }
        public GameObject GetGameObject(Type type)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameObject)))
            {
                throw new Exception("type must be equal to GameObject or be assignable from GameObject.");
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
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            foreach (GameObject gameObject in _gameObjects)
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
            if (_destroyed)
            {
                throw new Exception("Scene has already been destroyed.");
            }

            return new List<GameObject>(_gameObjects);
        }
        public List<GameObject> GetGameObjects(Type type)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(GameObject)))
            {
                throw new Exception("type must be equal to GameObject or be assignable from GameObject.");
            }

            List<GameObject> output = new List<GameObject>();

            foreach (GameObject gameObject in _gameObjects)
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
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            List<T> output = new List<T>();

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
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            return _gameObjects.Count;
        }
        internal void RemoveGameObject(GameObject gameObject)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (gameObject is null)
            {
                throw new Exception("gameObject cannot be null.");
            }

            if (gameObject.Scene != this)
            {
                throw new Exception("gameObject belongs to a different Scene.");
            }


            bool gameObjectAdded = false;
            foreach (GameObject addedGameObject in _gameObjects)
            {
                if (addedGameObject == gameObject)
                {
                    gameObjectAdded = true;
                }
            }
            if (!gameObjectAdded)
            {
                throw new Exception("gameObject has already been removed.");
            }

            foreach (GameObject removeQueGameObject in _gameObjectRemoveQue)
            {
                if (removeQueGameObject == gameObject)
                {
                    throw new Exception("gameObject has already been removed.");
                }
            }

            _gameObjectRemoveQue.Add(gameObject);
        }
        internal void AddGameObject(GameObject gameObject)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (gameObject is null)
            {
                throw new Exception("gameObject cannot be null.");
            }

            if (gameObject.Scene != this)
            {
                throw new Exception("gameObject belongs to a different Scene.");
            }

            foreach (GameObject addedGameObject in _gameObjects)
            {
                if (addedGameObject == gameObject)
                {
                    throw new Exception("gameObject has already been added.");
                }
            }

            foreach (GameObject addQueGameObject in _gameObjectAddQue)
            {
                if (addQueGameObject == gameObject)
                {
                    throw new Exception("gameObject has already been added.");
                }
            }

            _gameObjectAddQue.Add(gameObject);
        }
        #endregion
        #region SceneManager Management
        public SceneManager GetSceneManager(int index)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (index < 0 || index >= _sceneManagers.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _sceneManagers[index];
        }
        public SceneManager GetSceneManager(Type type)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new Exception("type must be equal to SceneManager or be assignable from SceneManager.");
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
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            foreach (SceneManager sceneManager in _sceneManagers)
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
            if (_destroyed)
            {
                throw new Exception("Scene has already been destroyed.");
            }

            return new List<SceneManager>(_sceneManagers);
        }
        public List<SceneManager> GetSceneManagers(Type type)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new Exception("type must be equal to SceneManager or be assignable from SceneManager.");
            }

            List<SceneManager> output = new List<SceneManager>();

            foreach (SceneManager sceneManager in _sceneManagers)
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
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            List<T> output = new List<T>();

            foreach (SceneManager sceneManager in _sceneManagers)
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
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            return _sceneManagers.Count;
        }
        internal void RemoveSceneManager(SceneManager sceneManager)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (sceneManager is null)
            {
                throw new Exception("sceneManager cannot be null.");
            }

            if (sceneManager.Scene != this)
            {
                throw new Exception("sceneManager belongs to a different Scene.");
            }


            bool sceneManagerAdded = false;
            foreach (SceneManager addedSceneManager in _sceneManagers)
            {
                if (addedSceneManager == sceneManager)
                {
                    sceneManagerAdded = true;
                }
            }
            if (!sceneManagerAdded)
            {
                throw new Exception("sceneManager has already been removed.");
            }

            foreach (SceneManager removeQueSceneManager in _sceneManagerRemoveQue)
            {
                if (removeQueSceneManager == sceneManager)
                {
                    throw new Exception("sceneManager has already been removed.");
                }
            }

            _sceneManagerRemoveQue.Add(sceneManager);
        }
        internal void AddSceneManager(SceneManager sceneManager)
        {
            if (_destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }

            if (sceneManager is null)
            {
                throw new Exception("sceneManager cannot be null.");
            }

            if (sceneManager.Scene != this)
            {
                throw new Exception("sceneManager belongs to a different Scene.");
            }

            foreach (SceneManager addedSceneManager in _sceneManagers)
            {
                if (addedSceneManager == sceneManager)
                {
                    throw new Exception("sceneManager has already been added.");
                }
            }

            foreach (SceneManager addQueSceneManager in _sceneManagerAddQue)
            {
                if (addQueSceneManager == sceneManager)
                {
                    throw new Exception("sceneManager has already been added.");
                }
            }

            _sceneManagerAddQue.Add(sceneManager);
        }
        #endregion
        #region Overridables
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