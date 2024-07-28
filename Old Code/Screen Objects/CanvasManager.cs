using System;
namespace EpsilonEngine
{
    public abstract class CanvasManager
    {
        public bool destroyed { get; private set; } = false;
        public Game game
        {
            get
            {
                if (!destroyed)
                {
                    throw new Exception("SceneManager has been destroyed.");
                }
                return _scene.game;
            }
        }
        public Scene scene
        {
            get
            {
                if (destroyed)
                {
                    throw new Exception("SceneManager has been destroyed.");
                }
                return _scene;
            }
        }
        private Scene _scene = null;
        public SceneManager(Scene scene)
        {
            if (scene is null)
            {
                throw new NullReferenceException();
            }
            _scene = scene;
            _scene.AddSceneManager(this);
        }
        public void CallDestroy()
        {
            if (destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }
            Destroy();
            _scene.RemoveSceneManager(this);
            destroyed = true;
        }
        public void CallInitialize()
        {
            if (destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }
            Initialize();
        }
        public void CallPrepare()
        {
            if (destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }
            Prepare();
        }
        public void CallUpdate()
        {
            if (destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }
            Update();
        }
        public void CallCleanup()
        {
            if (destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }
            Cleanup();
        }
        public RenderTexture CallRender()
        {
            if (destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }
            return Render();
        }
        protected virtual void Destroy()
        {

        }
        protected virtual void Initialize()
        {

        }
        protected virtual void Prepare()
        {

        }
        protected virtual void Update()
        {

        }
        protected virtual void Cleanup()
        {

        }
        protected virtual RenderTexture Render()
        {
            return new RenderTexture();
        }
    }
}