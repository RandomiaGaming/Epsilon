namespace EpsilonEngine
{
    public sealed class TextureRenderer : Component
    {
        public Texture Texture { get; set; } = null;
        public int OffsetX { get; set; } = 0;
        public int OffsetY { get; set; } = 0;
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
        public byte ColorR { get; set; } = byte.MaxValue;
        public byte ColorG { get; set; } = byte.MaxValue;
        public byte ColorB { get; set; } = byte.MaxValue;
        public byte ColorA { get; set; } = byte.MaxValue;
        public Color Color
        {
            get
            {
                return new Color(ColorR, ColorG, ColorB, ColorA);
            }
            set
            {
                ColorR = value.R;
                ColorG = value.G;
                ColorB = value.B;
                ColorA = value.A;
            }
        }
        public TextureRenderer(GameObject gameObject) : base(gameObject)
        {

        }
        protected override void Render()
        {
           if (Texture is not null)
            {
                GameObject.DrawTextureLocalSpaceUnsafe(Texture, OffsetX, OffsetY, ColorR, ColorG, ColorB, ColorA);
            }
        }
        public override string ToString()
        {
            return $"EpsilonEngine.TextureRenderer()";
        }
    }
}