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
            PhysicsLayer mainPhysicsLayer = new PhysicsLayer(this);

            Texture groundTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ground.png"));

            for (int i = 0; i < (ViewPortWidth / 16); i++)
            {
                Ground ground = new Ground(this, mainPhysicsLayer, mainPhysicsLayer, groundTexture);
                ground.PositionX = i * 16;
                ground.PositionY = 0;
                Ground upperGround = new Ground(this, mainPhysicsLayer, mainPhysicsLayer, groundTexture);
                upperGround.PositionX = i * 16;
                upperGround.PositionY = ViewPortHeight - 16;
            }

            for (int i = 1; i < (ViewPortHeight / 16) - 1; i++)
            {
                Ground ground = new Ground(this, mainPhysicsLayer, mainPhysicsLayer, groundTexture);
                ground.PositionX = 0;
                ground.PositionY = i * 16;

                Ground rightGround = new Ground(this, mainPhysicsLayer, mainPhysicsLayer, groundTexture);
                rightGround.PositionX = ViewPortWidth - 16;
                rightGround.PositionY = i * 16;
            }

            Texture playerTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ball.png"));

            Player player = new Player(this, mainPhysicsLayer, mainPhysicsLayer, playerTexture);
            player.PositionX = 16;
            player.PositionY = 16;

            Texture crateTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Crate.png"));

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Crate crate = new Crate(this, mainPhysicsLayer, mainPhysicsLayer, crateTexture);
                    crate.PositionX = x * 16 + 32;
                    crate.PositionY = y * 16 + 32;
                }
            }
        }
        public override string ToString()
        {
            return $"Epsilon.StagePlayer()";
        }
    }
}
