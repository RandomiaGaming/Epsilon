using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace EpsilonEngine
{
    public sealed class TextureRenderer : Component
    {
        private Texture _texture = null;
        public Point Offset = new Point(0, 0);
        public Color Color = Color.White;
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
                    throw new Exception("Texture cannot be null.");
                }

                _texture = value;
            }
        }
        public TextureRenderer(GameObject stageObject, Texture texture) : base(stageObject)
        {
            if (texture is null)
            {
                throw new Exception("texture cannot be null.");
            }

            _texture = texture;
        }
        protected override void Render()
        {
            GameObject.DrawTexture(_texture, Offset, Color);
        }
    }
}