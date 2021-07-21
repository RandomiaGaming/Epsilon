using System;
namespace DontMelt
{
    public class Texture
    {
        public readonly int width = 0;
        public readonly int height = 0;
        private Color[] data = new Color[0];
        public Texture(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.width = width;
            this.height = height;
            data = new Color[width * height];
        }
        public Texture(int width, int height, Color fillColor)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.width = width;
            this.height = height;
            data = new Color[width * height];
            data[0] = fillColor;
            int halfDataLength = data.Length / 2;
            for (int i = 1; i < data.Length; i *= 2)
            {
                int copyLength = i;
                if (i >halfDataLength)
                {
                    copyLength = data.Length - i;
                }
                Array.Copy(data, 0, data, i, copyLength);
            }
        }
        public Texture(int width, int height, Color[] data)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.width = width;
            this.height = height;
            if (data is null)
            {
                throw new NullReferenceException();
            }
            if (data.Length != width * height)
            {
                throw new ArgumentException();
            }
            this.data = (Color[])data.Clone();
        }

        public void SetPixelUnsafe(int x, int y, Color newColor)
        {
            data[(y * width) + x] = newColor;
        }
        public Color GetPixelUnsafe(int x, int y)
        {
            return data[(y * width) + x];
        }

        public void SetPixel(int x, int y, Color newColor)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                throw new ArgumentOutOfRangeException();
            }
            data[(y * width) + x] = newColor;
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                throw new ArgumentOutOfRangeException();
            }
            return data[(y * width) + x];
        }

        public void SetData(Color[] newData)
        {
            if (newData.Length != width * height)
            {
                throw new ArgumentException();
            }
            data = (Color[])newData.Clone();
        }
        public Color[] GetData()
        {
            return (Color[])data.Clone();
        }
    }
}