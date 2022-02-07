using System;
using System.Reflection;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Element
    {
        #region Variables
        private List<Behavior> _behaviors = new List<Behavior>();
        private Behavior[] _behaviorCache = new Behavior[0];
        private bool _behaviorCacheValid = true;

        private List<Element> _children = new List<Element>();
        private Element[] _childCache = new Element[0];
        private bool _childCacheValid = true;
        #endregion
        #region Properties
        public bool IsDestroyed { get; private set; } = false;
        public bool IsOrphan { get; private set; } = true;

        public Game Game { get; private set; } = null;
        public Canvas Canvas { get; private set; } = null;
        public Element Parent { get; private set; } = null;

        public float LocalMinX { get; set; } = 0;
        public float LocalMinY { get; set; } = 0;
        public float LocalMaxX { get; set; } = 1;
        public float LocalMaxY { get; set; } = 1;
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

        public float MinX
        {
            get
            {
                if (IsOrphan)
                {
                    return LocalMinX;
                }
                else
                {
                    return MathHelper.Lerp(LocalMinX, Parent.MinX, Parent.MaxX);
                }
            }
        }
        public float MinY
        {
            get
            {
                if (IsOrphan)
                {
                    return LocalMinY;
                }
                else
                {
                    return MathHelper.Lerp(LocalMinY, Parent.MinY, Parent.MaxY);
                }
            }
        }
        public float MaxX
        {
            get
            {
                if (IsOrphan)
                {
                    return LocalMaxX;
                }
                else
                {
                    return MathHelper.Lerp(LocalMaxX, Parent.MinX, Parent.MaxX);
                }
            }
        }
        public float MaxY
        {
            get
            {
                if (IsOrphan)
                {
                    return LocalMaxY;
                }
                else
                {
                    return MathHelper.Lerp(LocalMaxY, Parent.MinY, Parent.MaxY);
                }
            }
        }
        public Vector Min
        {
            get
            {
                return new Vector(MinX, MinY);
            }
        }
        public Vector Max
        {
            get
            {
                return new Vector(MaxX, MaxY);
            }
        }
        public Bounds Bounds
        {
            get
            {
                return new Bounds(MinX, MinY, MaxX, MaxY);
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
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Element()";
        }
        #endregion
        #region Methods
        public void DrawTextureLocalSpace(Texture texture, Bounds bounds, Color color)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            DrawTextureLocalSpaceUnsafe(texture, bounds.MinX, bounds.MinY, bounds.MaxX, bounds.MaxY, color.R, color.B, color.B, color.A);
        }
        public void DrawTextureLocalSpace(Texture texture, float minX, float minY, float maxX, float maxY, byte r, byte g, byte b, byte a)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            if (maxX < minX)
            {
                throw new Exception("maxX must be greater than minX.");
            }
            if (maxY < minY)
            {
                throw new Exception("maxY must be greater than minY.");
            }
            DrawTextureLocalSpaceUnsafe(texture, minX, minY, maxX, maxY, r, g, b, a);
        }
        public void DrawTextureLocalSpaceUnsafe(Texture texture, float minX, float minY, float maxX, float maxY, byte r, byte g, byte b, byte a)
        {
            Canvas.DrawTextureScreenSpaceUnsafe(texture, MathHelper.Lerp(minX, MinX, MaxX), MathHelper.Lerp(minY, MinY, MaxY), MathHelper.Lerp(maxX, MinX, MaxX), MathHelper.Lerp(maxY, MinY, MaxY), r, g, b, a);
        }
        public void Destroy()
        {
            foreach (Element child in _childCache)
            {
                child.Destroy();
            }

            foreach (Behavior behavior in _behaviorCache)
            {
                behavior.Destroy();
            }

            Canvas.RemoveElement(this);

            _childCache = null;
            _children = null;
            _behaviorCache = null;
            _behaviors = null;
            Canvas = null;
            Game = null;

            IsDestroyed = true;
        }
        public Behavior GetBehavior(int index)
        {
            if (index < 0 || index >= _behaviorCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _behaviorCache[index];
        }
        public Behavior GetBehavior(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Behavior)))
            {
                throw new Exception("type must be equal to Behavior or be assignable from Behavior.");
            }

            foreach (Behavior behavior in _behaviorCache)
            {
                if (behavior.GetType().IsAssignableFrom(type))
                {
                    return behavior;
                }
            }

            return null;
        }
        public T GetBehavior<T>() where T : Behavior
        {
            foreach (Behavior behavior in _behaviorCache)
            {
                if (behavior.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)behavior;
                }
            }

            return null;
        }
        public List<Behavior> GetBehaviors()
        {
            return new List<Behavior>(_behaviorCache);
        }
        public List<Behavior> GetBehaviors(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Behavior)))
            {
                throw new Exception("type must be equal to Behavior or be assignable from Behavior.");
            }

            List<Behavior> output = new List<Behavior>();

            foreach (Behavior behavior in _behaviorCache)
            {
                if (behavior.GetType().IsAssignableFrom(type))
                {
                    output.Add(behavior);
                }
            }

            return output;
        }
        public List<T> GetBehaviors<T>() where T : Behavior
        {
            List<T> output = new List<T>();

            foreach (Behavior behavior in _behaviorCache)
            {
                if (behavior.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)behavior);
                }
            }

            return output;
        }
        public int GetBehaviorCount()
        {
            return _behaviorCache.Length;
        }
        public Behavior GetBehaviorUnsafe(int index)
        {
            return _behaviorCache[index];
        }
        public Behavior GetBehaviorUnsafe(Type type)
        {
            foreach (Behavior behavior in _behaviorCache)
            {
                if (behavior.GetType().IsAssignableFrom(type))
                {
                    return behavior;
                }
            }

            return null;
        }
        public List<Behavior> GetBehaviorsUnsafe(Type type)
        {
            List<Behavior> output = new List<Behavior>();

            foreach (Behavior behavior in _behaviorCache)
            {
                if (behavior.GetType().IsAssignableFrom(type))
                {
                    output.Add(behavior);
                }
            }

            return output;
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
            if (!_behaviorCacheValid)
            {
                _behaviorCache = _behaviors.ToArray();
                _behaviorCacheValid = true;
            }

            if (!_childCacheValid)
            {
                _childCache = _children.ToArray();
                _childCacheValid = true;
            }
        }
        internal void RemoveBehavior(Behavior behavior)
        {
            Game.RegisterForSingleRun(ClearCache);

            _behaviors.Remove(behavior);

            _behaviorCacheValid = false;
        }
        internal void AddBehavior(Behavior behavior)
        {
            Game.RegisterForSingleRun(ClearCache);

            _behaviors.Add(behavior);

            _behaviorCacheValid = false;
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