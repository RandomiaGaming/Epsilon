namespace EpsilonEngine
{
    public class Element
    {
        #region Variables
        private System.Collections.Generic.List<Element> _children = new System.Collections.Generic.List<Element>();
        private Element[] _childCache = new Element[0];
        private bool _childCacheValid = true;
        private double _localMinX = 0f;
        private double _localMinY = 0f;
        private double _localMaxX = 1f;
        private double _localMaxY = 1f;
        #endregion
        #region Properties
        public bool IsDestroyed { get; private set; } = false;
        public bool IsOrphan { get; private set; } = true;
        public Game Game { get; private set; } = null;
        public Canvas Canvas { get; private set; } = null;
        public Element Parent { get; private set; } = null;
        public double LocalMinX
        {
            get { return _localMinX; }
            set
            {
                _localMinX = value;
                RecalculateWorldX();
            }
        }
        public double LocalMinY
        {
            get { return _localMinY; }
            set
            {
                _localMinY = value;
                RecalculateWorldY();
            }
        }
        public double LocalMaxX
        {
            get { return _localMaxX; }
            set
            {
                _localMaxX = value;
                RecalculateWorldX();
            }
        }
        public double LocalMaxY
        {
            get { return _localMaxY; }
            set
            {
                _localMaxY = value;
                RecalculateWorldY();
            }
        }
        public Vector LocalMin
        {
            get { return new Vector(LocalMinX, LocalMinY); }
            set
            {
                LocalMinX = value.X;
                LocalMinY = value.Y;
            }
        }
        public Vector LocalMax
        {
            get { return new Vector(LocalMaxX, LocalMaxY); }
            set
            {
                LocalMaxX = value.X;
                LocalMaxY = value.Y;
            }
        }
        public Bounds LocalBounds
        {
            get { return new Bounds(LocalMinX, LocalMinY, LocalMaxX, LocalMaxY); }
            set
            {
                LocalMinX = value._minX;
                LocalMinY = value._minY;
                LocalMaxX = value._maxX;
                LocalMaxY = value._maxY;
            }
        }
        public double WorldMinX { get; private set; } = 0f;
        public double WorldMinY { get; private set; } = 0f;
        public double WorldMaxX { get; private set; } = 1f;
        public double WorldMaxY { get; private set; } = 1f;
        public Vector WorldMin
        {
            get { return new Vector(WorldMinX, WorldMinY); }
        }
        public Vector WorldMax
        {
            get { return new Vector(WorldMaxX, WorldMaxY); }
        }
        public Bounds WorldBounds
        {
            get { return new Bounds(WorldMinX, WorldMinY, WorldMaxX, WorldMaxY); }
        }
        public int ScreenMinX { get; private set; } = 0;
        public int ScreenMinY { get; private set; } = 0;
        public int ScreenMaxX { get; private set; } = 1920;
        public int ScreenMaxY { get; private set; } = 1080;
        public Point ScreenMin
        {
            get { return new Point(ScreenMinX, ScreenMinY); }
        }
        public Point ScreenMax
        {
            get { return new Point(ScreenMaxX, ScreenMaxY); }
        }
        public Rect ScreenRect
        {
            get { return new Rect(ScreenMinX, ScreenMinY, ScreenMaxX, ScreenMaxY); }
        }
        #endregion
        #region Constructors
        public Element(Canvas canvas)
        {
            if (canvas is null)
            {
                throw new System.Exception("canvas cannot be null.");
            }
            Canvas = canvas;
            Game = canvas.Game;
            canvas.AddElement(this);
            IsOrphan = true;
            Parent = null;
            System.Type thisType = GetType();
            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Element))
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, 0);
            }
            System.Reflection.MethodInfo renderMethod = thisType.GetMethod("Render", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Element))
            {
                Game.RenderPump.RegisterPumpEventUnsafe(Render);
            }
            RecalculateScreenX();
            RecalculateScreenY();
        }
        public Element(Canvas canvas, Element parent)
        {
            if (canvas is null)
            {
                throw new System.Exception("canvas cannot be null.");
            }
            Canvas = canvas;
            Game = canvas.Game;
            canvas.AddElement(this);
            if (parent is null)
            {
                IsOrphan = true;
                Parent = null;
            }
            else
            {
                IsOrphan = false;
                Parent = parent;
                Parent.AddChild(this);
            }
            System.Type thisType = GetType();
            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Element))
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, 0);
            }
            System.Reflection.MethodInfo renderMethod = thisType.GetMethod("Render", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Element))
            {
                Game.RenderPump.RegisterPumpEventUnsafe(Render);
            }
            RecalculateWorldX();
            RecalculateWorldY();
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Element()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            foreach (Element child in _childCache)
            {
                child.Destroy();
            }
            Canvas.RemoveElement(this);
            _childCache = null;
            _children = null;
            Canvas = null;
            Game = null;
            IsDestroyed = true;
        }
        public Element GetChild(int index)
        {
            if (index < 0 || index >= _childCache.Length)
            {
                throw new System.Exception("index was out of range.");
            }
            return _childCache[index];
        }
        public Element GetChild(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(Element)))
            {
                throw new System.Exception("type must be equal to Element or be assignable from Element.");
            }
            foreach (Element child in _childCache)
            {
                if (child.GetType().IsAssignableFrom(type))
                {
                    return child;
                }
            }
            return null;
        }
        public T GetElement<T>() where T : Element
        {
            foreach (Element child in _childCache)
            {
                if (child.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)child;
                }
            }
            return null;
        }
        public System.Collections.Generic.List<Element> GetChildren()
        {
            return new System.Collections.Generic.List<Element>(_childCache);
        }
        public System.Collections.Generic.List<Element> GetChildren(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(Element)))
            {
                throw new System.Exception("type must be equal to Element or be assignable from Element.");
            }
            System.Collections.Generic.List<Element> output = new System.Collections.Generic.List<Element>();
            foreach (Element child in _childCache)
            {
                if (child.GetType().IsAssignableFrom(type))
                {
                    output.Add(child);
                }
            }
            return output;
        }
        public System.Collections.Generic.List<T> GetChildren<T>() where T : Element
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();
            foreach (Element child in _childCache)
            {
                if (child.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)child);
                }
            }
            return output;
        }
        public int GetChildCount()
        {
            return _childCache.Length;
        }
        public Element GetChildUnsafe(int index)
        {
            return _childCache[index];
        }
        public Element GetChildUnsafe(System.Type type)
        {
            foreach (Element child in _childCache)
            {
                if (child.GetType().IsAssignableFrom(type))
                {
                    return child;
                }
            }
            return null;
        }
        public System.Collections.Generic.List<Element> GetChildrenUnsafe(System.Type type)
        {
            System.Collections.Generic.List<Element> output = new System.Collections.Generic.List<Element>();
            foreach (Element child in _childCache)
            {
                if (child.GetType().IsAssignableFrom(type))
                {
                    output.Add(child);
                }
            }
            return output;
        }
        #endregion
        #region Internals
        internal void ClearCache()
        {
            if (!_childCacheValid)
            {
                _childCache = _children.ToArray();
                _childCacheValid = true;
            }
        }
        internal void RemoveChild(Element child)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);
            _children.Remove(child);
            _childCacheValid = false;
        }
        internal void AddChild(Element child)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);
            _children.Add(child);
            _childCacheValid = false;
        }
        internal void RecalculateWorldX()
        {
            if (IsOrphan)
            {
                WorldMinX = LocalMinX;
                WorldMaxX = LocalMaxX;
            }
            else
            {
                WorldMinX = MathHelper.LinInterp(LocalMinX, Parent.WorldMinX, Parent.WorldMaxX);
                WorldMaxX = MathHelper.LinInterp(LocalMaxX, Parent.WorldMinX, Parent.WorldMaxX);
            }
            ScreenMinX = (int)(WorldMinX * Game.ViewportWidth);
            ScreenMaxX = (int)(WorldMaxX * Game.ViewportWidth);
            foreach (Element child in _childCache)
            {
                child.RecalculateWorldX();
            }
        }
        internal void RecalculateWorldY()
        {
            if (IsOrphan)
            {
                WorldMinY = LocalMinY;
                WorldMaxY = LocalMaxY;
            }
            else
            {
                WorldMinY = MathHelper.LinInterp(LocalMinY, Parent.WorldMinY, Parent.WorldMaxY);
                WorldMaxY = MathHelper.LinInterp(LocalMaxY, Parent.WorldMinY, Parent.WorldMaxY);
            }
            ScreenMinY = (int)(WorldMinY * Game.ViewportHeight);
            ScreenMaxY = (int)(WorldMaxY * Game.ViewportHeight);
            foreach (Element child in _childCache)
            {
                child.RecalculateWorldY();
            }
        }
        internal void RecalculateScreenX()
        {
            ScreenMinX = (int)(WorldMinX * Game.ViewportWidth);
            ScreenMaxX = (int)(WorldMaxX * Game.ViewportWidth);
            foreach (Element child in _childCache)
            {
                child.RecalculateScreenX();
            }
        }
        internal void RecalculateScreenY()
        {
            ScreenMinY = (int)(WorldMinY * Game.ViewportHeight);
            ScreenMaxY = (int)(WorldMaxY * Game.ViewportHeight);
            foreach (Element child in _childCache)
            {
                child.RecalculateScreenY();
            }
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