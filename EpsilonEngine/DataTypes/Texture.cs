using System;
using System.IO;

namespace EpsilonEngine
{
    public sealed class Texture
    {
        #region Variables
        internal Microsoft.Xna.Framework.Graphics.Texture2D XNABase { get; private set; } = null;
        internal Microsoft.Xna.Framework.Rectangle XNARectangle { get; private set; } = new Microsoft.Xna.Framework.Rectangle(0, 0, 0, 0);
        private Color[] buffer = new Color[0];
        #endregion
        #region Properties
        public ushort Width { get; private set; }
        public ushort Height { get; private set; }
        public Game Engine { get; private set; }
        #endregion
        #region Constructors
        public Texture(Game game, ushort width, ushort height)
        {
            if (game is null)
            {
                throw new Exception("engine cannot be null.");
            }
            Engine = game;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            Width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            Height = height;

            XNARectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, Width, Height);

            buffer = new Color[width * height];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.Black;
            }

            XNABase = new Microsoft.Xna.Framework.Graphics.Texture2D(game.GraphicsDevice, width, height);
        }
        public Texture(Game engine, ushort width, ushort height, Color[] data)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }
            Engine = engine;

            if (width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
           Width = width;

            if (height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            Height = height;

            XNARectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, Width, Height);

            if (data is null)
            {
                throw new Exception("data cannot be null.");
            }

            if (data.Length != width * height)
            {
                throw new Exception("data.Length must be equal to width times height.");
            }

            buffer = (Color[])data.Clone();

            XNABase = new Microsoft.Xna.Framework.Graphics.Texture2D(engine.GraphicsDevice, width, height);

            Microsoft.Xna.Framework.Color[] XNABuffer = new Microsoft.Xna.Framework.Color[width * height];

            for (int i = 0; i < XNABuffer.Length; i++)
            {
                XNABuffer[i] = buffer[i].ToXNA();
            }

            XNABase.SetData(XNABuffer);
        }
        public Texture(Game engine, string filePath)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }
            Engine = engine;

            if (!File.Exists(filePath))
            {
                throw new Exception("filePath does not exist.");
            }

            XNABase = Microsoft.Xna.Framework.Graphics.Texture2D.FromFile(engine.GraphicsDevice, filePath);

            if (XNABase.Width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (XNABase.Width > ushort.MaxValue)
            {
                throw new Exception("width was too large.");
            }
            Width = (ushort)XNABase.Width;

            if (XNABase.Height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (XNABase.Height > ushort.MaxValue)
            {
                throw new Exception("height was too large.");
            }
            Height = (ushort)XNABase.Height;

            XNARectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, Width, Height);

            Microsoft.Xna.Framework.Color[] XNABuffer = new Microsoft.Xna.Framework.Color[Width * Height];

            XNABase.GetData(XNABuffer);

            buffer = new Color[Width * Height];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = new Color(XNABuffer[i]);
            }
        }
        public Texture(Game engine, Stream stream)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }
            Engine = engine;

            XNABase = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(engine.GraphicsDevice, stream);

            if (XNABase.Width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (XNABase.Width > ushort.MaxValue)
            {
                throw new Exception("width was too large.");
            }
            Width = (ushort)XNABase.Width;

            if (XNABase.Height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (XNABase.Height > ushort.MaxValue)
            {
                throw new Exception("height was too large.");
            }
            Height = (ushort)XNABase.Height;

            XNARectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, Width, Height);

            Microsoft.Xna.Framework.Color[] XNABuffer = new Microsoft.Xna.Framework.Color[Width * Height];

            XNABase.GetData(XNABuffer);

            buffer = new Color[Width * Height];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = new Color(XNABuffer[i]);
            }
        }
        public Texture(Game engine, Microsoft.Xna.Framework.Graphics.Texture2D source)
        {
            if (engine is null)
            {
                throw new Exception("engine cannot be null.");
            }
            Engine = engine;

            if(source is null)
            {
                throw new Exception("source cannot be null.");
            }
            if(source.GraphicsDevice != engine.GraphicsDevice)
            {
                throw new Exception("source belongs to a different Game.");
            }
            XNABase = source;

            if (XNABase.Width <= 0)
            {
                throw new Exception("width must be greater than 0.");
            }
            if (XNABase.Width > ushort.MaxValue)
            {
                throw new Exception("width was too large.");
            }
            Width = (ushort)XNABase.Width;

            if (XNABase.Height <= 0)
            {
                throw new Exception("height must be greater than 0.");
            }
            if (XNABase.Height > ushort.MaxValue)
            {
                throw new Exception("height was too large.");
            }
            Height = (ushort)XNABase.Height;

            XNARectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, Width, Height);

            Microsoft.Xna.Framework.Color[] XNABuffer = new Microsoft.Xna.Framework.Color[Width * Height];

            XNABase.GetData(XNABuffer);

            buffer = new Color[Width * Height];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = new Color(XNABuffer[i]);
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Texture({Width}, {Height})";
        }
        #endregion
        #region Methods
        public void SetPixel(int x, int y, Color newColor)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                throw new Exception("Coordinants outside the bounds of the texture.");
            }
            buffer[(y * Width) + x] = newColor;
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                throw new Exception("Coordinants outside the bounds of the texture.");
            }
            return buffer[(y * Width) + x];
        }
        public void SetData(Color[] newData)
        {
            if (newData.Length != Width * Height)
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
            Microsoft.Xna.Framework.Color[] XNABuffer = new Microsoft.Xna.Framework.Color[Width * Height];

            for (int i = 0; i < XNABuffer.Length; i++)
            {
                XNABuffer[i] = buffer[i].ToXNA();
            }

            XNABase.SetData(XNABuffer);
        }
        public Microsoft.Xna.Framework.Graphics.Texture2D ToXNA()
        {
            return XNABase;
        }
        #endregion
    }
}