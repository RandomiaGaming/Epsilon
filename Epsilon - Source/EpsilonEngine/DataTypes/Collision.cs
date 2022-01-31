using System;
namespace EpsilonEngine
{
    public struct Collision
    {
        #region Properties
        public Collider ThisCollider { get; private set; }
        public GameObject ThisGameObject { get; private set; }
        public Collider OtherCollider { get; private set; }
        public GameObject OtherGameObject { get; private set; }
        public SideInfo SideInfo { get; private set; }
        #endregion
        #region Contructors
        public Collision(Collider thisCollider, Collider otherCollider, SideInfo sideInfo)
        {
            if (thisCollider is null)
            {
                throw new Exception("thisCollider cannot be null.");
            }
            ThisCollider = thisCollider;
            ThisGameObject = thisCollider.GameObject;

            if (otherCollider is null)
            {
                throw new Exception("otherCollider cannot be null.");
            }
            OtherCollider = otherCollider;
            OtherGameObject = otherCollider.GameObject;

            if (sideInfo == SideInfo.False)
            {
                throw new Exception("sideInfo cannot be false.");
            }
            SideInfo = sideInfo;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Collision({ThisCollider}, {OtherCollider}, {SideInfo})";
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
            return (a.ThisCollider == b.ThisCollider) && (a.OtherCollider == b.OtherCollider) && (a.SideInfo == b.SideInfo);
        }
        public static bool operator !=(Collision a, Collision b)
        {
            return (a.ThisCollider != b.ThisCollider) || (a.OtherCollider != b.OtherCollider) || (a.SideInfo != b.SideInfo);
        }
        #endregion
        #region Methods
        public static Collision Invert(Collision source)
        {
            return new Collision(source.OtherCollider, source.ThisCollider, source.SideInfo.Invert());
        }
        public Collision Invert()
        {
            return new Collision(OtherCollider, ThisCollider, SideInfo.Invert());
        }
        #endregion
    }
}
