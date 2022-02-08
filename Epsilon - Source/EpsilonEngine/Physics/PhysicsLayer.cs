using System;
namespace EpsilonEngine
{
    public sealed class PhysicsLayer
    {
        public int PhysicsLayerIndex { get; private set; } = 0;
        public PhysicsScene PhysicsScene { get; private set; } = null;
        public Collider[] ManagedColliders { get; private set; } = new Collider[0];
        public PhysicsLayer(PhysicsScene physicsScene, int physicsLayerIndex)
        {
            if (physicsScene is null)
            {
                throw new Exception("physicsManager cannot be null.");
            }

            PhysicsScene = physicsScene;

            PhysicsLayerIndex = physicsLayerIndex;
        }
        internal void ManageCollider(Collider collider)
        {
            Collider[] newManagedColliders = new Collider[ManagedColliders.Length + 1];
            Array.Copy(ManagedColliders, 0, newManagedColliders, 0, ManagedColliders.Length);
            newManagedColliders[ManagedColliders.Length] = collider;
            ManagedColliders = newManagedColliders;
        }
    }
}
