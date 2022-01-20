using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Epsilon
{
    public sealed class Particle : StageObject
    {
        private Vector2 _velocity = Vector2.Zero;
        private Vector2 _subPixel = Vector2.Zero;
        public Particle(Stage stage, Vector2 velocity) : base(stage)
        {
            Position = new Point(0, 0);
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.texture = Texture2D.FromFile(Epsilon.GraphicsDevice, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\Player.png");
            textureRenderer.offset = new Point(0, 0);
            AddComponent(textureRenderer);
            _velocity = velocity;
        }
        protected override void OnUpdate()
        {
            _subPixel += _velocity;
            Position = new Point((int)_subPixel.X, (int)_subPixel.Y);
        }
    }
}
