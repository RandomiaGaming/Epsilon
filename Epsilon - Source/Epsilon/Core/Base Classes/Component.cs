using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public abstract class Component
    {
        #region Variables
        private GameObject _gameObject = null;
        public string _name = "Unnamed Component";
        private bool _destroyed = false;
        #endregion
        #region Properties
        public Epsilon Engine
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Component has been destroyed.");
                }
                return _gameObject.Scene.Epsilon;
            }
        }
        public Stage Scene
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Component has been destroyed.");
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
        public string Name
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("Component has been destroyed.");
                }
                return _name;
            }
            set
            {
                if (_destroyed)
                {
                    throw new Exception("Component has been destroyed.");
                }
                _name = value;
            }
        }
        public bool Destroyed
        {
            get
            {
                return _destroyed;
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
            if (_destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            return $"EpsilonEngine.Component({_name})";
        }
        #endregion
        #region Methods
        public void Initialize()
        {
            if (_destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            initialize();
        }
        public void Destroy()
        {
            if (_destroyed)
            {
                return;
            }

            destroy();

            _destroyed = true;
        }
        public void Update()
        {
            if (_destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            update();
        }
        public void Render()
        {
            if (_destroyed)
            {
                throw new Exception("Component has been destroyed.");
            }
            render();
        }
        #endregion
        #region Overridables
        protected virtual void destroy()
        {

        }
        protected virtual void initialize()
        {

        }
        protected virtual void update()
        {

        }
        protected virtual void render()
        {

        }
        #endregion
    }
}