using EpsilonEngine;
namespace DMCCR
{
    public sealed class Lava : PhysicsObject
    {
        public Lava(Stage stagePlayer, Rect colliderShape) : base(stagePlayer, true)
        {
            SetColliderShape(new Rect[1] { colliderShape });
        }
        public override string ToString()
        {
            return $"DMCCR.Lava()";
        }
    }
}