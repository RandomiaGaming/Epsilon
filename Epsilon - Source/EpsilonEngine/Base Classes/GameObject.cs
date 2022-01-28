using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpsilonEngine
{
    public class GameObject
    {
        #region Variables
        private Scene _scene = null;
        private bool _markedForDestruction = false;
        private bool _destroyed = false;
        private bool _renderring = false;
        private int _updateOrder = 0;
        private int _renderOrder = 0;

        private Point _position = new Point(0, 0);
        private List<Component> _components = new List<Component>();
        private List<Component> _componentAddQue = new List<Component>();
        private List<Component> _componentRemoveQue = new List<Component>();
        #endregion
        #region Properties
        public Engine Engine
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
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
                    throw new Exception("GameObject has been destroyed.");
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
        public Point Position
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
                }

                return _position;
            }
            set
            {
                if (_destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
                }

                _position = value;
            }
        }
        #endregion
        #region Constructors
        public GameObject(Scene scene)
        {
            if (scene is null)
            {
                throw new Exception("scene cannot be null.");
            }

            _scene = scene;

            _scene.AddGameObject(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.GameObject()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has already been destroyed.");
            }

            if (_markedForDestruction)
            {
                throw new Exception("GameObject has already been marked for destruction.");
            }

            foreach (Component component in _components)
            {
                component.Destroy();
            }

            _scene.RemoveGameObject(this);

            _markedForDestruction = true;
        }
        public void DrawTexture(Texture texture, Point localSpacePosition, Color color)
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            if (!_renderring)
            {
                throw new Exception("GameObject is not currently renderring.");
            }

            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }

            Scene.DrawTexture(texture, Position + localSpacePosition, color);
        }
        internal void InvokeInitialize()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            foreach (Component componentToAdd in _componentAddQue)
            {
                _components.Add(componentToAdd);
            }

            Initialize();

            foreach (Component addedComponent in _componentAddQue)
            {
                addedComponent.InvokeInitialize();
            }

            _componentAddQue = new List<Component>();
        }
        internal void InvokeUpdate()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            foreach (Component componentToRemove in _componentRemoveQue)
            {
                componentToRemove.InvokeOnDestroy();
            }

            foreach (Component removedComponent in _componentRemoveQue)
            {
                _components.Remove(removedComponent);
            }

            _componentRemoveQue = new List<Component>();

            foreach (Component componentToAdd in _componentAddQue)
            {
                _components.Add(componentToAdd);
            }

            foreach (Component addedComponent in _componentAddQue)
            {
                addedComponent.InvokeInitialize();
            }

            _componentAddQue = new List<Component>();

            Update();

            foreach (Component component in _components)
            {
                component.InvokeUpdate();
            }
        }
        internal void InvokeRender()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            _renderring = true;

            Render();

            foreach (Component component in _components)
            {
                component.InvokeRender();
            }

            _renderring = false;
        }
        internal void InvokeOnDestroy()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has alread been destroyed.");
            }

            foreach (Component componentToRemove in _componentRemoveQue)
            {
                componentToRemove.InvokeOnDestroy();
            }

            OnDestroy();

            _components = null;
            _componentAddQue = null;
            _componentRemoveQue = null;
            _scene = null;

            _destroyed = true;
        }
        #endregion
        #region Component Management
        public Component GetComponent(int index)
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            if (index < 0 || index >= _components.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _components[index];
        }
        public Component GetComponent(Type type)
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new Exception("type must be equal to Component or be assignable from Component.");
            }

            foreach (Component component in _components)
            {
                if (component.GetType().IsAssignableFrom(type))
                {
                    return component;
                }
            }

            return null;
        }
        public T GetComponent<T>() where T : Component
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            foreach (Component component in _components)
            {
                if (component.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)component;
                }
            }

            return null;
        }
        public List<Component> GetComponents()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            return new List<Component>(_components);
        }
        public List<Component> GetComponents(Type type)
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new Exception("type must be equal to Component or be assignable from Component.");
            }

            List<Component> output = new List<Component>();

            foreach (Component component in _components)
            {
                if (component.GetType().IsAssignableFrom(type))
                {
                    output.Add(component);
                }
            }

            return output;
        }
        public List<T> GetComponents<T>() where T : Component
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            List<T> output = new List<T>();

            foreach (Component component in _components)
            {
                if (component.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)component);
                }
            }

            return output;
        }
        public int GetComponentCount()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            return _components.Count;
        }
        internal void RemoveComponent(Component component)
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            if (component is null)
            {
                throw new Exception("gameObject cannot be null.");
            }

            if (component.GameObject != this)
            {
                throw new Exception("component belongs to a different GameObject.");
            }


            bool componentAdded = false;
            foreach (Component addedComponent in _components)
            {
                if (addedComponent == component)
                {
                    componentAdded = true;
                }
            }
            if (!componentAdded)
            {
                throw new Exception("component has already been removed.");
            }

            foreach (Component removeQueComponent in _componentRemoveQue)
            {
                if (removeQueComponent == component)
                {
                    throw new Exception("component has already been removed.");
                }
            }

            _componentRemoveQue.Add(component);
        }
        internal void AddComponent(Component component)
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            if (component is null)
            {
                throw new Exception("component cannot be null.");
            }

            if (component.GameObject != this)
            {
                throw new Exception("component belongs to a different GameObject.");
            }

            foreach (Component addedComponent in _components)
            {
                if (addedComponent == component)
                {
                    throw new Exception("component has already been added.");
                }
            }

            foreach (Component addQueComponent in _componentAddQue)
            {
                if (addQueComponent == component)
                {
                    throw new Exception("component has already been added.");
                }
            }

            _componentAddQue.Add(component);
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