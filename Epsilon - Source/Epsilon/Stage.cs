using System.Reflection;
using EpsilonEngine;
using System;
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
                ground.LocalPositionX = i * 16;
                ground.LocalPositionY = 0;
                Ground upperGround = new Ground(this, physicsManager);
                upperGround.LocalPositionX = i * 16;
                upperGround.LocalPositionY = ViewPortHeight - 16;
            }

            for (int i = 1; i < (ViewPortHeight / 16) - 1; i++)
            {
                Ground ground = new Ground(this, physicsManager);
                ground.LocalPositionX = 0;
                ground.LocalPositionY = i * 16;

                Ground rightGround = new Ground(this, physicsManager);
                rightGround.LocalPositionX = ViewPortWidth - 16;
                rightGround.LocalPositionY = i * 16;
            }

            Texture ballTexture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ball.png"));

            for (int i = 0; i < 1000; i++)
            {
                new BouncyBall(this, ballTexture, physicsManager);
            }
        }
    }
}
