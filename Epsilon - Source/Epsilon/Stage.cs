using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class Stage : Scene
    {
        public const int ViewPortWidth = 256;
        public const int ViewPortHeight = 144;
        public Stage(Epsilon epsilon) : base(epsilon, ViewPortWidth, ViewPortHeight)
        {

            PhysicsManager physicsManager = new PhysicsManager(this);

            for (int i = 0; i < (ViewPortWidth / 16); i++)
            {
                Ground ground = new Ground(this, physicsManager);
                ground.Position = new Point(i * 16, 0);

                Ground upperGround = new Ground(this, physicsManager);
                upperGround.Position = new Point(i * 16, ViewPortHeight - 16);
            }

            for (int i = 1; i < (ViewPortHeight / 16) - 1; i++)
            {
                Ground ground = new Ground(this, physicsManager);
                ground.Position = new Point(0, i * 16);

                Ground upperGround = new Ground(this, physicsManager);
                upperGround.Position = new Point(ViewPortWidth - 16, i * 16);
            }

            Texture ballTexture = new Texture(Engine, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ball.png"));

            for (int i = 0; i < 1000; i++)
            {
                BouncyBall bouncyBall = new BouncyBall(this, ballTexture, physicsManager);
            }
        }
    }
}
