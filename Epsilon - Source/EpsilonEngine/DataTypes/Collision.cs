using System;
namespace Epsilon
{
    public sealed class Collision
    {
        public readonly Collider OtherCollider = null;
        public readonly StageObject OtherStageObject = null;
        public readonly Collider ThisCollider = null;
        public readonly StageObject ThisStageObject = null;
        public readonly SideInfo SideInfo = SideInfo.False;
        public Collision(Collider thisCollider, Collider otherCollider, SideInfo sideInfo)
        {
            if (thisCollider is null)
            {
                throw new NullReferenceException();
            }
            this.OtherCollider = otherCollider;
            if (thisCollider.StageObject is null)
            {
                throw new NullReferenceException();
            }
            ThisStageObject = thisCollider.StageObject;
            if (otherCollider is null)
            {
                throw new NullReferenceException();
            }
            this.OtherCollider = otherCollider;
            if (otherCollider.StageObject is null)
            {
                throw new NullReferenceException();
            }
            OtherStageObject = otherCollider.StageObject;
            this.SideInfo = sideInfo;
        }
        public Collision Invert()
        {
            return new Collision(OtherCollider, ThisCollider, SideInfo.Invert());
        }
    }
}
