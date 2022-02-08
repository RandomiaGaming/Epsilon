using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    throw new Exception("texture cannot be null.");
                }
                _texture = value;
            }
        }
        public byte R { get; set; } = 255;
        public byte G { get; set; } = 255;
        public byte B { get; set; } = 255;
        public byte A { get; set; } = 255;
        public Color Color
        {
            get
            {
                return new Color(R, G, B, A);
            }
            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
                A = value.A;
            }
        }
        public Image(Canvas canvas, Texture texture) : base(canvas)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            _texture = texture;

            Game.RegisterForRender(DrawTexture);
        }
        public Image(Canvas canvas, Element parent, Texture texture) : base(canvas, parent)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }
            _texture = texture;

            Game.RegisterForRender(DrawTexture);
        }
        internal void DrawTexture()
        {
            Game.DrawTextureUnsafe(Texture, ScreenMinX, ScreenMinY, ScreenMaxX, ScreenMaxY, R, G, B, A);
        }
    }
}
