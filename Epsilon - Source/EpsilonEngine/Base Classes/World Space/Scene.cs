using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Scene
    {
        #region Variables
        private Microsoft.Xna.Framework.Graphics.RenderTarget2D _renderTarget = null;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch = null;

        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<SceneManager> _sceneManagers = new List<SceneManager>();

        private PumpEvent[] renderPump = new PumpEvent[0];
        private PumpEvent[] safeRenderPump = new PumpEvent[0];
        private bool renderPumpDirty = false;
        private bool renderPumpInUse = false;
        #endregion
        #region Properties
        public bool GameObjectsInitialized { get; private set; } = true;
        public Game Engine { get; private set; } = null;
        public Point CameraPosition { get; set; } = Point.Zero;
        public ushort ViewPortWidth { get; set; } = 1;
        public ushort ViewPortHeight { get; set; } = 1;
        public Point ViewPortSize
        {
            get
            {
                return new Point(ViewPortWidth, ViewPortHeight);
            }
        }
        public Rectangle ViewPortRect
        {
            get
            {
                return new Rectangle(CameraPosition, CameraPosition + ViewPortSize);
            }
        }
        public double AspectRatio
        {
            get
            {
                return ViewPortWidth / (double)ViewPortHeight;
            }
        }
        #endregion
        #region Constructors
        public Scene(Game engine, ushort viewPortWidth, ushort viewPortHeight)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }

            Engine = engine;

            if (viewPortWidth <= 0)
            {
                throw new Exception("viewPortWidth must be greater than 0.");
            }
            ViewPortWidth = viewPortWidth;

            if (viewPortHeight <= 0)
            {
                throw new Exception("viewPortHeight must be greater than 0.");
            }
            ViewPortHeight = viewPortHeight;

            _renderTarget = new Microsoft.Xna.Framework.Graphics.RenderTarget2D(Engine.GraphicsDevice, ViewPortWidth, ViewPortHeight, false, Microsoft.Xna.Framework.Graphics.SurfaceFormat.Color, Microsoft.Xna.Framework.Graphics.DepthFormat.None);

            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(Engine.GraphicsDevice);
            _spriteBatch.Name = "Scene SpriteBatch";
            _spriteBatch.Tag = null;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Scene()";
        }
        #endregion
        #region Methods
        public void DrawTextureWorldSpace(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            DrawTextureScreenSpace(texture, x - CameraPosition.X, y - CameraPosition.Y, r, g, b, a);
        }
        public void DrawTextureScreenSpace(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            y = ViewPortSize.Y - y;
            y = y - texture.Height;

            if (x + texture.Width < 0 || y + texture.Height < 0 || x > ViewPortSize.X || y > ViewPortSize.Y)
            {
                return;
            }

            _spriteBatch.Draw(texture.ToXNA(), new Microsoft.Xna.Framework.Rectangle(x,y, texture.Width, texture.Height), texture.Rect, new Microsoft.Xna.Framework.Color(r, g, b, a), 0, new Microsoft.Xna.Framework.Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }
        public void OnRemove()
        {
            foreach (SceneManager sceneManager in _sceneManagers)
            {
                sceneManager.OnRemove();
            }
            _sceneManagers = null;
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.OnRemove();
            }
            _gameObjects = null;
        }
        public void Initialize()
        {
            foreach (SceneManager sceneManager in _sceneManagers)
            {
                if (!sceneManager.Initialized)
                {
                    sceneManager.Initialize();
                }
            }
            foreach (GameObject gameObject in _gameObjects)
            {
                if (!gameObject.ChildrenInitialized)
                {
                    gameObject.Initialize();
                }
            }
            GameObjectsInitialized = true;
        }
        public Microsoft.Xna.Framework.Graphics.RenderTarget2D Render()
        {
            Engine.GraphicsDevice.SetRenderTarget(_renderTarget);

            Engine.GraphicsDevice.Clear(Engine.BackgroundColor.ToXNA());

            _spriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            int renderPumpLength = safeRenderPump.Length;
            for (int i = 0; i < renderPumpLength; i++)
            {
                safeRenderPump[i].Invoke();
            }

            if (renderPumpDirty)
            {
                safeRenderPump = renderPump;
                renderPumpDirty = false;
            }

            _spriteBatch.End();

            Engine.GraphicsDevice.SetRenderTarget(null);

            return _renderTarget;
        }
        public void RegisterForRender(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new Exception("updateable cannot be null.");
            }

            PumpEvent[] newRenderPump = new PumpEvent[renderPump.Length + 1];
            Array.Copy(renderPump, 0, newRenderPump, 0, renderPump.Length);
            newRenderPump[renderPump.Length] = pumpEvent;
            renderPump = newRenderPump;

            if (!renderPumpInUse)
            {
                safeRenderPump = newRenderPump;
            }
            else
            {
                renderPumpDirty = true;
            }
        }
        #endregion
        #region GameObject Management
        public GameObject GetGameObject(int index)
        {
            if (index < 0 || index >= _gameObjects.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _gameObjects[index];
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
        public List<GameObject> GetGameObjects()
        {
            return new List<GameObject>(_gameObjects);
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
            return _gameObjects.Count;
        }
        public void RemoveGameObject(GameObject gameObject)
        {
            if (gameObject is null)
            {
                throw new Exception("gameObject cannot be null.");
            }

            if (gameObject.Scene != this)
            {
                throw new Exception("gameObject belongs to a different Scene.");
            }

            bool gameObjectPresent = false;
            foreach (GameObject potentialMatch in _gameObjects)
            {
                if (gameObject == potentialMatch)
                {
                    gameObjectPresent = true;
                }
            }
            if (!gameObjectPresent)
            {
                throw new Exception("gameObject has already been removed.");
            }

            gameObject.OnRemove();

            _gameObjects.Remove(gameObject);
        }
        public void AddGameObject(GameObject gameObject)
        {
            if (gameObject is null)
            {
                throw new Exception("gameObject cannot be null.");
            }

            if (gameObject.Scene != this)
            {
                throw new Exception("gameObject belongs to a different Scene.");
            }

            /*foreach (GameObject potentialMatch in _gameObjects)
            {
                if (gameObject == potentialMatch)
                {
                    throw new Exception("gameObject has already been added.");
                }
            }*/

            _gameObjects.Add(gameObject);

            GameObjectsInitialized = false;
        }
        #endregion
        #region SceneManager Management
        public SceneManager GetSceneManager(int index)
        {
            if (index < 0 || index >= _sceneManagers.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _sceneManagers[index];
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
        public List<SceneManager> GetSceneManagers()
        {
            return new List<SceneManager>(_sceneManagers);
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
            return _sceneManagers.Count;
        }
        public void RemoveSceneManager(SceneManager sceneManager)
        {
            if (sceneManager is null)
            {
                throw new Exception("sceneManager cannot be null.");
            }

            if (sceneManager.Scene != this)
            {
                throw new Exception("sceneManager belongs to a different Scene.");
            }

            bool sceneManagerPresent = false;
            foreach (SceneManager potentialMatch in _sceneManagers)
            {
                if (sceneManager == potentialMatch)
                {
                    sceneManagerPresent = true;
                }
            }
            if (!sceneManagerPresent)
            {
                throw new Exception("sceneManager has already been removed.");
            }

            sceneManager.OnRemove();

            _sceneManagers.Remove(sceneManager);
        }
        public void AddSceneManager(SceneManager sceneManager)
        {
            if (sceneManager is null)
            {
                throw new Exception("sceneManager cannot be null.");
            }

            if (sceneManager.Scene != this)
            {
                throw new Exception("sceneManager belongs to a different Scene.");
            }

            foreach (SceneManager potentialMatch in _sceneManagers)
            {
                if (sceneManager == potentialMatch)
                {
                    throw new Exception("sceneManager has already been added.");
                }
            }

            _sceneManagers.Add(sceneManager);

            GameObjectsInitialized = false;
        }
        #endregion
    }
}