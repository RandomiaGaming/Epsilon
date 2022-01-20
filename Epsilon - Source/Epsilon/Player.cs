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
            stage.CameraPosition = new Point(256 / -2, 144 / -2);

            Position = new Point(0, 0);
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.texture = Texture2D.FromFile(Epsilon.GraphicsDevice, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\LavaBubble.png");
            textureRenderer.offset = new Point(0, 0);
            AddComponent(textureRenderer);

            jumpVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Jump");
            rightVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Right");
            leftVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Left");
            upVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Up");
            downVirtualInput = Epsilon.InputManager.GetVirtualInputFromName("Down");
        }
        private Vector2 _subPixelPosition = Vector2.Zero;
        private double moveSpeed = 0.03;
        protected override void OnUpdate()
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

            if (jumpVirtualInput.Pressed)
            {
                double particleDirection = RandomnessHelper.NextDouble() * 2.0 * Math.PI;
                Vector2 particleVelocity = new Vector2((float)Math.Cos(particleDirection), (float)Math.Sin(particleDirection));
                Stage.AddStageObject(new Particle(Stage, particleVelocity));
            }
        }
    }
}
