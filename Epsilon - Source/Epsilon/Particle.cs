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
        private double _lifetime = 0;
        public Particle(Stage stage, Vector2 velocity, double lifetime) : base(stage)
        {
            Position = new Point(0, 0);
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.texture = Texture2D.FromFile(Epsilon.GraphicsDevice, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\GhostBlock.png");
            textureRenderer.offset = new Point(0, 0);
            AddComponent(textureRenderer);
            _velocity = velocity;
            _lifetime = lifetime;
        }
        protected override void OnUpdate()
        {
            _lifetime -= 0.1;
            if (_lifetime <= 0)
            {
                Stage.RemoveStageObject(this);
            }
            _subPixel += _velocity;
            Point targetMove = new Point((int)_subPixel.X, (int)_subPixel.Y);
            Position += targetMove;
            _subPixel -= new Vector2(targetMove.X, targetMove.Y);
        }
    }
}
