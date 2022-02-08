using System;
namespace EpsilonEngine
{
    public sealed class PhysicsScene : Scene
    {
        public PhysicsLayer[] PhysicsLayers { get; private set; } = new PhysicsLayer[0];
        public PhysicsScene(Game game) : base(game)
        {

        }
        public override string ToString()
        {
            return $"EpsilonEngine.PhysicsScene()";
        }
        public void ManageCollider(Collider collider)
        {
            if (collider is null)
            {
                throw new Exception("collider cannot be null.");
            }

            if (collider.PhysicsManager != this)
            {
                throw new Exception("collider belongs to a difference PhysicsManager.");
            }

            int physicsLayerCount = PhysicsLayers.Length;
            for (int i = 0; i < physicsLayerCount; i++)
            {
                PhysicsLayer physicsLayer = PhysicsLayers[i];
                if (physicsLayer.PhysicsLayerIndex == collider.PhysicsLayerIndex)
                {
                    physicsLayer.ManageCollider(collider);
                    return;
                }
            }

            PhysicsLayer newPhysicsLayer = new PhysicsLayer(this, collider.PhysicsLayerIndex);
            newPhysicsLayer.ManageCollider(collider);

            PhysicsLayer[] newPhysicsLayers = new PhysicsLayer[PhysicsLayers.Length + 1];
            Array.Copy(PhysicsLayers, 0, newPhysicsLayers, 0, PhysicsLayers.Length);
            newPhysicsLayers[PhysicsLayers.Length] = newPhysicsLayer;
            PhysicsLayers = newPhysicsLayers;
        }
        public Collider[] GetManagedColliders(int physicsLayerIndex)
        {
            int physicsLayersLength = PhysicsLayers.Length;

            for (int i = 0; i < physicsLayersLength; i++)
            {
                PhysicsLayer physicsLayer = PhysicsLayers[i];
                if(physicsLayer.PhysicsLayerIndex == physicsLayerIndex)
                {
                    return physicsLayer.ManagedColliders;
                }
            }

            return null;
        }
        public PhysicsLayer GetPhysicsLayer(int physicsLayerIndex)
        {
            for (int i = 0; i < PhysicsLayers.Length; i++)
            {
                if (PhysicsLayers[i].PhysicsLayerIndex == physicsLayerIndex)
                {
                    return PhysicsLayers[i];
                }
            }

            return null;
        }
    }
}
