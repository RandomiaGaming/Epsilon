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
        private VirtualInput upVirtualInput = null;
        private VirtualInput downVirtualInput = null;
        public Player(Stage stage) : base(stage)
        {
            
        }
        private Vector2 _subPixelPosition = Vector2.Zero;
        private double moveSpeed = 0.1;
        protected override void OnUpdate()
        {
            jumpVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Jump");
            rightVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Right");
            leftVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Left");
            upVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Up");
            downVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Down");

            int horizontalAxis = 0;

            if (rightVirtualInput.Pressed && !leftVirtualInput.Pressed)
            {
                horizontalAxis = 1;
            }
            else if (leftVirtualInput.Pressed && !rightVirtualInput.Pressed)
            {
                horizontalAxis = -1;
            }

            int vericalAxis = 0;

            if (upVirtualInput.Pressed && !downVirtualInput.Pressed)
            {
                vericalAxis = 1;
            }
            else if (downVirtualInput.Pressed && !upVirtualInput.Pressed)
            {
                vericalAxis = -1;
            }

            _subPixelPosition += new Vector2((float)(horizontalAxis * moveSpeed), (float)(vericalAxis * moveSpeed));

            Position = new Point((int)_subPixelPosition.X, (int)_subPixelPosition.Y);

            Stage.CameraPosition = Position - new Point(Stage.ViewportSize.X / 2, Stage.ViewportSize.Y / 2) + new Point(8, 8);
        }
    }
}
