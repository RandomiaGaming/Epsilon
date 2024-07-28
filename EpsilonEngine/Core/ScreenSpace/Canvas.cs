namespace EpsilonEngine
{
    public class Canvas
    {
        #region Variables
        private System.Collections.Generic.List<Element> _elements = new System.Collections.Generic.List<Element>();
        private Element[] _elementCache = new Element[0];
        private bool _elementCacheValid = true;

        private System.Collections.Generic.List<CanvasBehavior> _canvasBehaviors = new System.Collections.Generic.List<CanvasBehavior>();
        private CanvasBehavior[] _canvasBehaviorCache = new CanvasBehavior[0];
        private bool _canvasBehaviorCacheValid = true;
        #endregion
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        #endregion
        #region Constructors
        public Canvas(Game game)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }

            Game = game;

            Game.AddCanvas(this);

            System.Type thisType = GetType();

            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Scene))
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, 0);
            }

            System.Reflection.MethodInfo renderMethod = thisType.GetMethod("Render", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Scene))
            {
                Game.RenderPump.RegisterPumpEventUnsafe(Render);
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Canvas()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            foreach(CanvasBehavior canvasBehavior in _canvasBehaviorCache)
            {
                canvasBehavior.Destroy();
            }

            foreach (Element element in _elementCache)
            {
                element.Destroy();
            }

            Game.RemoveCanvas(this);

            _canvasBehaviors = null;
            _canvasBehaviorCache = null;
            _elements = null;
            _elementCache = null;
            Game = null;

            IsDestroyed = true;
        }
        public CanvasBehavior GetCanvasBehavior(int index)
        {
            if (index < 0 || index >= _canvasBehaviorCache.Length)
            {
                throw new System.Exception("index was out of range.");
            }

            return _canvasBehaviorCache[index];
        }
        public CanvasBehavior GetCanvasBehavior(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(CanvasBehavior)))
            {
                throw new System.Exception("type must be equal to CanvasBehavior or be assignable from CanvasBehavior.");
            }

            foreach (CanvasBehavior canvasBehavior in _canvasBehaviorCache)
            {
                if (canvasBehavior.GetType().IsAssignableFrom(type))
                {
                    return canvasBehavior;
                }
            }

            return null;
        }
        public T GetCanvasBehavior<T>() where T : CanvasBehavior
        {
            foreach (CanvasBehavior canvasBehavior in _canvasBehaviorCache)
            {
                if (canvasBehavior.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)canvasBehavior;
                }
            }

            return null;
        }
        public System.Collections.Generic.List<CanvasBehavior> GetCanvasBehaviors()
        {
            return new System.Collections.Generic.List<CanvasBehavior>(_canvasBehaviorCache);
        }
        public System.Collections.Generic.List<CanvasBehavior> GetCanvasBehaviors(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(CanvasBehavior)))
            {
                throw new System.Exception("type must be equal to CanvasBehavior or be assignable from CanvasBehavior.");
            }

            System.Collections.Generic.List<CanvasBehavior> output = new System.Collections.Generic.List<CanvasBehavior>();

            foreach (CanvasBehavior canvasBehavior in _canvasBehaviorCache)
            {
                if (canvasBehavior.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvasBehavior);
                }
            }

            return output;
        }
        public System.Collections.Generic.List<T> GetCanvasBehaviors<T>() where T : CanvasBehavior
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();

            foreach (CanvasBehavior canvasBehavior in _canvasBehaviorCache)
            {
                if (canvasBehavior.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)canvasBehavior);
                }
            }

            return output;
        }
        public int GetCanvasBehaviorCount()
        {
            return _canvasBehaviorCache.Length;
        }
        public CanvasBehavior GetCanvasBehaviorUnsafe(int index)
        {
            return _canvasBehaviorCache[index];
        }
        public CanvasBehavior GetCanvasBehaviorUnsafe(System.Type type)
        {
            foreach (CanvasBehavior canvasBehavior in _canvasBehaviorCache)
            {
                if (canvasBehavior.GetType().IsAssignableFrom(type))
                {
                    return canvasBehavior;
                }
            }

            return null;
        }
        public System.Collections.Generic.List<CanvasBehavior> GetCanvasBehaviorsUnsafe(System.Type type)
        {
            System.Collections.Generic.List<CanvasBehavior> output = new System.Collections.Generic.List<CanvasBehavior>();

            foreach (CanvasBehavior canvasBehavior in _canvasBehaviorCache)
            {
                if (canvasBehavior.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvasBehavior);
                }
            }

            return output;
        }
        public Element GetElement(int index)
        {
            if (index < 0 || index >= _elementCache.Length)
            {
                throw new System.Exception("index was out of range.");
            }

            return _elementCache[index];
        }
        public Element GetElement(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Element)))
            {
                throw new System.Exception("type must be equal to Element or be assignable from Element.");
            }

            foreach (Element element in _elementCache)
            {
                if (element.GetType().IsAssignableFrom(type))
                {
                    return element;
                }
            }

            return null;
        }
        public T GetElement<T>() where T : Element
        {
            foreach (Element element in _elementCache)
            {
                if (element.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)element;
                }
            }

            return null;
        }
        public System.Collections.Generic.List<Element> GetElements()
        {
            return new System.Collections.Generic.List<Element>(_elementCache);
        }
        public System.Collections.Generic.List<Element> GetElements(System.Type type)
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

            foreach (Element element in _elementCache)
            {
                if (element.GetType().IsAssignableFrom(type))
                {
                    output.Add(element);
                }
            }

            return output;
        }
        public System.Collections.Generic.List<T> GetElements<T>() where T : Element
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();

            foreach (Element element in _elementCache)
            {
                if (element.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)element);
                }
            }

            return output;
        }
        public int GetElementCount()
        {
            return _elementCache.Length;
        }
        public Element GetElementUnsafe(int index)
        {
            return _elementCache[index];
        }
        public Element GetElementUnsafe(System.Type type)
        {
            foreach (Element element in _elementCache)
            {
                if (element.GetType().IsAssignableFrom(type))
                {
                    return element;
                }
            }

            return null;
        }
        public System.Collections.Generic.List<Element> GetElementsUnsafe(System.Type type)
        {
            System.Collections.Generic.List<Element> output = new System.Collections.Generic.List<Element>();

            foreach (Element element in _elementCache)
            {
                if (element.GetType().IsAssignableFrom(type))
                {
                    output.Add(element);
                }
            }

            return output;
        }
        #endregion
        #region Internals
        internal void OnScreenResize()
        {
            foreach(Element element in _elementCache)
            {
                element.RecalculateWorldX();
                element.RecalculateWorldY();
            }
        }
        internal void ClearCache()
        {
            if (!_canvasBehaviorCacheValid)
            {
                _canvasBehaviorCache = _canvasBehaviors.ToArray();
                _canvasBehaviorCacheValid = true;
            }

            if (!_elementCacheValid)
            {
                _elementCache = _elements.ToArray();
                _elementCacheValid = true;
            }
        }
        internal void RemoveCanvasBehavior(CanvasBehavior canvasBehavior)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);

            _canvasBehaviors.Remove(canvasBehavior);

            _canvasBehaviorCacheValid = false;
        }
        internal void AddCanvasBehavior(CanvasBehavior canvasBehavior)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);

            _canvasBehaviors.Add(canvasBehavior);

            _canvasBehaviorCacheValid = false;
        }
        internal void RemoveElement(Element element)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);

            _elements.Remove(element);

            _elementCacheValid = false;
        }
        internal void AddElement(Element element)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);

            _elements.Add(element);

            _elementCacheValid = false;
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