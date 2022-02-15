using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class Ground : PhysicsObject
    {
        public Ground(StagePlayer stagePlayer, PhysicsLayer physicsLayer, PhysicsLayer collsionPhysicsLayer, Texture groundTexture) : base(stagePlayer, physicsLayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = groundTexture;

            LocalColliderMinX = 0;
            LocalColliderMinY = 0;
            LocalColliderMaxX = 15;
            LocalColliderMaxY = 15;

            PushableUp = false;
            PushableDown = false;
            PushableLeft = false;
            PushableRight = false;

            PushOthersUp = false;
            PushOthersDown = false;
            PushOthersLeft = false;
            PushOthersRight = false;

            CollisionPhysicsLayer = collsionPhysicsLayer;
        }
        public override string ToString()
        {
            return $"Epsilon.Ground()";
        }
    }
}
