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
        private Stage _stage = null;
        public string _name = "Unnamed StageObject";
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
        public Epsilon Epsilon
        {
            get
            {
                return _stage.Epsilon;
            }
        }
        public Stage Stage
        {
            get
            {
                return _stage;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        #endregion
        #region Constructors
        public StageObject(Stage stage)
        {
            if (stage is null)
            {
                throw new Exception("stage cannot be null.");
            }

            _stage = stage;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"Epsilon.StageObject({_name})";
        }
        #endregion
        #region Methods
        public void Update()
        {
            OnUpdate();
            foreach (Component component in _components)
            {
                component.Update();
            }
        }
        public void Render()
        {
            OnRender();
            foreach (Component component in _components)
            {
                component.Render();
            }
        }
        public void DrawTexture(Texture2D texture, Point offset, Color color)
        {
            Point drawPosition = offset + _position;
            Stage.DrawTexture(texture, drawPosition, color);
        }
        #endregion
        #region Component Management
        public Component GetComponent(int index)
        {
            if (index < 0 || index >= _components.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _components[index];
        }
        public Component GetComponent(Type type)
        {
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
            return new List<Component>(_components);
        }
        public List<Component> GetComponents(Type type)
        {
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
            return _components.Count;
        }
        public void RemoveComponent(Component component)
        {
            if (component is null)
            {
                throw new Exception("Component was null.");
            }

            if (component.StageObject != this)
            {
                throw new Exception("Component belongs on a different StageObject.");
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
        public void AddComponent(Component component)
        {
            if (component is null)
            {
                throw new Exception("Component was null.");
            }

            if (component.StageObject != this)
            {
                throw new Exception("Component belongs on a different StageObject.");
            }

            foreach (Component _component in _components)
            {
                if (_component == component)
                {
                    throw new Exception("Component was already added.");
                }
            }

            _components.Add(component);
        }
        #endregion
        #region Overridables
        protected virtual void OnUpdate()
        {

        }
        protected virtual void OnRender()
        {
        }
        #endregion
    }
}