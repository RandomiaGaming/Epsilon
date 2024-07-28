namespace EpsilonEngine
{
    public class Image : Element
    {
        private Texture _texture = null;
        public Texture Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                if (value is null)
                {
                    throw new System.Exception("texture cannot be null.");
                }
                _texture = value;
            }
        }
        public byte ColorR { get; set; } = 255;
        public byte ColorG { get; set; } = 255;
        public byte ColorB { get; set; } = 255;
        public byte ColorA { get; set; } = 255;
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
        public Image(Canvas canvas, Texture texture) : base(canvas)
        {
            if (texture is null)
            {
                throw new System.Exception("texture cannot be null.");
            }
            _texture = texture;
        }
        public Image(Canvas canvas, Element parent, Texture texture) : base(canvas, parent)
        {
            if (texture is null)
            {
                throw new System.Exception("texture cannot be null.");
            }
            _texture = texture;
        }
       protected sealed override void Render()
        {
            //Game.DrawTextureUnsafe(Texture, ScreenMinX, ScreenMinY, ScreenMaxX, ScreenMaxY, ColorR, ColorG, ColorB, ColorA);
        }
    }
}