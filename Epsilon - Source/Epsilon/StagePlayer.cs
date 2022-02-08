using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class StagePlayer : Scene
    {
        public const int ViewPortWidth = 256 * 2;
        public const int ViewPortHeight = 144 * 2;
        public StagePlayer(Epsilon epsilon, StageData stageData) : base(epsilon, ViewPortWidth, ViewPortHeight)
        {
            for (int i = 0; i < (ViewPortWidth / 16); i++)
            {
                Ground ground = new Ground(this);
                ground.PositionX = i * 16;
                ground.PositionY = 0;
                Ground upperGround = new Ground(this);
                upperGround.PositionX = i * 16;
                upperGround.PositionY = ViewPortHeight - 16;
            }

            for (int i = 1; i < (ViewPortHeight / 16) - 1; i++)
            {
                Ground ground = new Ground(this);
                ground.PositionX = 0;
                ground.PositionY = i * 16;

                Ground rightGround = new Ground(this);
                rightGround.PositionX = ViewPortWidth - 16;
                rightGround.PositionY = i * 16;
            }

            Texture playerTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ball.png"));

            for (int i = 0; i < 1000; i++)
            {
                new Player(this, playerTexture);
            }
        }
        public override string ToString()
        {
            return $"Epsilon.StagePlayer()";
        }
    }
}
