using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public class StageObject
    {
        #region Variables
        private Point _position = new Point(0, 0);
        private List<Component> _components = new List<Component>();
        private Stage _scene = null;
        public string _name = "Unnamed Scene";
        private bool _destroyed = false;
        #endregion
        #region Properties
        public Point Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }
        public Epsilon Engine
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
                }
                return _scene.Epsilon;
            }
        }
        public Stage Scene
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
        public string Name
        {
            get
            {
                if (_destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
                }
                return _name;
            }
            set
            {
                if (_destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
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
        public StageObject(Stage scene)
        {
            if (scene is null)
            {
                throw new Exception("scene cannot be null.");
            }

            _scene = scene;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            return $"EpsilonEngine.GameObject({_name})";
        }
        #endregion
        #region Methods
        #region Basic Methods
        public void Initialize()
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
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

            foreach (Component component in _components)
            {
                component.Destroy();
            }

            _destroyed = true;
        }
        public void Update()
        {
            update();
            foreach (Component component in _components)
            {
                component.Update();
            }
        }
        public List<DrawInstruction> Render()
        {
            List<DrawInstruction> output = render();
            foreach (Component component in _components)
            {
                output.AddRange(component.Render());
            }
            return output;
        }
        #endregion
        #region Component Methods
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
        #region Internal Methods
        internal void RemoveComponent(Component component)
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            if (component is null)
            {
                throw new Exception("Component was null.");
            }

            if (component.GameObject != this)
            {
                throw new Exception("Component belongs on a different GameObject.");
            }

            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] == component)
                {
                    _components.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Component not found.");
        }
        internal void AddComponent(Component component)
        {
            if (_destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }

            if (component is null)
            {
                throw new Exception("Component was null.");
            }

            if (component.GameObject != this)
            {
                throw new Exception("Component belongs on a different GameObject.");
            }

            foreach (Component _component in _components)
            {
                if (_component == component)
                {
                    throw new Exception("Component was already added.");
                }
            }

            _components.Add(component);

            component.Initialize();
        }
        #endregion
        #endregion
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
        protected virtual List<DrawInstruction> render()
        {
            return new List<DrawInstruction>();
        }
        #endregion
    }
}