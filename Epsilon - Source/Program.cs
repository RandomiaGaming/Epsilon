using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Epsilon epsilon = new Epsilon();
            Stage stage = new Stage(epsilon);
            Player player = new Player(stage);
            TextureRenderer textureRenderer = new TextureRenderer(player);
            textureRenderer.Texture = Texture2D.FromFile(epsilon.GraphicsDevice, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\LavaBubble.png");
            textureRenderer.Offset = new Point(0, 0);
            player.AddComponent(textureRenderer);
            stage.AddStageObject(player);
            epsilon.ChangeStage(stage);

            epsilon.Run();
        }
    }
}