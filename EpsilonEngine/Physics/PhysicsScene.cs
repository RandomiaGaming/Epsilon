using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class PhysicsScene : Scene
    {
        #region Variables
        private List<PhysicsObject> _physicsObjects = new List<PhysicsObject>();
        private PhysicsObject[] _physicsObjectCache = new PhysicsObject[0];
        private bool _physicsObjectCacheValid = true;

        private List<PhysicsLayer> _physicsLayers = new List<PhysicsLayer>();
        private PhysicsLayer[] _physicsLayerCache = new PhysicsLayer[0];
        private bool _physicsLayerCacheValid = true;
        #endregion
        #region Constructors
        public PhysicsScene(Game game, ushort width, ushort height) : base(game, width, height)
        {

        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.PhysicsScene()";
        }
        #endregion
        #region Methods
        public PhysicsLayer GetPhysicsLayer(int index)
        {
            if (index < 0 || index >= _physicsLayerCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _physicsLayerCache[index];
        }
        public List<PhysicsLayer> GetPhysicsLayers()
        {
            return new List<PhysicsLayer>(_physicsLayerCache);
        }
        public int GetPhysicsLayerCount()
        {
            return _physicsLayerCache.Length;
        }
        public PhysicsLayer GetPhysicsLayerUnsafe(int index)
        {
            return _physicsLayerCache[index];
        }

        public PhysicsObject GetPhysicsObject(int index)
        {
            if (index < 0 || index >= _physicsObjectCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _physicsObjectCache[index];
        }
        public PhysicsObject GetPhysicsObject(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(PhysicsObject)))
            {
                throw new Exception("type must be equal to PhysicsObject or be assignable from PhysicsObject.");
            }

            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    return physicsObject;
                }
            }

            return null;
        }
        public T GetPhysicsObject<T>() where T : PhysicsObject
        {
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)physicsObject;
                }
            }

            return null;
        }
        public List<PhysicsObject> GetPhysicsObjects()
        {
            return new List<PhysicsObject>(_physicsObjectCache);
        }
        public List<PhysicsObject> GetPhysicsObjects(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(PhysicsObject)))
            {
                throw new Exception("type must be equal to PhysicsObject or be assignable from PhysicsObject.");
            }

            List<PhysicsObject> output = new List<PhysicsObject>();

            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(physicsObject);
                }
            }

            return output;
        }
        public List<T> GetPhysicsObjects<T>() where T : PhysicsObject
        {
            List<T> output = new List<T>();

            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)physicsObject);
                }
            }

            return output;
        }
        public int GetPhysicsObjectCount()
        {
            return _physicsObjectCache.Length;
        }
        public PhysicsObject GetPhysicsObjectUnsafe(int index)
        {
            return _physicsObjectCache[index];
        }
        public PhysicsObject GetPhysicsObjectUnsafe(Type type)
        {
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    return physicsObject;
                }
            }

            return null;
        }
        public List<PhysicsObject> GetPhysicsObjectsUnsafe(Type type)
        {
            List<PhysicsObject> output = new List<PhysicsObject>();

            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(physicsObject);
                }
            }

            return output;
        }
        #endregion
        #region Internals
        private void ClearCache()
        {
            if (!_physicsObjectCacheValid)
            {
                _physicsObjectCache = _physicsObjects.ToArray();
                _physicsObjectCacheValid = true;
            }

            if (!_physicsLayerCacheValid)
            {
                _physicsLayerCache = _physicsLayers.ToArray();
                _physicsLayerCacheValid = true;
            }
        }
        internal void RemovePhysicsLayer(PhysicsLayer physicsLayer)
        {
            Game.RegisterForSingleRun(ClearCache);

            _physicsLayers.Remove(physicsLayer);

            _physicsLayerCacheValid = false;
        }
        internal void AddPhysicsLayer(PhysicsLayer physicsLayer)
        {
            Game.RegisterForSingleRun(ClearCache);

            _physicsLayers.Add(physicsLayer);

            _physicsLayerCacheValid = false;
        }
        internal void RemovePhysicsObject(PhysicsObject physicsObject)
        {
            Game.RegisterForSingleRun(ClearCache);

            _physicsObjects.Remove(physicsObject);

            _physicsObjectCacheValid = false;
        }
        internal void AddPhysicsObject(PhysicsObject physicsObject)
        {
            Game.RegisterForSingleRun(ClearCache);

            _physicsObjects.Add(physicsObject);

            _physicsObjectCacheValid = false;
        }
        #endregion
    }
}