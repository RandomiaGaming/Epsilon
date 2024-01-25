using System;
using System.Reflection;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Canvas
    {
        #region Variables
        private List<Element> _elements = new List<Element>();
        private Element[] _elementCache = new Element[0];
        private bool _elementCacheValid = true;

        private List<CanvasBehavior> _canvasBehaviors = new List<CanvasBehavior>();
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
                throw new Exception("game cannot be null.");
            }

            Game = game;

            Game.AddCanvas(this);

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Scene))
            {
                Game.RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Scene))
            {
                Game.RegisterForRender(Render);
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
                throw new Exception("index was out of range.");
            }

            return _canvasBehaviorCache[index];
        }
        public CanvasBehavior GetCanvasBehavior(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(CanvasBehavior)))
            {
                throw new Exception("type must be equal to CanvasBehavior or be assignable from CanvasBehavior.");
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
        public List<CanvasBehavior> GetCanvasBehaviors()
        {
            return new List<CanvasBehavior>(_canvasBehaviorCache);
        }
        public List<CanvasBehavior> GetCanvasBehaviors(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(CanvasBehavior)))
            {
                throw new Exception("type must be equal to CanvasBehavior or be assignable from CanvasBehavior.");
            }

            List<CanvasBehavior> output = new List<CanvasBehavior>();

            foreach (CanvasBehavior canvasBehavior in _canvasBehaviorCache)
            {
                if (canvasBehavior.GetType().IsAssignableFrom(type))
                {
                    output.Add(canvasBehavior);
                }
            }

            return output;
        }
        public List<T> GetCanvasBehaviors<T>() where T : CanvasBehavior
        {
            List<T> output = new List<T>();

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
        public CanvasBehavior GetCanvasBehaviorUnsafe(Type type)
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
        public List<CanvasBehavior> GetCanvasBehaviorsUnsafe(Type type)
        {
            List<CanvasBehavior> output = new List<CanvasBehavior>();

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
                throw new Exception("index was out of range.");
            }

            return _elementCache[index];
        }
        public Element GetElement(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(Element)))
            {
                throw new Exception("type must be equal to Element or be assignable from Element.");
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
        public List<Element> GetElements()
        {
            return new List<Element>(_elementCache);
        }
        public List<Element> GetElements(Type type)
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

            foreach (Element element in _elementCache)
            {
                if (element.GetType().IsAssignableFrom(type))
                {
                    output.Add(element);
                }
            }

            return output;
        }
        public List<T> GetElements<T>() where T : Element
        {
            List<T> output = new List<T>();

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
        public Element GetElementUnsafe(Type type)
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
        public List<Element> GetElementsUnsafe(Type type)
        {
            List<Element> output = new List<Element>();

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
            Game.RegisterForSingleRun(ClearCache);

            _canvasBehaviors.Remove(canvasBehavior);

            _canvasBehaviorCacheValid = false;
        }
        internal void AddCanvasBehavior(CanvasBehavior canvasBehavior)
        {
            Game.RegisterForSingleRun(ClearCache);

            _canvasBehaviors.Add(canvasBehavior);

            _canvasBehaviorCacheValid = false;
        }
        internal void RemoveElement(Element element)
        {
            Game.RegisterForSingleRun(ClearCache);

            _elements.Remove(element);

            _elementCacheValid = false;
        }
        internal void AddElement(Element element)
        {
            Game.RegisterForSingleRun(ClearCache);

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