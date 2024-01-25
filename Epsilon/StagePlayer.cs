using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class StagePlayer : PhysicsScene
    {
        public const int ViewPortWidth = 256 * 2;
        public const int ViewPortHeight = 144 * 2;
        public StagePlayer(Epsilon epsilon, StageData stageData) : base(epsilon, ViewPortWidth, ViewPortHeight)
        {
            PhysicsLayer groundPhysicsLayer = new PhysicsLayer(this);

            Texture groundTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ground.png"));

            PhysicsLayer glassPhysicsLayer = new PhysicsLayer(this);

            Texture glassTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Glass.png"));

            PhysicsLayer cratePhysicsLayer = new PhysicsLayer(this);

            Texture crateTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Crate.png"));

            PhysicsLayer playerPhysicsLayer = new PhysicsLayer(this);

            Texture playerTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ball.png"));

            for (int i = 0; i < (ViewPortWidth / 16); i++)
            {
                Ground ground = new Ground(this, groundPhysicsLayer, null, groundTexture);
                ground.PositionX = i * 16;
                ground.PositionY = 0;
                Ground upperGround = new Ground(this, groundPhysicsLayer, null, groundTexture);
                upperGround.PositionX = i * 16;
                upperGround.PositionY = ViewPortHeight - 16;
            }

            for (int i = 1; i < (ViewPortHeight / 16) - 1; i++)
            {
                Ground ground = new Ground(this, groundPhysicsLayer, null, groundTexture);
                ground.PositionX = 0;
                ground.PositionY = i * 16;

                Ground rightGround = new Ground(this, groundPhysicsLayer, null, groundTexture);
                rightGround.PositionX = ViewPortWidth - 16;
                rightGround.PositionY = i * 16;
            }

            for (int i = 1; i < (ViewPortWidth / 16) - 1; i++)
            {
                Glass glass = new Glass(this, glassPhysicsLayer, null, glassTexture);
                glass.PositionX = i * 16;
                glass.PositionY = 16;

                Glass upperGlass = new Glass(this, glassPhysicsLayer, null, glassTexture);
                upperGlass.PositionX = i * 16;
                upperGlass.PositionY = ViewPortHeight - 32;
            }
            
            for (int i = 2; i < (ViewPortHeight / 16) - 2; i++)
            {
                Glass glass = new Glass(this, glassPhysicsLayer, null, glassTexture);
                glass.PositionX = 16;
                glass.PositionY = i * 16;

                Glass rightGlass = new Glass(this, glassPhysicsLayer, null, glassTexture);
                rightGlass.PositionX = ViewPortWidth - 32;
                rightGlass.PositionY = i * 16;
            }

            for (int x = -4; x < 4; x++)
            {
                for (int y = -4; y < 4; y++)
                {
                    Crate crate = new Crate(this, cratePhysicsLayer, new PhysicsLayer[] { playerPhysicsLayer, cratePhysicsLayer, groundPhysicsLayer, glassPhysicsLayer}, crateTexture);
                    crate.PositionX = (ViewPortWidth / 2) + (x * 16);
                    crate.PositionY = (ViewPortHeight / 2) + (y * 16);
                }
            }

            Player player = new Player(this, playerPhysicsLayer, new PhysicsLayer[] { playerPhysicsLayer, cratePhysicsLayer, groundPhysicsLayer }, playerTexture);
            player.PositionX = 16;
            player.PositionY = 16;
        }
        public override string ToString()
        {
            return $"Epsilon.StagePlayer()";
        }
    }
}
