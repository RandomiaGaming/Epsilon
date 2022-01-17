using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public sealed class TextureRenderer : Component
    {
        public Texture2D texture = null;
        public Point offset = new Point(0, 0);
        public TextureRenderer(StageObject stageObject) : base(stageObject)
        {

        }
        protected override List<DrawInstruction> render()
        {
            return new List<DrawInstruction>() { new DrawInstruction(texture, offset) };
        }
    }
}