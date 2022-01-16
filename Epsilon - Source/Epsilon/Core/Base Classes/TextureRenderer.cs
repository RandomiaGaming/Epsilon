using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public sealed class TextureRenderer : Component
    {
        public Texture2D texture;
        public TextureRenderer(StageObject stageObject) : base(stageObject)
        {
            texture = Texture2D.FromFile(stageObject.Engine.GraphicsDevice, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\NoJump.png");
        }
        protected override List<DrawInstruction> render()
        {
            return new List<DrawInstruction>() { new DrawInstruction(texture, Point.Zero) };
        }
    }
}