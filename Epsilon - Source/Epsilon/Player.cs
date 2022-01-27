using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace EpsilonEngine
{
    public sealed class Player : GameObject
    {
        private VirtualInput jumpVirtualInput = null;
        private VirtualInput rightVirtualInput = null;
        private VirtualInput leftVirtualInput = null;
        private VirtualInput upVirtualInput = null;
        private VirtualInput downVirtualInput = null;
        public Player(Scene stage) : base(stage)
        {
            
        }
        private Vector2 _subPixelPosition = Vector2.Zero;
        private double moveSpeed = 0.1;
        protected override void Initialize()
        {
            jumpVirtualInput = Engine.InputManager.GetVirtualInputFromName("Jump");
            rightVirtualInput = Engine.InputManager.GetVirtualInputFromName("Right");
            leftVirtualInput = Engine.InputManager.GetVirtualInputFromName("Left");
            upVirtualInput = Engine.InputManager.GetVirtualInputFromName("Up");
            downVirtualInput = Engine.InputManager.GetVirtualInputFromName("Down");
        }
        protected override void Update()
        {
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
        }
    }
}
