using EpsilonEngine;
namespace DMCCR
{
    public sealed class NoJump : PhysicsObject
    {
        public NoJump(Stage stagePlayer) : base(stagePlayer, true)
        {
            SetColliderShape(new Rect[1] { new Rect(0, 0, 15, 15) });
        }
        public override string ToString()
        {
            return $"DMCCR.NoJump()";
        }
    }
}