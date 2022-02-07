using System;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class BouncyBall : GameObject
    {
        public BouncyBall(StagePlayer stagePlayer, Texture texture, PhysicsManager physicsManager) : base(stagePlayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = texture;
            Collider collider = new Collider(this, physicsManager, 1);
            collider.Rect = new Rectangle(0, 0, 15, 15);
            Rigidbody rigidbody = new Rigidbody(this, 0);
            double rot = RandomnessHelper.NextDouble(0, Math.PI * 2);
            rigidbody.VelocityX = (float)Math.Cos(rot) * 0.1f;
            rigidbody.VelocityY = (float)Math.Sin(rot) * 0.1f;
            rigidbody.BouncynessX = -1f;
            rigidbody.BouncynessY = -1f;
            PositionX = RandomnessHelper.NextInt(16, Scene.Width - 32);
            PositionY = RandomnessHelper.NextInt(16, Scene.Height - 32);
        }
        public override string ToString()
        {
            return $"Epsilon.BouncyBall()";
        }
    }
}
