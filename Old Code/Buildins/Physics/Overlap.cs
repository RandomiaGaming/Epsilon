using System;
namespace EpsilonEngine.Physics
{
    public sealed class Overlap
    {
        public readonly Collider otherCollider = null;
        public readonly GameObject otherGameObject = null;
        public readonly Collider thisCollider = null;
        public readonly GameObject thisGameObject = null;
        public Overlap(Collider thisCollider, Collider otherCollider)
        {
            if (thisCollider is null)
            {
                throw new NullReferenceException();
            }
            this.otherCollider = otherCollider;
            if (thisCollider.gameObject is null)
            {
                throw new NullReferenceException();
            }
            thisGameObject = thisCollider.gameObject;
            if (otherCollider is null)
            {
                throw new NullReferenceException();
            }
            this.otherCollider = otherCollider;
            if (otherCollider.gameObject is null)
            {
                throw new NullReferenceException();
            }
            if (thisCollider.game != otherCollider.game)
            {
                throw new ArgumentException();
            }
            if (thisCollider.gameInterface != otherCollider.gameInterface)
            {
                throw new ArgumentException();
            }
            otherGameObject = otherCollider.gameObject;
        }
    }
}