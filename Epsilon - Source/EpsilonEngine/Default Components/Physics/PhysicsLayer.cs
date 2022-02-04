using System;
namespace EpsilonEngine
{
    public sealed class PhysicsLayer
    {
        public int PhysicsLayerIndex { get; private set; } = 0;
        public PhysicsManager PhysicsManager { get; private set; } = null;
        public Collider[] ManagedColliders { get; private set; } = new Collider[0];
        public PhysicsLayer(PhysicsManager physicsManager, int physicsLayerIndex)
        {
            if (physicsManager is null)
            {
                throw new Exception("physicsManager cannot be null.");
            }

            PhysicsManager = physicsManager;

            PhysicsLayerIndex = physicsLayerIndex;
        }
        public void ManageCollider(Collider collider)
        {
            if (collider is null)
            {
                throw new Exception("collider cannot be null.");
            }

            if (collider.PhysicsLayerIndex != PhysicsLayerIndex)
            {
                throw new Exception("collider belongs to a different PhysicsLayer.");
            }

            if (collider.PhysicsManager != PhysicsManager)
            {
                throw new Exception("collider belongs to a different PhysicsManager.");
            }

            Collider[] newManagedColliders = new Collider[ManagedColliders.Length + 1];
            Array.Copy(ManagedColliders, 0, newManagedColliders, 0, ManagedColliders.Length);
            newManagedColliders[ManagedColliders.Length] = collider;
            ManagedColliders = newManagedColliders;
        }
    }
}
