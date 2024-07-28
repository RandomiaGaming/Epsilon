using System;
namespace EpsilonEngine
{
    public abstract class ElementBehavior
    {
        public bool destroyed { get; private set; } = false;
        public Game game
        {
            get
            {
                if (!destroyed)
                {
                    throw new Exception("Component has been destroyed.");
                }
                return _gameObject.game;
            }
        }
        public Scene scene
        {
            get
            {
                if (destroyed)
                {
                    throw new Exception("Component has been destroyed.");
                }
                return _gameObject.scene;
            }
        }
        public GameObject gameObject
        {
            get
            {
                if (destroyed)
                {
                    throw new Exception("Component has been destroyed.");
                }
                return _gameObject;
            }
        }
        private GameObject _gameObject = null;
        public ElementBehavior(Element element)
        {
            if (gameObject is null)
            {
                throw new NullReferenceException();
            }
            _gameObject = gameObject;
            _gameObject.AddComponent(this);
        }
        public void Destroy()
        {
            if (destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            onDestroy();
            _gameObject.RemoveComponent(this);
            destroyed = true;
        }
        internal void Initialize()
        {
            if (destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            initialize();
        }
        internal void CallPrepare()
        {
            if (destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            Prepare();
        }
        internal void CallUpdate()
        {
            if (destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            update();
        }
        internal void Cleanup()
        {
            if (destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            cleanup();
        }
        internal void Render()
        {
            if (destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            render();
        }
        protected virtual void onDestroy()
        {

        }
        protected virtual void initialize()
        {

        }
        protected virtual void prepare()
        {

        }
        protected virtual void update()
        {

        }
        protected virtual void cleanup()
        {

        }
        protected virtual void render()
        {

        }
    }
}