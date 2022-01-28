using System;
namespace EpsilonEngine
{
    public sealed class Overlap
    {
        public readonly Collider OtherCollider = null;
        public readonly GameObject OtherGameObject = null;
        public readonly Collider ThisCollider = null;
        public readonly GameObject ThisGameObject = null;
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
            OtherGameObject = otherCollider.GameObject;
        }
    }
}