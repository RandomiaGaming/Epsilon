using EpsilonEngine;
namespace Epsilon
{
    public sealed class Epsilon : Game
    {
        public Epsilon()
        {
            BackgroundColor = new Color(255, 150, 255, 255);
            new StagePlayer(this, new StageData());
           // new PauseMenu(this);
        }
        public override string ToString()
        {
            return $"Epsilon.Epsilon()";
        }
    }
}
