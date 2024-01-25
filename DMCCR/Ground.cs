using EpsilonEngine;
namespace DMCCR
{
    public sealed class Ground : PhysicsObject
    {
        public Ground(Stage stagePlayer, Rect colliderShape) : base(stagePlayer, true)
        {
           SetColliderShape(new Rect[1] { colliderShape });
        }
        public override string ToString()
        {
            return $"DMCCR.Ground()";
        }
    }
}