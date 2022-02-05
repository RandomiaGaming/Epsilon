using System;
namespace EpsilonEngine
{
    public sealed class TextureRenderer : Component
    {
        public Texture Texture { get; set; } = null;
        public Point Offset { get; set; } = new Point(0, 0);
        public Color Color { get; set; } = Color.White;
        public TextureRenderer(GameObject gameObject) : base(gameObject)
        {

        }
        protected override void Render()
        {
            if (Texture is not null)
            {
                GameObject.DrawTextureLocalSpaceUnsafe(Texture, Offset.X, Offset.Y, Color.R, Color.G, Color.B, Color.A);
            }
        }
        public override string ToString()
        {
            return $"EpsilonEngine.TextureRenderer()";
        }
    }
}