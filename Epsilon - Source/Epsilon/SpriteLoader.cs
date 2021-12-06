using System.Collections.Generic;
namespace Epsilon
{
    public static class SpriteLoader
    {
        public static List<Sprite> sprites = null;
        public static void LoadSprite(string assetPath)
        {
            
            System.Drawing.Image loadedImage = System.Drawing.Image.FromStream(sourceStream);
            System.Drawing.Bitmap loadedBitMap = new System.Drawing.Bitmap(loadedImage);
            Texture output = new Texture(loadedBitMap.Width, loadedBitMap.Height);
            for (int x = 0; x < loadedBitMap.Width; x++)
            {
                for (int y = 0; y < loadedBitMap.Height; y++)
                {
                    System.Drawing.Color systemColor = loadedBitMap.GetPixel(x, loadedBitMap.Height - y - 1);
                    output.SetPixel(x, y, new Color(systemColor.R, systemColor.G, systemColor.B, systemColor.A));
                }
            }
            return new TextureAsset(sourceStream, fullName, output);
        }
    }
}