using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public sealed class TextureRenderer : Component
    {
        public Texture2D Texture = null;
        public Point Offset = new Point(0, 0);
        public Color Color = Color.White;
        public TextureRenderer(StageObject stageObject) : base(stageObject)
        {

        }
        protected override void OnRender()
        {
            StageObject.DrawTexture(Texture, Offset, Color);
        }
    }
}