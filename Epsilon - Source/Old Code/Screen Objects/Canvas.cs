using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Canvas
    {
        public bool destroyed { get; private set; } = false;
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<SceneManager> sceneManagers = new List<SceneManager>();
        public Game game
        {
            get
            {
                if (destroyed)
                {
                    throw new Exception("Scene has been destroyed.");
                }
                return _game;
            }
        }
        private Game _game = null;
        public Scene(Game game)
        {
            if (game is null)
            {
                throw new NullReferenceException();
            }
            _game = game;
            _game.AddScene(this);
        }
        public void Destroy()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            foreach (SceneManager sceneManager in sceneManagers)
            {
                sceneManager.CallDestroy();
            }
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Destroy();
            }
            _game.RemoveScene(this);
            destroyed = true;
        }
        public void Prepare()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            foreach (SceneManager sceneManager in sceneManagers)
            {
                sceneManager.CallPrepare();
            }
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Prepare();
            }
        }
        public void Update()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            foreach (SceneManager sceneManager in sceneManagers)
            {
                sceneManager.CallUpdate();
            }
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update();
            }
        }
        public void Cleanup()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            foreach (SceneManager sceneManager in sceneManagers)
            {
                sceneManager.CallCleanup();
            }
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Cleanup();
            }
        }
        public RenderTexture Render()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            RenderTexture output = new RenderTexture();
            foreach (SceneManager sceneManager in sceneManagers)
            {
                output.Merge(sceneManager.CallRender());
            }
            foreach (GameObject gameObject in gameObjects)
            {
                output.Merge(gameObject.Render());
            }
            output.Offset(cameraPosition * -1);
            return output;
        }
        public SceneManager GetSceneManager(int index)
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            if (index < 0 || index >= sceneManagers.Count)
            {
                throw new ArgumentException();
            }
            return sceneManagers[index];
        }
        public SceneManager GetSceneManager(Type type)
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            if (type is null)
            {
                throw new NullReferenceException();
            }
            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(type))
                {
                    return sceneManagers[i];
                }
            }
            return null;
        }
        public T GetSceneManager<T>() where T : SceneManager
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)sceneManagers[i];
                }
            }
            return null;
        }
        public List<SceneManager> GetSceneManagers()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            return new List<SceneManager>(sceneManagers);
        }
        public List<SceneManager> GetSceneManagers(Type type)
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            if (type is null)
            {
                throw new NullReferenceException();
            }
            if (!type.IsAssignableFrom(typeof(SceneManager)))
            {
                throw new ArgumentException();
            }
            List<SceneManager> output = new List<SceneManager>();
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(type))
                {
                    output.Add(sceneManagers[i]);
                }
            }
            return output;
        }
        public List<T> GetSceneManagers<T>() where T : SceneManager
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            List<T> output = new List<T>();
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)sceneManagers[i]);
                }
            }
            return output;
        }
        public int GetSceneManagerCount()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            return sceneManagers.Count;
        }
        public void RemoveSceneManager(SceneManager sceneManager)
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            if (sceneManager is null)
            {
                throw new Exception("SceneManager was null.");
            }
            if (sceneManager.scene != this)
            {
                throw new Exception("SceneManager belongs on a different Scene.");
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i] == sceneManager)
                {
                    sceneManagers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("SceneManager not found on this Scene.");
        }
        public void AddSceneManager(SceneManager sceneManager)
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            if (sceneManager is null)
            {
                throw new Exception("SceneManager was null.");
            }
            if (sceneManager.scene != this)
            {
                throw new Exception("SceneManager belongs to a different Scene.");
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i] == sceneManager)
                {
                    throw new Exception("SceneManager was already added.");
                }
            }
            sceneManagers.Add(sceneManager);
            sceneManager.CallInitialize();
        }
        public GameObject GetGameObject(int index)
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            if (index < 0 || index >= gameObjects.Count)
            {
                throw new ArgumentException();
            }
            return gameObjects[index];
        }
        public List<GameObject> GetGameObjects()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            return new List<GameObject>(gameObjects);
        }
        public int GetGameObjectCount()
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            return gameObjects.Count;
        }
        public void RemoveGameObject(GameObject gameObject)
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            if (gameObject is null)
            {
                throw new Exception("GameObject was null.");
            }
            if (gameObject.scene != this)
            {
                throw new Exception("GameObject belongs on a different Scene.");
            }
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] == gameObject)
                {
                    gameObjects.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("GameObject not found on this Scene.");
        }
        public void AddGameObject(GameObject gameObject)
        {
            if (destroyed)
            {
                throw new Exception("Scene has been destroyed.");
            }
            if (gameObject is null)
            {
                throw new Exception("GameObject was null.");
            }
            if (gameObject.scene != this)
            {
                throw new Exception("GameObject belongs to a different Scene.");
            }
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] == gameObject)
                {
                    throw new Exception("GameObject was already added.");
                }
            }
            gameObjects.Add(gameObject);
        }
    }
}