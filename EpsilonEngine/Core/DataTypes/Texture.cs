//Approved 09/24/2022
namespace EpsilonEngine
{
    public sealed class Texture : System.IDisposable
    {
        #region Public Variables
        public readonly Game Game;
        public readonly int Width;
        public readonly int Height;
        #endregion
        #region Internal Variables
        internal Microsoft.Xna.Framework.Graphics.Texture2D _xnaTexture;
        internal bool _editorBound;
        internal object _editorBindingLock = new object();
        #endregion
        #region Public Constructors
        public Texture(Game game, int width, int height)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = width;
            if (height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = height;
            _xnaTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
        }
        public Texture(Game game, int width, int height, Color color)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = width;
            if (height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = height;
            int halfBufferLength = (width * height) << 1;
            byte[] buffer = new byte[halfBufferLength << 1];
            buffer[0] = color.R;
            buffer[1] = color.G;
            buffer[2] = color.B;
            buffer[3] = color.A;
            int extent = 4;
            while (extent <= halfBufferLength)
            {
                System.Array.Copy(buffer, 0, buffer, extent, extent);
                extent <<= 1;
            }
            System.Array.Copy(buffer, 0, buffer, extent, buffer.Length - extent);
            _xnaTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
            _xnaTexture.SetData<byte>(buffer);
        }
        public Texture(Game game, int width, int height, Color[] data)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = width;
            if (height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = height;
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != width * height)
            {
                throw new System.Exception("data.Length must be equal to width times height.");
            }
            _xnaTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
            _xnaTexture.SetData<Color>(data);
        }
        public Texture(Game game, int width, int height, byte[] data)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = width;
            if (height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = height;
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != (width * height) << 2)
            {
                throw new System.Exception("data.Length must be equal to width times height times 4.");
            }
            _xnaTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
            _xnaTexture.SetData<byte>(data);
        }
        public Texture(Game game, int width, int height, System.IO.Stream data)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = width;
            if (height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = height;
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            int dataBytesLength = (width * height) << 2;
            if (data.Position - data.Length != dataBytesLength)
            {
                throw new System.Exception("Starting at data.Position there must be at least width times height times 4 bytes of data before the end of the stream.");
            }
            _xnaTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(Game.GameInterface.GraphicsDevice, Width, Height);
            byte[] dataBytes = new byte[dataBytesLength];
            data.Read(dataBytes, 0, dataBytesLength);
            _xnaTexture.SetData<byte>(dataBytes);
        }

        public Texture(Game game, string filePath)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (filePath is null)
            {
                throw new System.Exception("filePath cannot be null.");
            }
            if (filePath is "")
            {
                throw new System.Exception("filePath cannot be empty.");
            }
            if (!System.IO.File.Exists(filePath))
            {
                throw new System.Exception("filePath does not exist.");
            }
            Microsoft.Xna.Framework.Graphics.Texture2D xnaBase;
            try
            {
                xnaBase = Microsoft.Xna.Framework.Graphics.Texture2D.FromFile(Game.GameInterface.GraphicsDevice, filePath);
            }
            catch
            {
                throw new System.Exception("texture could not be loaded from filePath.");
            }
            if (xnaBase is null)
            {
                throw new System.Exception("texture could not be loaded from filePath.");
            }
            _xnaTexture = xnaBase;
            if (xnaBase.Width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = xnaBase.Width;
            if (xnaBase.Height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = xnaBase.Height;
        }
        public Texture(Game game, byte[] encodedBytes)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (encodedBytes is null)
            {
                throw new System.Exception("encodedBytes cannot be null.");
            }
            if (encodedBytes.Length is 0)
            {
                throw new System.Exception("encodedBytes cannot be empty.");
            }
            System.IO.MemoryStream stream = new System.IO.MemoryStream(encodedBytes);
            Microsoft.Xna.Framework.Graphics.Texture2D xnaBase;
            try
            {
                xnaBase = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Game.GameInterface.GraphicsDevice, stream);
            }
            catch
            {
                throw new System.Exception("texture could not be loaded from bytes.");
            }
            if (xnaBase is null)
            {
                throw new System.Exception("texture could not be loaded from bytes.");
            }
            _xnaTexture = xnaBase;
            if (xnaBase.Width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = xnaBase.Width;
            if (xnaBase.Height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = xnaBase.Height;
        }
        public Texture(Game game, System.IO.Stream encodedStream)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            if (encodedStream is null)
            {
                throw new System.Exception("encodedStream cannot be null.");
            }
            if (!encodedStream.CanRead)
            {
                throw new System.Exception("encodedStream must be readable.");
            }
            Microsoft.Xna.Framework.Graphics.Texture2D xnaBase;
            try
            {
                xnaBase = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Game.GameInterface.GraphicsDevice, encodedStream);
            }
            catch
            {
                throw new System.Exception("texture could not be loaded from stream.");
            }
            if (xnaBase is null)
            {
                throw new System.Exception("texture could not be loaded from stream.");
            }
            _xnaTexture = xnaBase;
            if (xnaBase.Width <= 0)
            {
                throw new System.Exception("width must be greater than 0.");
            }
            Width = xnaBase.Width;
            if (xnaBase.Height <= 0)
            {
                throw new System.Exception("height must be greater than 0.");
            }
            Height = xnaBase.Height;
        }

        public Texture(Texture source)
        {
            if (source is null)
            {
                throw new System.Exception("source cannot be null.");
            }
            Game = source.Game;
            Width = source.Width;
            Height = source.Height;
            _xnaTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(source.Game.GameInterface.GraphicsDevice, source.Width, source.Height);
            byte[] buffer = new byte[(source.Width * source.Height) << 2];
            source._xnaTexture.GetData<byte>(buffer);
            _xnaTexture.SetData<byte>(buffer);
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Texture({Width}, {Height})";
        }
        #endregion
        #region Public Methods
        public void SaveAsJPG(string filePath)
        {
            if(filePath is null)
            {
                throw new System.Exception("filePath cannot be null.");
            }
            if(filePath is "")
            {
                throw new System.Exception("filePath cannot be empty.");
            }
            if (System.IO.File.Exists(filePath))
            {
                throw new System.Exception("File already exists at filePath.");
            }
            System.IO.FileStream fileStream = System.IO.File.Open(filePath, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
            _xnaTexture.SaveAsJpeg(fileStream, Width, Height);
            fileStream.Dispose();
        }
        public void SaveAsPNG(string filePath)
        {
            if (filePath is null)
            {
                throw new System.Exception("filePath cannot be null.");
            }
            if (filePath is "")
            {
                throw new System.Exception("filePath cannot be empty.");
            }
            if (System.IO.File.Exists(filePath))
            {
                throw new System.Exception("File already exists at filePath.");
            }
            System.IO.FileStream fileStream = System.IO.File.Open(filePath, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
            _xnaTexture.SaveAsPng(fileStream, Width, Height);
            fileStream.Dispose();
        }
        public Texture Clone()
        {
            byte[] buffer = new byte[(Width * Height) << 2];
            _xnaTexture.GetData<byte>(buffer);
            return new Texture(Game, Width, Height, buffer);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}