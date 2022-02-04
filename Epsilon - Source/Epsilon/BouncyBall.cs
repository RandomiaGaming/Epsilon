using System;
namespace EpsilonEngine
{
    public sealed class BouncyBall : GameObject
    {
        public BouncyBall(Scene stage, Texture texture, PhysicsManager physicsManager) : base(stage)
        {
            TextureRenderer ohGod = new TextureRenderer(this, texture);
            Rigidbody rigidbody = new Rigidbody(this, 0);
            double rot = RandomnessHelper.NextDouble(0, Math.PI * 2);
            rigidbody.velocity = new Vector((float)Math.Cos(rot) * 0.1f, (float)Math.Sin(rot) * 0.1f);
            rigidbody.bouncyness = new Vector(-1, -1);
            Collider collider = new Collider(this, physicsManager, 1);
            collider.Rect = new Rectangle(0, 0, 15, 15);
            LocalPosition =  new Point(RandomnessHelper.NextInt(16, Scene.ViewPortSize.X - 32), RandomnessHelper.NextInt(16, Scene.ViewPortSize.Y - 32));
        }
        public override string ToString()
        {
            return $"Epsilon.BouncyBall({Position})";
        }
    }
}
