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

        private Rigidbody _rigidbody = null;
        private float moveSpeed = 0.05f;
        public Player(Scene stage) : base(stage)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this, new Texture(Engine, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\LavaBubble.png"));
            CameraFollower cameraFollower = new CameraFollower(this);
            cameraFollower.Width = 16;
            cameraFollower.Height = 16;
            cameraFollower.PaddingRight = 16;
            cameraFollower.PaddingLeft = 16;
            cameraFollower.PaddingUp = 16;
            cameraFollower.PaddingDown = 16;
            Rigidbody rigidbody = new Rigidbody(this);
            Collider collider = new Collider(this);
            collider.shape = new Rectangle(0, 0, 16, 16);
        }
        public override string ToString()
        {
            return $"Epsilon.Player({Position})";
        }
        protected override void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
            jumpVirtualInput = Engine.InputManager.GetVirtualInputFromName("Jump");
            rightVirtualInput = Engine.InputManager.GetVirtualInputFromName("Right");
            leftVirtualInput = Engine.InputManager.GetVirtualInputFromName("Left");
            upVirtualInput = Engine.InputManager.GetVirtualInputFromName("Up");
            downVirtualInput = Engine.InputManager.GetVirtualInputFromName("Down");
        }
        protected override void Update()
        {
            float horizontalAxis = 0;

            if (rightVirtualInput.Pressed && !leftVirtualInput.Pressed)
            {
                horizontalAxis = 1;
            }
            else if (leftVirtualInput.Pressed && !rightVirtualInput.Pressed)
            {
                horizontalAxis = -1;
            }

            float vericalAxis = 0;

            if (upVirtualInput.Pressed && !downVirtualInput.Pressed)
            {
                vericalAxis = 1;
            }
            else if (downVirtualInput.Pressed && !upVirtualInput.Pressed)
            {
                vericalAxis = -1;
            }

            _rigidbody.velocity = new Vector2(horizontalAxis * moveSpeed, vericalAxis * moveSpeed);
        }
    }
}
