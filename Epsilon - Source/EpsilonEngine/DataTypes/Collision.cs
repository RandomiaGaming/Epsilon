using System;
namespace EpsilonEngine
{
    public sealed class Collision
    {
        #region Variables
        private Collider _otherCollider = null;
        private Collider _thisCollider = null;
        private SideInfo _sideInfo = SideInfo.False;
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
        public SideInfo SideInfo
        {
            get
            {
                return _sideInfo;
            }
        }
        #endregion
        #region Contructors
        public Collision(Collider thisCollider, Collider otherCollider, SideInfo sideInfo)
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

            if(sideInfo == SideInfo.False)
            {
                throw new Exception("sideInfo cannot be false.");
            }
            _sideInfo = sideInfo;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Collision({_thisCollider}, {_otherCollider}, {_sideInfo})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Collision))
            {
                return false;
            }
            else
            {
                return this == (Collision)obj;
            }
        }
        public static bool operator ==(Collision a, Collision b)
        {
            if(a is null && b is null)
            {
                return true;
            }
            if(a is null || b is null)
            {
                return false;
            }
            return (a._thisCollider == b.ThisCollider) && (a._otherCollider == b._otherCollider) && (a._sideInfo == b._sideInfo);
        }
        public static bool operator !=(Collision a, Collision b)
        {
            if (a is null && b is null)
            {
                return false;
            }
            if (a is null || b is null)
            {
                return true;
            }
            return (a._thisCollider != b.ThisCollider) || (a._otherCollider != b._otherCollider) || (a._sideInfo != b._sideInfo);
        }
        #endregion
        #region Methods
        public static Collision Invert(Collision source)
        {
            if(source is null)
            {
                throw new Exception("source cannot be null.");
            }
            return new Collision(source._otherCollider, source.ThisCollider, source._sideInfo.Invert());
        }
        public Collision Invert()
        {
            return new Collision(_otherCollider, _thisCollider, _sideInfo.Invert());
        }
        #endregion
    }
}
