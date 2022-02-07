namespace EpsilonEngine
{
    public sealed class Image : Behavior
    {
        public Texture Texture { get; set; } = null;
        public Bounds bounds { get; set; } = Bounds.One;
        public Color Color { get; set; } = Color.White;
        public Image(Element element) : base(element)
        {

        }
        protected override void Render()
        {
            if (Texture is not null)
            {
                Element.DrawTextureLocalSpaceUnsafe(Texture, bounds.MinX, bounds.MinY, bounds.MaxX, bounds.MaxY, Color.R, Color.G, Color.B, Color.A);
            }
        }
        public override string ToString()
        {
            return $"EpsilonEngine.Image()";
        }
    }
}