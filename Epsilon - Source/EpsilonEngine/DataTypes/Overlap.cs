using System;
namespace EpsilonEngine
{
    public sealed class Overlap
    {
        #region Variables
        private Collider _otherCollider = null;
        private Collider _thisCollider = null;
        #endregion
        #region Properties
        public Collider ThisCollider
        {
            get
            {
                return _thisCollider;
            }
        }
        public GameObject ThisGameObject
        {
            get
            {
                return _thisCollider.GameObject;
            }
        }
        public Collider OtherCollider
        {
            get
            {
                return _otherCollider;
            }
        }
        public GameObject OtherGameObject
        {
            get
            {
                return _otherCollider.GameObject;
            }
        }
        #endregion
        #region Contructors
        public Overlap(Collider thisCollider, Collider otherCollider)
        {
            if (thisCollider is null)
            {
                throw new Exception("thisCollider cannot be null.");
            }
            _thisCollider = thisCollider;

            if (otherCollider is null)
            {
                throw new Exception("otherCollider cannot be null.");
            }
            _otherCollider = otherCollider;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Overlap({_thisCollider}, {_otherCollider})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Overlap))
            {
                return false;
            }
            else
            {
                return this == (Overlap)obj;
            }
        }
        public static bool operator ==(Overlap a, Overlap b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            if (a is null || b is null)
            {
                return false;
            }
            return (a._thisCollider == b.ThisCollider) && (a._otherCollider == b._otherCollider);
        }
        public static bool operator !=(Overlap a, Overlap b)
        {
            if (a is null && b is null)
            {
                return false;
            }
            if (a is null || b is null)
            {
                return true;
            }
            return (a._thisCollider != b.ThisCollider) || (a._otherCollider != b._otherCollider);
        }
        #endregion
        #region Methods
        public static Overlap Invert(Overlap source)
        {
            if (source is null)
            {
                throw new Exception("source cannot be null.");
            }
            return new Overlap(source._otherCollider, source.ThisCollider);
        }
        public Overlap Invert()
        {
            return new Overlap(_otherCollider, _thisCollider);
        }
        #endregion
    }
}