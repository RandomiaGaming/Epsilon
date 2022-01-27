using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpsilonEngine
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Engine epsilon = new Engine();
            Scene stage = new Scene(epsilon, 256, 144);
            Player player = new Player(stage);
            TextureRenderer textureRenderer = new TextureRenderer(player);
            textureRenderer.Texture = Texture2D.FromFile(epsilon.GraphicsDevice, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\LavaBubble.png");
            textureRenderer.Offset = new Point(0, 0);
            _ = new CameraFollower(player);
            new ParticleSystem(stage);
            epsilon.ChangeStage(stage);
            epsilon.Run();
        }
    }
}