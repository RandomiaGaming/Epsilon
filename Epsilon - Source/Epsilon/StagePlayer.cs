using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class StagePlayer : PhysicsScene
    {
        public const int ViewPortWidth = 256;
        public const int ViewPortHeight = 144;
        public StagePlayer(Epsilon epsilon, StageData stageData) : base(epsilon, ViewPortWidth, ViewPortHeight)
        {
            PhysicsLayer groundLayer = new PhysicsLayer(this);

            Texture groundTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ground.png"));

            for (int i = 0; i < (ViewPortWidth / 16); i++)
            {
                Ground ground = new Ground(this, groundLayer, groundTexture);
                ground.PositionX = i * 16;
                ground.PositionY = 0;
                Ground upperGround = new Ground(this, groundLayer, groundTexture);
                upperGround.PositionX = i * 16;
                upperGround.PositionY = ViewPortHeight - 16;
            }

            for (int i = 1; i < (ViewPortHeight / 16) - 1; i++)
            {
                Ground ground = new Ground(this, groundLayer, groundTexture);
                ground.PositionX = 0;
                ground.PositionY = i * 16;

                Ground rightGround = new Ground(this, groundLayer, groundTexture);
                rightGround.PositionX = ViewPortWidth - 16;
                rightGround.PositionY = i * 16;
            }

            PhysicsLayer playerLayer = new PhysicsLayer(this);

            Texture playerTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ball.png"));

            for (int i = 0; i < 1000; i++)
            {
                new Player(this, playerLayer, playerTexture);
            }
        }
        public override string ToString()
        {
            return $"Epsilon.StagePlayer()";
        }
    }
}
