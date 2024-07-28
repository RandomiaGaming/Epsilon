namespace EpsilonEngine
{
    public class GameObject
    {
        #region Public Variables
        public Scene Scene { get; private set; }
        public Game Game { get; private set; }
        public bool Destroyed { get; private set; }
        public bool OverridesUpdate { get; private set; }
        public int UpdatePriority { get; private set; }
        public bool OverridesRender { get; private set; }
        public int RenderPriority { get; private set; }
        public int PositionX
        {
            get { return _positionX; }
            set
            {
                _positionX = value;
                _position.X = value;
                MovePump.Invoke();
            }
        }
        public int PositionY
        {
            get { return _positionY; }
            set
            {
                _positionY = value;
                _position.Y = value;
                MovePump.Invoke();
            }
        }
        public Point Position
        {
            get { return _position; }
            set
            {
                _positionX = value.X;
                _positionY = value.Y;
                _position.X = value.X;
                _position.Y = value.Y;
                MovePump.Invoke();
            }
        }
        #endregion
        #region Internal Variables
        internal UnorderedPump MovePump = new UnorderedPump();
        #endregion
        #region Private Variables
        private System.Collections.Generic.List<Component> _components = new System.Collections.Generic.List<Component>();
        private Component[] _componentCache = new Component[0];
        private bool _componentValidateQued;
        private int _positionX;
        private int _positionY;
        private Point _position;
        private Microsoft.Xna.Framework.Vector2 _XNAReusableDrawPosition;
        private Microsoft.Xna.Framework.Color _XNAReusableDrawColor;
        #endregion
        #region Constructors
        public GameObject(Scene scene, int updatePriority, int renderPriority)
        {
            if (scene is null)
            {
                throw new System.Exception("scene cannot be null.");
            }
            Scene = scene;
            Game = Scene.Game;
            Scene.AddGameObject(this);
            System.Type thisType = GetType();
            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(GameObject))
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, updatePriority);
                OverridesUpdate = true;
            }
            System.Reflection.MethodInfo renderMethod = thisType.GetMethod("Render", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(GameObject))
            {
                Scene.RenderPump.RegisterPumpEventUnsafe(Render, renderPriority);
                OverridesRender = true;
            }
        }
        #endregion
        #region Public Methods
        public void DrawTextureLocalSpace(Texture texture, Point position, Color color)
        {
            if (!Scene.Rendering)
            {
                throw new System.Exception("cannot draw texture because scene is not rendering.");
            }
            if (texture is null)
            {
                throw new System.Exception("texture cannot be null.");
            }
            DrawTextureLocalSpaceUnsafe(texture._xnaTexture, position.X, position.Y, color.R, color.B, color.B, color.A);
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
            Destroyed = true;
        }
        public Component GetComponent(int index)
        {
            if (index < 0 || index >= _componentCache.Length)
            {
                throw new System.Exception("index was out of range.");
            }
            return _componentCache[index];
        }
        public Component GetComponent(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new System.Exception("type must be equal to Component or be assignable from Component.");
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
        public Component[] GetComponents()
        {
            return (Component[])_componentCache.Clone();
        }
        public Component[] GetComponents(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new System.Exception("type must be equal to Component or be assignable from Component.");
            }
            System.Collections.Generic.List<Component> output = new System.Collections.Generic.List<Component>();
            foreach (Component component in _componentCache)
            {
                if (component.GetType().IsAssignableFrom(type))
                {
                    output.Add(component);
                }
            }
            return output.ToArray();
        }
        public T[] GetComponents<T>() where T : Component
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();
            foreach (Component component in _componentCache)
            {
                if (component.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)component);
                }
            }
            return output.ToArray();
        }
        public int GetComponentCount()
        {
            return _componentCache.Length;
        }
        #endregion
        #region Internal Methods
        internal void DrawTextureLocalSpaceUnsafe(Microsoft.Xna.Framework.Graphics.Texture2D texture, int x, int y, byte r, byte g, byte b, byte a)
        {
            _XNAReusableDrawPosition.X = x - Scene.CameraPositionX;
            _XNAReusableDrawPosition.Y = Scene.RenderHeight - y + Scene.CameraPositionY - texture.Height;
            _XNAReusableDrawColor.R = r;
            _XNAReusableDrawColor.G = g;
            _XNAReusableDrawColor.B = b;
            _XNAReusableDrawColor.A = a;
            Scene.XNASpriteBatch.Draw(texture, _XNAReusableDrawPosition, _XNAReusableDrawColor);
        }
        internal void RemoveComponent(Component component)
        {
            _components.Remove(component);
            if (!_componentValidateQued)
            {
                Game.InitializationPump.RegisterPumpEventUnsafe(ValidateComponentCache);
                _componentValidateQued = true;
            }
        }
        internal void AddComponent(Component component)
        {
            _components.Add(component);
            if (!_componentValidateQued)
            {
                Game.InitializationPump.RegisterPumpEventUnsafe(ValidateComponentCache);
                _componentValidateQued = true;
            }
        }
        #endregion
        #region Private Methods
        private void ValidateComponentCache()
        {
            _componentCache = _components.ToArray();
            _componentValidateQued = false;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.GameObject()";
        }
        #endregion
        #region Overridable Methods
        protected virtual void Update()
        {
        }
        protected virtual void Render()
        {
        }
        #endregion
    }
}