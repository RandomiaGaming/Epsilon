using System;

namespace EpsilonEngine
{
    public abstract class Updateable
    {
        #region Constructors
        public Updateable()
        {

        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Updateable()";
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