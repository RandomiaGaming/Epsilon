using System;
using System.IO;

namespace EpsilonEngine
{
    public sealed class Texture
    {
        private Microsoft.Xna.Framework.Graphics.Texture2D _base = null;
        private Engine _engine;

        public readonly ushort _width = 0;
        public readonly ushort _height = 0;

        public ushort Width
        {
            get
            {
                return _width;
            }
        }
        public ushort Height
        {
            get
            {
                return _height;
            }
        }

        private Color[] buffer = new Color[0];

        public Engine Engine
        {
            get
            {
                return _engine;
            }
        }
        public Texture(Engine engine, ushort width, ushort height)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }
            _engine = engine;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            _width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            _height = height;

            buffer = new Color[_width * _height];

            _base = new Microsoft.Xna.Framework.Graphics.Texture2D(engine.GraphicsDevice, _width, _height);
        }
        public Texture(Engine engine, ushort width, ushort height, Color[] data)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }
            _engine = engine;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            _width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            _height = height;

            if (data is null)
            {
                throw new Exception("data cannot be null.");
            }

            if (data.Length != width * height)
            {
                throw new Exception("data.Length must be equal to width times height.");
            }

            buffer = (Color[])data.Clone();

            _base = new Microsoft.Xna.Framework.Graphics.Texture2D(engine.GraphicsDevice, _width, _height);

            Microsoft.Xna.Framework.Color[] XNABuffer = new Microsoft.Xna.Framework.Color[_width * _height];

            for (int i = 0; i < XNABuffer.Length; i++)
            {
                XNABuffer[i] = buffer[i].ToXNA();
            }

            _base.SetData(XNABuffer);
        }
        public Texture(Engine engine, string filePath)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }
            _engine = engine;

            if (!File.Exists(filePath))
            {
                throw new Exception("filePath does not exist.");
            }

            _base = Microsoft.Xna.Framework.Graphics.Texture2D.FromFile(engine.GraphicsDevice, filePath);

            if (_base.Width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (_base.Width > ushort.MaxValue)
            {
                throw new Exception("width was too large.");
            }
            _width = (ushort)_base.Width;

            if (_base.Height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (_base.Height > ushort.MaxValue)
            {
                throw new Exception("height was too large.");
            }
            _height = (ushort)_base.Height;


            Microsoft.Xna.Framework.Color[] XNABuffer = new Microsoft.Xna.Framework.Color[_width * _height];

            _base.GetData(XNABuffer);

            buffer = new Color[_width * _height];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = new Color(XNABuffer[i].PackedValue);
            }

            Apply();
        }

        public void SetPixelUnsafe(int x, int y, Color newColor)
        {
            buffer[(y * _width) + x] = newColor;
        }
        public Color GetPixelUnsafe(int x, int y)
        {
            return buffer[(y * _width) + x];
        }

        public void SetPixel(int x, int y, Color newColor)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                throw new Exception("Coordinants outside the bounds of the texture.");
            }
            buffer[(y * _width) + x] = newColor;
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                throw new Exception("Coordinants outside the bounds of the texture.");
            }
            return buffer[(y * _width) + x];
        }

        public void SetData(Color[] newData)
        {
            if (newData.Length != _width * _height)
            {
                throw new ArgumentException();
            }
            buffer = (Color[])newData.Clone();
        }
        public Color[] GetData()
        {
            return (Color[])buffer.Clone();
        }

        public void Apply()
        {
            Microsoft.Xna.Framework.Color[] XNABuffer = new Microsoft.Xna.Framework.Color[_width * _height];

            for (int i = 0; i < XNABuffer.Length; i++)
            {
                XNABuffer[i] = buffer[i].ToXNA();
            }

            _base.SetData(XNABuffer);
        }
        public Microsoft.Xna.Framework.Graphics.Texture2D ToXNA()
        {
            return _base;
        }
    }
}