namespace EpsilonEngine
{
    public sealed class Tilemap : Component
    {
        #region Public Variables
        public readonly int CellWidth;
        public readonly int CellHeight;
        public readonly int TileWidth;
        public readonly int TileHeight;
        public readonly int PixelWidth;
        public readonly int PixelHeight;
        public Point Offset;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _XNAColorCache = new Microsoft.Xna.Framework.Color(value.R, value.G, value.B, value.A);
            }
        }
        #endregion
        #region Private Variables
        private int _heightMinusOne;
        private int _dataLength;
        private Tile[] _tileData;
        private Microsoft.Xna.Framework.Graphics.RenderTarget2D _XNARenderTarget;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _XNASpriteBatch;
        private Microsoft.Xna.Framework.Color _XNAColorCache = Microsoft.Xna.Framework.Color.White;
        private Color _color;
        #endregion
        #region Public Constructors
        public Tilemap(GameObject gameObject, int cellWidth, int cellHeight, int tileWidth, int tileHeight) : base(
            gameObject, 0, 0)
        {
            if (cellWidth <= 0)
            {
                throw new System.Exception("cellWidth must be greater than 0.");
            }
            CellWidth = cellWidth;
            if (cellHeight <= 0)
            {
                throw new System.Exception("cellHeight must be greater than 0.");
            }
            CellHeight = cellHeight;
            if (tileWidth <= 0)
            {
                throw new System.Exception("tileWidth must be greater than 0.");
            }
            TileWidth = tileWidth;
            if (tileHeight <= 0)
            {
                throw new System.Exception("tileHeight must be greater than 0.");
            }
            TileHeight = tileHeight;
            PixelWidth = CellWidth * TileWidth;
            PixelHeight = CellHeight * TileHeight;
            _heightMinusOne = TileHeight - 1;
            _dataLength = TileWidth * TileHeight;
            _tileData = new Tile[_dataLength];
            _XNARenderTarget =
                new Microsoft.Xna.Framework.Graphics.RenderTarget2D(Game.GameInterface.XNAGraphicsDevice, PixelWidth,
                    PixelHeight);
            _XNASpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(Game.GameInterface.XNAGraphicsDevice);
            Game.GameInterface.XNAGraphicsDevice.SetRenderTarget(_XNARenderTarget);
            Game.GameInterface.XNAGraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
            Game.GameInterface.XNAGraphicsDevice.SetRenderTarget(null);
        }
        public Tilemap(GameObject gameObject, int cellWidth, int cellHeight, int tileWidth, int tileHeight,
            Tile[] data) : base(gameObject, 0, 0)
        {
            if (cellWidth <= 0)
            {
                throw new System.Exception("cellWidth must be greater than 0.");
            }
            CellWidth = cellWidth;
            if (cellHeight <= 0)
            {
                throw new System.Exception("cellHeight must be greater than 0.");
            }
            CellHeight = cellHeight;
            if (tileWidth <= 0)
            {
                throw new System.Exception("tileWidth must be greater than 0.");
            }
            TileWidth = tileWidth;
            if (tileHeight <= 0)
            {
                throw new System.Exception("tileHeight must be greater than 0.");
            }
            TileHeight = tileHeight;
            PixelWidth = CellWidth * TileWidth;
            PixelHeight = CellHeight * TileHeight;
            _heightMinusOne = TileHeight - 1;
            _dataLength = TileWidth * TileHeight;
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != _dataLength)
            {
                throw new System.Exception("data.Length must be equal to tileWidth times tileHeight.");
            }
            _tileData = (Tile[])data.Clone();
            _XNARenderTarget =
                new Microsoft.Xna.Framework.Graphics.RenderTarget2D(Game.GameInterface.XNAGraphicsDevice, PixelWidth,
                    PixelHeight);
            _XNASpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(Game.GameInterface.XNAGraphicsDevice);
            Game.GameInterface.XNAGraphicsDevice.SetRenderTarget(_XNARenderTarget);
            Game.GameInterface.XNAGraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
            _XNASpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred,
                Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend,
                Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);
            for (int i = 0; i < _dataLength; i++)
            {
                Tile tile = _tileData[i];
                if (tile is not null)
                {
                    int x = (i % TileWidth) * CellWidth;
                    int y = (_heightMinusOne - (i / TileWidth)) * CellHeight;
                    y = (TileHeight * CellHeight) - y - CellHeight;
                    _XNASpriteBatch.Draw(tile.Texture._xnaTexture, new Microsoft.Xna.Framework.Vector2(x, y),
                        new Microsoft.Xna.Framework.Color(tile.Color.R, tile.Color.G, tile.Color.B, tile.Color.A));
                }
            }
            _XNASpriteBatch.End();
            Game.GameInterface.XNAGraphicsDevice.SetRenderTarget(null);
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Tilemap({TileWidth}, {TileHeight})";
        }
        #endregion
        #region Public Methods
        public void SetTile(int x, int y, Tile tile)
        {
            if (x < 0)
            {
                throw new System.Exception("x must be greater than or equal to 0.");
            }
            if (x >= TileWidth)
            {
                throw new System.Exception("x must be less than width.");
            }
            if (y < 0)
            {
                throw new System.Exception("y must be greater than or equal to 0.");
            }
            if (y >= TileHeight)
            {
                throw new System.Exception("y must be less than height.");
            }
            _tileData[((_heightMinusOne - y) * TileWidth) + x] = tile;
        }
        public Tile GetTile(int x, int y)
        {
            if (x < 0)
            {
                throw new System.Exception("x must be greater than or equal to 0.");
            }
            if (x >= TileWidth)
            {
                throw new System.Exception("x must be less than width.");
            }
            if (y < 0)
            {
                throw new System.Exception("y must be greater than or equal to 0.");
            }
            if (y >= TileHeight)
            {
                throw new System.Exception("y must be less than height.");
            }
            return _tileData[((_heightMinusOne - y) * TileWidth) + x];
        }
        public void SetData(Tile[] data)
        {
            if (data is null)
            {
                throw new System.Exception("data cannot be null.");
            }
            if (data.Length != _dataLength)
            {
                throw new System.Exception("data.Length must be equal to width times height.");
            }
            _tileData = (Tile[])data.Clone();
        }
        public Tile[] GetData()
        {
            return (Tile[])_tileData.Clone();
        }
        public void Fill(Tile color)
        {
            _tileData[0] = color;
            if (_dataLength == 1)
            {
                return;
            }
            int halfDataLength = _dataLength >> 1;
            int i = 1;
            while (i < halfDataLength)
            {
                System.Array.Copy(_tileData, 0, _tileData, i, i);
                i = i << 1;
            }
            if (i != _dataLength)
            {
                System.Array.Copy(_tileData, 0, _tileData, i, _dataLength - i);
            }
        }
        public void Clear()
        {
            _tileData = new Tile[_dataLength];
        }
        public void Apply()
        {
            Game.GameInterface.XNAGraphicsDevice.SetRenderTarget(_XNARenderTarget);
            Game.GameInterface.XNAGraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
            _XNASpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred,
                Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend,
                Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);
            for (int i = 0; i < _dataLength; i++)
            {
                Tile tile = _tileData[i];
                if (tile is not null)
                {
                    int x = (i % TileWidth) * CellWidth;
                    int y = (_heightMinusOne - (i / TileWidth)) * CellHeight;
                    y = (TileHeight * CellHeight) - y - CellHeight;
                    _XNASpriteBatch.Draw(tile.Texture._xnaTexture, new Microsoft.Xna.Framework.Vector2(x, y),
                        new Microsoft.Xna.Framework.Color(tile.Color.R, tile.Color.G, tile.Color.B, tile.Color.A));
                }
            }
            _XNASpriteBatch.End();
            Game.GameInterface.XNAGraphicsDevice.SetRenderTarget(null);
        }
        protected override void Render()
        {
            int positionX = Offset.X - Scene.CameraPositionX + GameObject.PositionX;
            int positionY = Scene.RenderHeight - Offset.Y + Scene.CameraPositionY - GameObject.PositionY - PixelHeight;
            if (positionX < -PixelWidth || positionY + PixelHeight < 0 || positionX > Scene.RenderWidth ||
                positionY > Scene.RenderHeight)
            {
                return;
            }
            Scene.XNASpriteBatch.Draw(_XNARenderTarget, new Microsoft.Xna.Framework.Vector2(positionX, positionY),
                _XNAColorCache);
        }
        #endregion
        #region Internal Methods
        public void SetTileUnsafe(int x, int y, Tile tile)
        {
            _tileData[((_heightMinusOne - y) * TileWidth) + x] = tile;
        }
        public Tile GetTileUnsafe(int x, int y)
        {
            return _tileData[((_heightMinusOne - y) * TileWidth) + x];
        }
        public void SetDataUnsafe(Tile[] data)
        {
            _tileData = data;
        }
        public Tile[] GetDataUnsafe()
        {
            return _tileData;
        }
        #endregion
    }
}