using System;

namespace EpsilonEngine
{
    public abstract class SceneManager
    {
        #region Variables
        private Scene _scene = null;
        private bool _markedForDestruction = false;
        private bool _destroyed = false;
        #endregion
        #region Properties
        public Engine Engine
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("SceneManager has been destroyed.");
                }

                return _scene.Engine;
            }
        }
        public Scene Scene
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("SceneManager has been destroyed.");
                }

                return _scene;
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
        public SceneManager(Scene scene)
        {
            if (scene is null)
            {
                throw new Exception("scene cannot be null.");
            }

            _scene = scene;

            _scene.AddSceneManager(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.SceneManager()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            if (_destroyed)
            {
                throw new Exception("SceneManager has already been destroyed.");
            }

            if (_markedForDestruction)
            {
                throw new Exception("SceneManager has already been marked for destruction.");
            }

            _scene.RemoveSceneManager(this);

            _markedForDestruction = true;
        }
        internal void InvokeInitialize()
        {
            if (_destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }

            Initialize();
        }
        internal void InvokeUpdate()
        {
            if (_destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }

            Update();
        }
        internal void InvokeRender()
        {
            if (_destroyed)
            {
                throw new Exception("SceneManager has been destroyed.");
            }

            Render();
        }
        internal void InvokeOnDestroy()
        {
            if (_destroyed)
            {
                throw new Exception("SceneManager has already been destroyed.");
            }

            OnDestroy();

            _scene = null;

            _destroyed = true;
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