using System;
namespace DontMelt
{
    public sealed class Overlap
    {
        public readonly Collider otherCollider = null;
        public readonly StageItem otherStageItem = null;
        public readonly Collider thisCollider = null;
        public readonly StageItem thisStageItem = null;
        public Overlap(Collider thisCollider, Collider otherCollider)
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
        }
    }
}