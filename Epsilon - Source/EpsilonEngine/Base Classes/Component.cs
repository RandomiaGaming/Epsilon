using System;

namespace EpsilonEngine
{
    public abstract class Component
    {
        #region Variables
        private GameObject _gameObject = null;
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
                    throw new Exception("Component has been destroyed.");
                }

                return _gameObject.Scene.Engine;
            }
        }
        public Scene Scene
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
                }

                return _gameObject.Scene;
            }
        }
        public GameObject GameObject
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Component has been destroyed.");
                }

                return _gameObject;
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
        public Component(GameObject gameObject)
        {
            if (gameObject is null)
            {
                throw new Exception("gameObject cannot be null.");
            }

            _gameObject = gameObject;

            _gameObject.AddComponent(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Component()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            if (_destroyed)
            {
                throw new Exception("Component has already been destroyed.");
            }

            if (_markedForDestruction)
            {
                throw new Exception("Component has already been marked for destruction.");
            }

            _gameObject.RemoveComponent(this);

            _markedForDestruction = true;
        }
        internal void InvokeInitialize()
        {
            if (_destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }

            Initialize();
        }
        internal void InvokeUpdate()
        {
            if (_destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }

            Update();
        }
        internal void InvokeRender()
        {
            if (_destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }

            Render();
        }
        internal void InvokeOnDestroy()
        {
            if (_destroyed)
            {
                throw new Exception("Component has already been destroyed.");
            }

            OnDestroy();

            _gameObject = null;

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