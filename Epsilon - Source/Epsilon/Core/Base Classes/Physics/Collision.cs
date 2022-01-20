using System;
namespace Epsilon
{
    public sealed class Collision
    {
        public readonly Collider otherCollider = null;
        public readonly StageObject otherStageItem = null;
        public readonly Collider thisCollider = null;
        public readonly StageObject thisStageItem = null;
        public readonly SideInfo sideInfo = SideInfo.False;
        public Collision(Collider thisCollider, Collider otherCollider, SideInfo sideInfo)
        {
            if (thisCollider is null)
            {
                throw new NullReferenceException();
            }
            this.otherCollider = otherCollider;
            if (thisCollider.stageItem is null)
            {
                throw new NullReferenceException();
            }
            thisStageItem = thisCollider.stageItem;
            if (otherCollider is null)
            {
                throw new NullReferenceException();
            }
            this.otherCollider = otherCollider;
            if (otherCollider.stageItem is null)
            {
                throw new NullReferenceException();
            }
            otherStageItem = otherCollider.stageItem;
            this.sideInfo = sideInfo;
        }
        public Collision Invert()
        {
            return new Collision(otherCollider, thisCollider, sideInfo.Invert());
        }
    }
}
