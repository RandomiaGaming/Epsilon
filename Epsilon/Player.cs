using EpsilonEngine;
namespace Epsilon
{
    public sealed class Player : PhysicsObject
    {
        public Player(StagePlayer stagePlayer, PhysicsLayer physicsLayer, PhysicsLayer[] collsionPhysicsLayers, Texture texture) : base(stagePlayer, physicsLayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = texture;

            LocalColliderRect = new Rectangle(0, 0, 15, 15);

            PushableUp = false;
            PushableDown = false;
            PushableLeft = false;
            PushableRight = false;

            PushOthersUp = true;
            PushOthersDown = true;
            PushOthersLeft = true;
            PushOthersRight = true;

            LogCollisionsUp = true;
            LogCollisionsRight = true;
            LogCollisionsDown = true;
            LogCollisionsLeft = true;

            CollisionPhysicsLayers = collsionPhysicsLayers;
        }
        protected override void Update()
        {
            PhysicsMoveXAxis(Scene.WorldMousePositionX - PositionX - 8);
            PhysicsMoveYAxis(Scene.WorldMousePositionY - PositionY - 8);

            foreach(PhysicsObject physicsObject in _collisionsUp)
            {
                physicsObject.CollisionPhysicsLayers = null;
            }
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