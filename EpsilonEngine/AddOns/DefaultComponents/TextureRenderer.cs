namespace EpsilonEngine
{
    public sealed class TextureRenderer : Component
    {
        public Texture Texture;
        public Point Offset;

        private Microsoft.Xna.Framework.Color _XNAColorCache = Microsoft.Xna.Framework.Color.White;

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                _XNAColorCache = new Microsoft.Xna.Framework.Color(value.R, value.G, value.B, value.A);
            }
        }
        private Color _color;
        public TextureRenderer(GameObject gameObject, int renderPriority) : base(gameObject, 0, renderPriority)
        {

        }
        protected override void Render()
        {
            if (Texture is not null)
            {
                int positionX = Offset.X - Scene.CameraPositionX + GameObject.PositionX;
                int positionY = Scene.RenderHeight - Offset.Y + Scene.CameraPositionY - GameObject.PositionY - Texture.Height;
                if (positionX < -Texture.Width || positionY + Texture.Height < 0 || positionX > Scene.RenderWidth || positionY > Scene.RenderHeight)
                {
                    return;
                }
                Scene.XNASpriteBatch.Draw(Texture._xnaTexture, new Microsoft.Xna.Framework.Vector2(positionX, positionY), _XNAColorCache);
            }
        }
        public override string ToString()
        {
            return $"EpsilonEngine.TextureRenderer()";
        }
    }
}