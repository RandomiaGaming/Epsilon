using EpsilonEngine;
namespace Epsilon
{
    public sealed class Crate : PhysicsObject
    {
        public Crate(StagePlayer stagePlayer, PhysicsLayer physicsLayer, PhysicsLayer[] collsionPhysicsLayers, Texture crateTexture) : base(stagePlayer, physicsLayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = crateTexture;

            LocalColliderRect = new Rectangle(0, 0, 15, 15);

            PushableUp = true;
            PushableDown = true;
            PushableLeft = true;
            PushableRight = true;

            PushOthersUp = true;
            PushOthersDown = true;
            PushOthersLeft = true;
            PushOthersRight = true;

            CollisionPhysicsLayers = collsionPhysicsLayers;
        }
        public override string ToString()
        {
            return $"Epsilon.Crate()";
        }
        protected override void Update()
        {
            VelocityY = -0.1f;
        }
    }
}
