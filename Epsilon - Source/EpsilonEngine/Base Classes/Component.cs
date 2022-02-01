using System;

namespace EpsilonEngine
{
    public abstract class Component : Updateable
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
        #region Overridables
        internal virtual void Initialize()
        {

        }
        internal override void Update()
        {

        }
        internal virtual void Render()
        {

        }
        internal virtual void OnDestroy()
        {

        }
        #endregion
    }
}