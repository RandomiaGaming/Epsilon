using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Epsilon
{
    public sealed class Player : StageObject
    {
        private VirtualInput jumpVirtualInput = null;
        private VirtualInput rightVirtualInput = null;
        private VirtualInput leftVirtualInput = null;
        public Player(Stage stage) : base(stage)
        {
            Position = new Point(0, 0);
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.texture = Texture2D.FromFile(Engine.GraphicsDevice, @"C:\Users\RandomiaGaming\Documents\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\LavaBubble.png");
            textureRenderer.offset = new Point(0, 0);
            AddComponent(textureRenderer);

            jumpVirtualInput = Engine.InputManager.GetVirtualInputFromName("Jump");
            rightVirtualInput = Engine.InputManager.GetVirtualInputFromName("Right");
            leftVirtualInput = Engine.InputManager.GetVirtualInputFromName("Left");
        }
        protected override void update()
        {
            int moveAxis = 0;
            if (rightVirtualInput.Pressed && !leftVirtualInput.Pressed)
            {
                moveAxis = 1;
            }
            else if (leftVirtualInput.Pressed && !rightVirtualInput.Pressed)
            {
                moveAxis = -1;
            }

            Position = new Point(Position.X + moveAxis, Position.Y);
        }
    }
}
