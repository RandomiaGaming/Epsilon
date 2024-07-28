using System;
namespace EpsilonEngine
{
    public sealed class Collision
    {
        #region Properties
        public PhysicsObject ThisPhysicsObject { get; private set; } = null;
        public PhysicsObject OtherPhysicsObject { get; private set; } = null;
        public DirectionInfo SideInfo { get; private set; }
        #endregion
        #region Contructors
        public Collision(PhysicsObject thisPhysicsObject, PhysicsObject otherPhysicsObject, DirectionInfo sideInfo)
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

            if (sideInfo == DirectionInfo.False)
            {
                throw new Exception("sideInfo cannot be false.");
            }
            SideInfo = sideInfo;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Collision({ThisPhysicsObject}, {OtherPhysicsObject}, {SideInfo})";
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
            return (a.ThisPhysicsObject == b.ThisPhysicsObject) && (a.OtherPhysicsObject == b.OtherPhysicsObject) && (a.SideInfo == b.SideInfo);
        }
        public static bool operator !=(Collision a, Collision b)
        {
            return !(a == b);
        }
        #endregion
        #region Methods
        public static Collision Invert(Collision source)
        {
            return new Collision(source.OtherPhysicsObject, source.ThisPhysicsObject, source.SideInfo.Invert());
        }
        public Collision Invert()
        {
            return Invert(this);
        }
        #endregion
    }
}
