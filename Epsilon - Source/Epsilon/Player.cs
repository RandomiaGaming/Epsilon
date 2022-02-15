using EpsilonEngine;
namespace Epsilon
{
    public sealed class Player : PhysicsObject
    {
        public Player(StagePlayer stagePlayer, PhysicsLayer physicsLayer, PhysicsLayer collsionPhysicsLayer, Texture texture) : base(stagePlayer, physicsLayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = texture;
            /*PositionX = RandomnessHelper.NextInt(16, StagePlayer.ViewPortWidth - 32);
            PositionY = RandomnessHelper.NextInt(16, StagePlayer.ViewPortHeight - 32);
            double rot = RandomnessHelper.NextDouble(0, System.Math.PI * 2);
            VelocityX = (float)System.Math.Cos(rot) * 0.1f;
            VelocityY = (float)System.Math.Sin(rot) * 0.1f;*/

            LocalColliderMinX = 0;
            LocalColliderMinY = 0;
            LocalColliderMaxX = 15;
            LocalColliderMaxY = 15;

            PushableUp = false;
            PushableDown = false;
            PushableLeft = false;
            PushableRight = false;

            PushOthersUp = true;
            PushOthersDown = true;
            PushOthersLeft = true;
            PushOthersRight = true;

            /* BouncynessUp = -1.0f;
             BouncynessDown = -1.0f;
             BouncynessRight = -1.0f;
             BouncynessLeft = -1.0f;*/

            CollisionPhysicsLayer = collsionPhysicsLayer;
        }
        protected override void Update()
        {
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                Scene.CameraPositionY += 2;
            }
            else if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                Scene.CameraPositionY -= 2;
            }

            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                Scene.CameraPositionX += 2;
            }
            else if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                Scene.CameraPositionX -= 2;
            }

            PhysicsMoveXAxis(Scene.WorldMousePositionX - PositionX);
            PhysicsMoveYAxis(Scene.WorldMousePositionY - PositionY);
        }
        public override string ToString()
        {
            return $"Epsilon.Player()";
        }
    }
}






/*using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace EpsilonEngine
{
    public sealed class Player : GameObject
    {
        public float gravityForce = -0.00008f;
        private VirtualInput jumpVirtualInput = null;
        private VirtualInput rightVirtualInput = null;
        private VirtualInput leftVirtualInput = null;
        private VirtualInput upVirtualInput = null;
        private VirtualInput downVirtualInput = null;

        private Rigidbody _rigidbody = null;
        private float moveSpeed = 0.00005f;
        public Player(Scene stage, PhysicsManager physicsManager) : base(stage)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this, new Texture(Engine, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\LavaBubble.png"));
            _rigidbody = new Rigidbody(this, 0);
            Collider collider = new Collider(this, physicsManager, 1);
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

            if (jumpVirtualInput.Pressed)
            {
                _rigidbody.velocity.Y = 0.1f;
            }

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.X + (horizontalAxis * moveSpeed), _rigidbody.velocity.Y + gravityForce);

            Scene.CameraPosition = Position - new Point(Scene.ViewPortSize.X / 2, Scene.ViewPortSize.Y / 2) + new Point(8, 8);
        }
    }
}
*/