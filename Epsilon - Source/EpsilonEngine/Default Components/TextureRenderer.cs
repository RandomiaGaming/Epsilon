using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace EpsilonEngine
{
    public sealed class TextureRenderer : Component
    {
        public Texture2D Texture = null;
        public Point Offset = new Point(0, 0);
        public Color Color = Color.White;
        public TextureRenderer(GameObject stageObject) : base(stageObject)
        {

        }
        protected override void Render()
        {
            GameObject.DrawTexture(Texture, Offset, Color);
        }
    }
}