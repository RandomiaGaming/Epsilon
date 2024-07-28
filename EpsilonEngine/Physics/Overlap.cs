using System;
namespace EpsilonEngine
{
    public sealed class Overlap
    {
        #region Properties
        public PhysicsObject ThisPhysicsObject { get; private set; } = null;
        public PhysicsObject OtherPhysicsObject { get; private set; } = null;
        #endregion
        #region Contructors
        public Overlap(PhysicsObject thisPhysicsObject, PhysicsObject otherPhysicsObject)
        {
            if (thisPhysicsObject is null)
            {
                throw new Exception("thisPhysicsObject cannot be null.");
            }
            ThisPhysicsObject = thisPhysicsObject;

            if (otherPhysicsObject is null)
            {
                throw new Exception("otherPhysicsObject cannot be null.");
            }
            OtherPhysicsObject = otherPhysicsObject;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Overlap({ThisPhysicsObject}, {OtherPhysicsObject})";
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
            return (a.ThisPhysicsObject == b.ThisPhysicsObject) && (a.OtherPhysicsObject == b.OtherPhysicsObject);
        }
        public static bool operator !=(Overlap a, Overlap b)
        {
            return !(a == b);
        }
        #endregion
        #region Methods
        public static Overlap Invert(Overlap source)
        {
            return new Overlap(source.OtherPhysicsObject, source.ThisPhysicsObject);
        }
        public Overlap Invert()
        {
            return Invert(this);
        }
        #endregion
    }
}