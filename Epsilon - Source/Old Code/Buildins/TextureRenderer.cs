namespace EpsilonEngine
{
    public sealed class TextureRenderer : Component
    {
        public Texture texture;
        public Point offset = Point.Zero;
        public TextureRenderer(GameObject gameObject) : base(gameObject)
        {

        }
        protected override void Update()
        {
            gameObject.position.x += 1;
            gameObject.position.x = MathHelper.LoopClamp(gameObject.position.x, 0, 100);
        }
        protected override RenderTexture Render()
        {
            RenderTexture output = new RenderTexture();
            output.Draw(texture, offset);
            return output;
        }
    }
}
