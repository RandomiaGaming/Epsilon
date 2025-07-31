namespace EpsilonEngine
{
    public sealed class TextureEditor
    {
        #region Public Variables
        public readonly Game Game;
        public readonly Texture Texture;
        public readonly int Width;
        public readonly int Height;
        #endregion
        #region Internal Variables
        internal int _stride;
        internal int _heightMinusOne;
        internal byte[] _buffer;
        internal Microsoft.Xna.Framework.Graphics.Texture2D _xnaTexture;
        #endregion
        #region Public Constructors
        public TextureEditor(Texture texture)
        {
            if (texture is null)
            {
                throw new System.Exception("texture cannot be null.");
            }
            Texture = texture;
            lock (texture._editorBindingLock)
            {
                if (texture._editorBound)
                {
                    throw new System.Exception("A textureEditor has already been created for this Texture.");
                }
                texture._editorBound = true;
            }
            Game = texture.Game;
            Width = texture.Width;
            Height = texture.Height;
            _stride = texture.Width << 2;
            _heightMinusOne = texture.Height - 1;
            _xnaTexture = texture._xnaTexture;
            _buffer = new byte[_stride * texture.Height];
            texture._xnaTexture.GetData<byte>(_buffer);
        }
        #endregion
        #region Public Methods
        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0)
            {
                throw new System.Exception("x must be greater than or equal to 0.");
            }
            if (x >= Width)
            {
                throw new System.Exception("x must be less than width.");
            }
            if (y < 0)
            {
                throw new System.Exception("y must be greater than or equal to 0.");
            }
            if (y >= Height)
            {
                throw new System.Exception("y must be less than height.");
            }
            int targetIndex = ((_heightMinusOne - y) * _stride) + (x << 2);
            _buffer[targetIndex] = color.R;
            _buffer[targetIndex + 1] = color.G;
            _buffer[targetIndex + 2] = color.B;
            _buffer[targetIndex + 3] = color.A;
        }
        public Color GetPixel(int x, int y)
        {
            if (x < 0)
            {
                throw new System.Exception("x must be greater than or equal to 0.");
            }
            if (x >= Width)
            {
                throw new System.Exception("x must be less than width.");
            }
            if (y < 0)
            {
                throw new System.Exception("y must be greater than or equal to 0.");
            }
            if (y >= Height)
            {
                throw new System.Exception("y must be less than height.");
            }
            int targetIndex = ((_heightMinusOne - y) * _stride) + (x << 2);
            Color output;
            output.R = _buffer[targetIndex];
            output.G = _buffer[targetIndex + 1];
            output.B = _buffer[targetIndex + 2];
            output.A = _buffer[targetIndex + 3];
            return output;
        }
        /*public void DrawRect(Rect rect, Color color)
        {

        }
        public void DrawCircle(Point center, int diameter, Color color)
        {

        }
        public void DrawTri(Point p0, Point p1, Point p2, Color color)
        {

        }
        public void SetData(Color[] data)
        {
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != _dataLength)
            {
                throw new System.Exception("data.Length must be equal to width times height.");
            }

            _colorData = (Color[])data.Clone();

            for (int i = 0; i < _dataLength; i++)
            {
                Color color = _colorData[i];
                _XNAColorData[i] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
            }
        }
        public Color[] GetData()
        {
            return (Color[])_colorData.Clone();
        }
        public void Clear(Color color)
        {
            _colorData[0] = color;

            _XNAColorData[0] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);

            if (_dataLength == 1)
            {
                return;
            }

            int halfDataLength = _dataLength >> 1;

            int i = 1;

            while (i < halfDataLength)
            {
                System.Array.Copy(_colorData, 0, _colorData, i, i);
                System.Array.Copy(_XNAColorData, 0, _XNAColorData, i, i);
                i = i << 2;
            }

            if (i != _dataLength)
            {
                System.Array.Copy(_colorData, 0, _colorData, i, _dataLength - i);
                System.Array.Copy(_XNAColorData, 0, _XNAColorData, i, _dataLength - i);
            }
        }
        public void Apply()
        {
            _XNABase.SetData(_XNAColorData);
        }*/
        #endregion
        #region Internal Methods
        internal void SetPixelUnsafe(int x, int y, Color color)
        {
            int targetIndex = ((_heightMinusOne - y) * _stride) + (x << 2);
            _buffer[targetIndex] = color.R;
            _buffer[targetIndex + 1] = color.G;
            _buffer[targetIndex + 2] = color.B;
            _buffer[targetIndex + 3] = color.A;
        }
        internal Color GetPixelUnsafe(int x, int y)
        {
            int targetIndex = ((_heightMinusOne - y) * _stride) + (x << 2);
            Color output;
            output.R = _buffer[targetIndex];
            output.G = _buffer[targetIndex + 1];
            output.B = _buffer[targetIndex + 2];
            output.A = _buffer[targetIndex + 3];
            return output;
        }
        /*public void SetDataUnsafe(Color[] data)
        {
            _colorData = data;

            for (int i = 0; i < _dataLength; i++)
            {
                Color color = _colorData[i];
                _XNAColorData[i] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
            }
        }
        public Color[] GetDataUnsafe()
        {
            return _colorData;
        }*/
        #endregion
    }
}
