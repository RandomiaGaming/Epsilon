using System;
using System.Reflection;
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
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        public Scene Scene { get; private set; } = null;

        public int PositionX = 0;
        public int PositionY = 0;
        public Point Position
        {
            get
            {
                return new Point(PositionX, PositionY);
            }
            set
            {
                PositionX = value.X;
                PositionY = value.Y;
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

            scene.AddGameObject(this);

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(GameObject))
            {
                Game.RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(GameObject))
            {
                Game.RegisterForRender(Render);
            }
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
            if (texture is null)
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
            Scene.DrawTextureWorldSpaceUnsafe(texture, PositionX + x, PositionY + y, r, g, b, a);
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
        private void ClearCache()
        {
            if (!_componentCacheValid)
            {
                _componentCache = _components.ToArray();
                _componentCacheValid = true;
            }
        }
        internal void RemoveComponent(Component component)
        {
            Game.RegisterForSingleRun(ClearCache);

            _components.Remove(component);

            _componentCacheValid = false;
        }
        internal void AddComponent(Component component)
        {
            Game.RegisterForSingleRun(ClearCache);

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