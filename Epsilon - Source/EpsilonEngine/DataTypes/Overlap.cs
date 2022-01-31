using System;
namespace EpsilonEngine
{
    public struct Overlap
    {
        #region Properties
        public Collider ThisCollider { get; private set; }
        public GameObject ThisGameObject { get; private set; }
        public Collider OtherCollider { get; private set; }
        public GameObject OtherGameObject { get; private set; }
        #endregion
        #region Contructors
        public Overlap(Collider thisCollider, Collider otherCollider)
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
            OtherGameObject = OtherCollider.GameObject;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Overlap({ThisCollider}, {OtherCollider})";
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
            return (a.ThisCollider == b.ThisCollider) && (a.OtherCollider == b.OtherCollider);
        }
        public static bool operator !=(Overlap a, Overlap b)
        {
            return (a.ThisCollider != b.ThisCollider) || (a.OtherCollider != b.OtherCollider);
        }
        #endregion
        #region Methods
        public static Overlap Invert(Overlap source)
        {
            return new Overlap(source.OtherCollider, source.ThisCollider);
        }
        public Overlap Invert()
        {
            return Invert(this);
        }
        #endregion
    }
}