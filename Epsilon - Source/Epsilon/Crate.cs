using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class Crate : PhysicsObject
    {
        public Crate(StagePlayer stagePlayer, PhysicsLayer physicsLayer, PhysicsLayer collsionPhysicsLayer, Texture crateTexture) : base(stagePlayer, physicsLayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = crateTexture;

            LocalColliderMinX = 0;
            LocalColliderMinY = 0;
            LocalColliderMaxX = 15;
            LocalColliderMaxY = 15;

            PushableUp = true;
            PushableDown = true;
            PushableLeft = true;
            PushableRight = true;

            PushOthersUp = true;
            PushOthersDown = true;
            PushOthersLeft = true;
            PushOthersRight = true;

            CollisionPhysicsLayer = collsionPhysicsLayer;
        }
        public override string ToString()
        {
            return $"Epsilon.Ground()";
        }
    }
}
