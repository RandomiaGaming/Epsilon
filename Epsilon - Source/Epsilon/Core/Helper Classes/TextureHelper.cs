using System;

namespace EpsilonEngine
{
    public static class TextureHelper
    {
        public static Texture Blitz(Texture source, Texture destination, Point position)
        {
            Texture output = new Texture(sourceRectangle.max.x - sourceRectangle.min.x, sourceRectangle.max.y - sourceRectangle.min.y);

            for (int x = 0; x < source.width; x++)
            {
                for (int y = 0; y < source.height; y++)
                {
                    if (x + position.x >= 0 && x + position.x < destination.width && y + position.y >= 0 && y + position.y < destination.height)
                    {
                        Color otherColor = source.GetPixelUnsafe(x, y);
                        Color thisColor = destination.GetPixelUnsafe(x + position.x, y + position.y);
                        destination.SetPixelUnsafe(x + position.x, y + position.y, ColorHelper.Mix(thisColor, otherColor));
                    }
                }
            }
        }
        public static Texture SubTexture(Texture source, PointRect sourceRectangle)
        {
            if (sourceRectangle.min.x < 0 || sourceRectangle.min.y < 0 || sourceRectangle.max.x > source.width || sourceRectangle.max.y > source.height)
            {
                throw new ArgumentException();
            }

            Texture output = new Texture(sourceRectangle.max.x - sourceRectangle.min.x, sourceRectangle.max.y - sourceRectangle.min.y);

            for (int x = 0; x < output.width; x++)
            {
                for (int y = 0; y < output.height; y++)
                {
                    output.SetPixelUnsafe(x, y, source.GetPixelUnsafe(sourceRectangle.min.x + x, sourceRectangle.min.y + y));
                }
            }

            return output;
        }
    }
}
