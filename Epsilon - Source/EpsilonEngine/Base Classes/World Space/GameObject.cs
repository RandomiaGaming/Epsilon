using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class GameObject
    {
        #region Variables
        private List<Component> _components = new List<Component>();
        private bool isOrphan = true;
        #endregion
        #region Properties
        public bool ChildrenInitialized { get; private set; } = true;
        public Game Engine { get; private set; } = null;
        public Scene Scene { get; private set; } = null;
        public Point LocalPosition { get; set; } = Point.Zero;
        public Point Position
        {
            get
            {
                if (isOrphan)
                {
                    return LocalPosition;
                }
                int x = LocalPosition.X;
                int y = LocalPosition.Y;
                GameObject parent = Parent;
                while (parent is not null)
                {
                    x += parent.LocalPosition.X;
                    y += parent.LocalPosition.Y;
                    parent = parent.Parent;
                }
                return new Point(x, y);
            }
            set
            {
                if (isOrphan)
                {
                    LocalPosition = value;
                }
                int x = value.X;
                int y = value.Y;
                GameObject parent = Parent;
                while (parent is not null)
                {
                    x -= parent.LocalPosition.X;
                    y -= parent.LocalPosition.Y;
                    parent = parent.Parent;
                }
                LocalPosition = new Point(x, y);
            }
        }
        public GameObject Parent = null;
        #endregion
        #region Constructors
        public GameObject(Scene scene)
        {
            if (scene is null)
            {
                throw new Exception("scene cannot be null.");
            }

            Scene = scene;
            Engine = scene.Engine;

            scene.AddGameObject(this);
        }
        public GameObject(Scene scene, GameObject parent)
        {
            if (scene is null)
            {
                throw new Exception("scene cannot be null.");
            }

            Scene = scene;
            Engine = scene.Engine;

            if (parent is not null && parent.Scene != scene)
            {
                throw new Exception("parent must belong to the same scene.");
            }

            Parent = parent;

            scene.AddGameObject(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.GameObject()";
        }
        #endregion
        #region Methods
        public void DrawTextureLocalSpace(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            Scene.DrawTextureWorldSpace(texture, Position.X + x, Position.Y + y, r, g, b, a);
        }
        public void OnRemove()
        {
            foreach (Component component in _components)
            {
                component.InvokeOnRemove();
            }

            _components = null;
        }
        public void Initialize()
        {
            foreach (Component component in _components)
            {
                if (!component.Initialized)
                {
                    component.InvokeInitialize();
                }
            }
            ChildrenInitialized = true;
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
                throw new Exception("gameObject cannot be null.");
            }

            if (component.GameObject != this)
            {
                throw new Exception("component belongs to a different GameObject.");
            }

            bool componentPresent = false;
            foreach (Component potentialMatch in _components)
            {
                if (potentialMatch == component)
                {
                    componentPresent = true;
                }
            }
            if (!componentPresent)
            {
                throw new Exception("component has already been removed.");
            }

            component.InvokeOnRemove();

            _components.Remove(component);
        }
        public void AddComponent(Component component)
        {
            if (component is null)
            {
                throw new Exception("component cannot be null.");
            }

            if (component.GameObject != this)
            {
                throw new Exception("component belongs to a different GameObject.");
            }

            foreach (Component potentialMatch in _components)
            {
                if (component == potentialMatch)
                {
                    throw new Exception("component has already been added.");
                }
            }

            _components.Add(component);

            ChildrenInitialized = false;
        }
        #endregion
    }
}