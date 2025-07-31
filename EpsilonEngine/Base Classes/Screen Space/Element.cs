using System;
using System.Reflection;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Element
    {
        #region Variables
        private List<Element> _children = new List<Element>();
        private Element[] _childCache = new Element[0];
        private bool _childCacheValid = true;

        private float _localMinX = 0f;
        private float _localMinY = 0f;
        private float _localMaxX = 1f;
        private float _localMaxY = 1f;
        #endregion
        #region Properties
        public bool IsDestroyed { get; private set; } = false;
        public bool IsOrphan { get; private set; } = true;

        public Game Game { get; private set; } = null;
        public Canvas Canvas { get; private set; } = null;
        public Element Parent { get; private set; } = null;

        public float LocalMinX
        {
            get
            {
                return _localMinX;
            }
            set
            {
                _localMinX = value;
                RecalculateWorldX();
            }
        }
        public float LocalMinY
        {
            get
            {
                return _localMinY;
            }
            set
            {
                _localMinY = value;
                RecalculateWorldY();
            }
        }
        public float LocalMaxX
        {
            get
            {
                return _localMaxX;
            }
            set
            {
                _localMaxX = value;
                RecalculateWorldX();
            }
        }
        public float LocalMaxY
        {
            get
            {
                return _localMaxY;
            }
            set
            {
                _localMaxY = value;
                RecalculateWorldY();
            }
        }
        public Vector LocalMin
        {
            get
            {
                return new Vector(LocalMinX, LocalMinY);
            }
            set
            {
                LocalMinX = value.X;
                LocalMinY = value.Y;
            }
        }
        public Vector LocalMax
        {
            get
            {
                return new Vector(LocalMaxX, LocalMaxY);
            }
            set
            {
                LocalMaxX = value.X;
                LocalMaxY = value.Y;
            }
        }
        public Bounds LocalBounds
        {
            get
            {
                return new Bounds(LocalMinX, LocalMinY, LocalMaxX, LocalMaxY);
            }
            set
            {
                LocalMinX = value.MinX;
                LocalMinY = value.MinY;
                LocalMaxX = value.MaxX;
                LocalMaxY = value.MaxY;
            }
        }

        public float WorldMinX { get; private set; } = 0f;
        public float WorldMinY { get; private set; } = 0f;
        public float WorldMaxX { get; private set; } = 1f;
        public float WorldMaxY { get; private set; } = 1f;
        public Vector WorldMin
        {
            get
            {
                return new Vector(WorldMinX, WorldMinY);
            }
        }
        public Vector WorldMax
        {
            get
            {
                return new Vector(WorldMaxX, WorldMaxY);
            }
        }
        public Bounds WorldBounds
        {
            get
            {
                return new Bounds(WorldMinX, WorldMinY, WorldMaxX, WorldMaxY);
            }
        }

        public int ScreenMinX { get; private set; } = 0;
        public int ScreenMinY { get; private set; } = 0;
        public int ScreenMaxX { get; private set; } = 1920;
        public int ScreenMaxY { get; private set; } = 1080;
        public Point ScreenMin
        {
            get
            {
                return new Point(ScreenMinX, ScreenMinY);
            }
        }
        public Point ScreenMax
        {
            get
            {
                return new Point(ScreenMaxX, ScreenMaxY);
            }
        }
        public Rectangle ScreenRect
        {
            get
            {
                return new Rectangle(ScreenMinX, ScreenMinY, ScreenMaxX, ScreenMaxY);
            }
        }
        #endregion
        #region Constructors
        public Element(Canvas canvas)
        {
            if (canvas is null)
            {
                throw new Exception("canvas cannot be null.");
            }

            Canvas = canvas;
            Game = canvas.Game;

            canvas.AddElement(this);

            IsOrphan = true;
            Parent = null;

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Element))
            {
                Game.RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Element))
            {
                Game.RegisterForRender(Render);
            }

            RecalculateScreenX();
            RecalculateScreenY();
        }
        public Element(Canvas canvas, Element parent)
        {
            if (canvas is null)
            {
                throw new Exception("canvas cannot be null.");
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

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Element))
            {
                Game.RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Element))
            {
                Game.RegisterForRender(Render);
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
                throw new Exception("index was out of range.");
            }

            return _childCache[index];
        }
        public Element GetChild(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Element)))
            {
                throw new Exception("type must be equal to Element or be assignable from Element.");
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
        public List<Element> GetChildren()
        {
            return new List<Element>(_childCache);
        }
        public List<Element> GetChildren(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Element)))
            {
                throw new Exception("type must be equal to Element or be assignable from Element.");
            }

            List<Element> output = new List<Element>();

            foreach (Element child in _childCache)
            {
                if (child.GetType().IsAssignableFrom(type))
                {
                    output.Add(child);
                }
            }

            return output;
        }
        public List<T> GetChildren<T>() where T : Element
        {
            List<T> output = new List<T>();

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
        public Element GetChildUnsafe(Type type)
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
        public List<Element> GetChildrenUnsafe(Type type)
        {
            List<Element> output = new List<Element>();

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
            Game.RegisterForSingleRun(ClearCache);

            _children.Remove(child);

            _childCacheValid = false;
        }
        internal void AddChild(Element child)
        {
            Game.RegisterForSingleRun(ClearCache);

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
                WorldMinX = MathHelper.Lerp(LocalMinX, Parent.WorldMinX, Parent.WorldMaxX);
                WorldMaxX = MathHelper.Lerp(LocalMaxX, Parent.WorldMinX, Parent.WorldMaxX);
            }

            ScreenMinX = (int)(WorldMinX * Game.Width);
            ScreenMaxX = (int)(WorldMaxX * Game.Width);

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
                WorldMinY = MathHelper.Lerp(LocalMinY, Parent.WorldMinY, Parent.WorldMaxY);
                WorldMaxY = MathHelper.Lerp(LocalMaxY, Parent.WorldMinY, Parent.WorldMaxY);
            }

            ScreenMinY = (int)(WorldMinY * Game.Height);
            ScreenMaxY = (int)(WorldMaxY * Game.Height);

            foreach (Element child in _childCache)
            {
                child.RecalculateWorldY();
            }
        }
        internal void RecalculateScreenX()
        {
            ScreenMinX = (int)(WorldMinX * Game.Width);
            ScreenMaxX = (int)(WorldMaxX * Game.Width);

            foreach (Element child in _childCache)
            {
                child.RecalculateScreenX();
            }
        }
        internal void RecalculateScreenY()
        {
            ScreenMinY = (int)(WorldMinY * Game.Height);
            ScreenMaxY = (int)(WorldMaxY * Game.Height);

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