namespace EpsilonEngine
{
    public sealed class TextureRenderer : Component
    {
        public Texture Texture { get; set; } = null;
        public int OffsetX = 0;
        public int OffsetY = 0;
        public Point Offset
        {
            get
            {
                return new Point(OffsetX, OffsetY);
            }
            set
            {
                OffsetX = value.X;
                OffsetY = value.Y;
            }
        }

        private Microsoft.Xna.Framework.Color _XNAColorCache = Microsoft.Xna.Framework.Color.White;
        private Microsoft.Xna.Framework.Vector2 _XNAPositionCache = Microsoft.Xna.Framework.Vector2.Zero;

        private byte _colorR = byte.MinValue;
        private byte _colorG = byte.MinValue;
        private byte _colorB = byte.MinValue;
        private byte _colorA = byte.MinValue;

        public byte ColorR
        {
            get
            {
                return _colorR;
            }
            set
            {
                _colorR = value;
                _XNAColorCache.R = _colorR;
            }
        }
        public byte ColorG
        {
            get
            {
                return _colorG;
            }
            set
            {
                _colorG = value;
                _XNAColorCache.G = _colorG;
            }
        }
        public byte ColorB
        {
            get
            {
                return _colorB;
            }
            set
            {
                _colorB = value;
                _XNAColorCache.B = _colorB;
            }
        }
        public byte ColorA
        {
            get
            {
                return _colorA;
            }
            set
            {
                _colorA = value;
                _XNAColorCache.A = _colorA;
            }
        }

        public Color Color
        {
            get
            {
                return new Color(_colorR, _colorG, _colorB, _colorA);
            }
            set
            {
                _colorR = value.R;
                _colorG = value.G;
                _colorB = value.B;
                _colorA = value.A;
                _XNAColorCache = new Microsoft.Xna.Framework.Color(_colorR, _colorG, _colorB, _colorA);
            }
        }
        public TextureRenderer(GameObject gameObject) : base(gameObject)
        {

        }
        protected override void Render()
        {
            if (Texture is not null)
            {
                int positionX = OffsetX - Scene.CameraPositionX + GameObject.PositionX;
                int positionY = Scene.Height - OffsetY + Scene.CameraPositionY - GameObject.PositionY - Texture.Height;
                if (positionX < -Texture.Width || positionY + Texture.Height < 0 || positionX > Scene.Width || positionY > Scene.Height)
                {
                    return;
                }
                _XNAPositionCache.X = positionX;
                _XNAPositionCache.Y = positionY;
                Scene._spriteBatch.Draw(Texture.XNABase, _XNAPositionCache, new Microsoft.Xna.Framework.Color(255, 255, 255, 255));
            }
        }
        public override string ToString()
        {
            return $"EpsilonEngine.TextureRenderer()";
        }
    }
}