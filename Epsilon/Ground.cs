using EpsilonEngine;
namespace Epsilon
{
    public sealed class Ground : PhysicsObject
    {
        public Ground(StagePlayer stagePlayer, PhysicsLayer physicsLayer, PhysicsLayer[] collsionPhysicsLayers, Texture groundTexture) : base(stagePlayer, physicsLayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = groundTexture;

            LocalColliderRect = new Rectangle(0, 0, 15, 15);

            PushableUp = false;
            PushableDown = false;
            PushableLeft = false;
            PushableRight = false;

            PushOthersUp = false;
            PushOthersDown = false;
            PushOthersLeft = false;
            PushOthersRight = false;

            CollisionPhysicsLayers = collsionPhysicsLayers;
        }
        public override string ToString()
        {
            return $"Epsilon.Ground()";
        }
    }
}
