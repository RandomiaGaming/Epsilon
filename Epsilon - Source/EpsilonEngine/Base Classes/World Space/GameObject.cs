using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class GameObject
    {
        #region Variables
        private List<Component> _components = new List<Component>();
        private Component[] _componentCache = new Component[0];
        private bool _componentCacheValid = true;
        #endregion
        #region Properties
        public bool IsOrphan { get; private set; } = true;
        public bool IsDestroyed { get; private set; } = false;
        
        public Game Game { get; private set; } = null;
        public Scene Scene { get; private set; } = null;
        public GameObject Parent { get; private set; } = null;

        public int LocalPositionX { get; set; } = 0;
        public int LocalPositionY { get; set; } = 0;
        public Point LocalPosition
        {
            get
            {
                return new Point(LocalPositionX, LocalPositionY);
            }
            set
            {
                LocalPositionX = value.X;
                LocalPositionY = value.Y;
            }
        }

        public int WorldPositionX
        {
            get
            {
                if (IsOrphan)
                {
                    return LocalPositionX;
                }
                else
                {
                    return LocalPositionX + Parent.WorldPositionX;
                }
            }
            set
            {
                if (IsOrphan)
                {
                    LocalPositionX = value;
                }
                else
                {
                    LocalPositionX = value - Parent.WorldPositionX;
                }
            }
        }
        public int WorldPositionY
        {
            get
            {
                if (IsOrphan)
                {
                    return LocalPositionY;
                }
                else
                {
                    return LocalPositionY + Parent.WorldPositionY;
                }
            }
            set
            {
                if (IsOrphan)
                {
                    LocalPositionY = value;
                }
                else
                {
                    LocalPositionY = value - Parent.WorldPositionY;
                }
            }
        }
        public Point WorldPosition
        {
            get
            {
                if (IsOrphan)
                {
                    return LocalPosition;
                }
                else
                {
                    return LocalPosition + Parent.WorldPosition;
                }
            }
            set
            {
                if (IsOrphan)
                {
                    LocalPosition = value;
                }
                else
                {
                    LocalPosition = LocalPosition - Parent.WorldPosition;
                }
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

            Scene = scene;
            Game = scene.Game;

            Parent = null;
            IsOrphan = true;

            scene.AddGameObject(this);
        }
        public GameObject(Scene scene, GameObject parent)
        {
            if (scene is null)
            {
                throw new Exception("scene cannot be null.");
            }

            Scene = scene;
            Game = scene.Game;


            if(parent is null)
            {
                Parent = null;
                IsOrphan = true;
            }
            else
            {
                if (parent.Scene != scene)
                {
                    throw new Exception("parent must belong to the same scene.");
                }

                Parent = parent;
                IsOrphan = false;
            }

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
        public void DrawTextureLocalSpace(Texture texture, Point position, Color color)
        {
            if(texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            DrawTextureLocalSpaceUnsafe(texture, position.X, position.Y, color.R, color.B, color.B, color.A);
        }
        public void DrawTextureLocalSpace(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            DrawTextureLocalSpaceUnsafe(texture, x, y, r, g, b, a);
        }
        public void DrawTextureLocalSpaceUnsafe(Texture texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            Point worldPosition = WorldPosition;
            Scene.DrawTextureWorldSpaceUnsafe(texture, worldPosition.X + x, worldPosition.Y + y, r, g, b, a);
        }
        public void Destroy()
        {
            foreach (Component component in _componentCache)
            {
                component.Destroy();
            }

            Scene.RemoveGameObject(this);

            _componentCache = null;
            _components = null;
            Parent = null;
            Scene = null;
            Game = null;

            IsDestroyed = true;
        }
        public Component GetComponent(int index)
        {
            if (index < 0 || index >= _componentCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _componentCache[index];
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

            foreach (Component component in _componentCache)
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
            foreach (Component component in _componentCache)
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
            return new List<Component>(_componentCache);
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

            foreach (Component component in _componentCache)
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

            foreach (Component component in _componentCache)
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
            return _componentCache.Length;
        }
        public Component GetComponentUnsafe(int index)
        {
            return _componentCache[index];
        }
        public Component GetComponentUnsafe(Type type)
        {
            foreach (Component component in _componentCache)
            {
                if (component.GetType().IsAssignableFrom(type))
                {
                    return component;
                }
            }

            return null;
        }
        public List<Component> GetComponentsUnsafe(Type type)
        {
            List<Component> output = new List<Component>();

            foreach (Component component in _componentCache)
            {
                if (component.GetType().IsAssignableFrom(type))
                {
                    output.Add(component);
                }
            }

            return output;
        }
        #endregion
        #region Internals
        internal void InvokeUpdate()
        {
            if (!_componentCacheValid)
            {
                _componentCache = _components.ToArray();
                _componentCacheValid = true;
            }

           Update();

            foreach (Component component in _componentCache)
            {
                component.InvokeUpdate();
            }
        }
        internal void InvokeRender()
        {
            Render();

            foreach (Component component in _componentCache)
            {
                component.InvokeRender();
            }
        }
        internal void RemoveComponent(Component component)
        {
            _components.Remove(component);

            _componentCacheValid = false;
        }
        internal void AddComponent(Component component)
        {
            _components.Add(component);

            _componentCacheValid = false;
        }
        #endregion
        #region Overridables
        protected virtual void Update()
        {

        }
        protected virtual void Render()
        {

        }
        #endregion
    }
}