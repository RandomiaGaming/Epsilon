using EpsilonEngine;
namespace DMCCR
{
    public sealed class DMCCR : Game
    {
        public DMCCR() : base(0, 0)
        {
            BackgroundColor = new Color(255, 0, 0, 0);
            Stage stage = new Stage(this);
            TargetFPS = double.PositiveInfinity;
            FPSCounter fPSCounter = new FPSCounter(stage);
            Canvas canvas = new Canvas(this);
            new Element(canvas);
        }
        public override string ToString()
        {
            return $"DMCCR.DMCCR()";
        }
    }
}