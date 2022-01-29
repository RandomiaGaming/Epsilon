using System;
using Microsoft.Xna.Framework.Graphics;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class Stage : Scene
    {
        public const int ViewPortWidth = 256 * 2;
        public const int ViewPortHeight = 144 * 2;
        public Stage(Epsilon epsilon) : base(epsilon, ViewPortWidth, ViewPortHeight)
        {
            Player player = new Player(this);
            player.Position = new Point(0, 32);
            for (int i = 0; i < 32; i++)
            {
                Ground ground = new Ground(this);
                ground.Position = new Point((i * 16) - (16 * 16), 0);
            }

            PhysicsManager physicsManager = new PhysicsManager(this);
        }
    }
}
