using System;
namespace EpsilonEngine
{
    public sealed class BouncyBall : GameObject
    {
        public BouncyBall(Scene stage, Texture texture, PhysicsManager physicsManager) : base(stage)
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
            LocalPositionX = RandomnessHelper.NextInt(16, Scene.Width - 32);
            LocalPositionY = RandomnessHelper.NextInt(16, Scene.Height - 32);
        }
        public override string ToString()
        {
            return $"Epsilon.BouncyBall()";
        }
    }
}
