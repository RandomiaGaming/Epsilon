using System.Collections.Generic;
namespace DontMelt
{
    public sealed class Player : StageItem
    {
        private Texture facingRight = null;
        private Texture facingLeft = null;

        private SideInfo touchingGround = SideInfo.False;

        private const double moveForce = 5.3333;
        private const double jumpForce = 2.2666;
        private const double maxMoveSpeed = 1.7333;
        private static readonly Vector2 wallJumpForce = new Vector2(2.2666, 1.6);
        private const double dragForce = 2.1333;
        private const double gravityForce = 2.6151;

        public Player(StagePlayer stagePlayer) : base(stagePlayer)
        {
            collider = new Collider(this)
            {
                trigger = false,
                shape = new Rectangle(new Point(2, 2), new Point(14, 14)),
                sideCollision = SideInfo.True,
            };

            rigidbody = new Rigidbody(this)
            {
                velocity = Vector2.Zero,
            };

            tag = StageItemTag.Player;

            collisionLogger = new CollisionLogger(this);

            position = new Point(128, 72);

            TextureAsset playerSpriteSheet = (TextureAsset)AssetHelper.LoadAsset("Player.png");
            facingLeft = TextureHelper.SubTexture(playerSpriteSheet.data, new Rectangle(new Point(0, 0), new Point(16, 16)));
            facingRight = TextureHelper.SubTexture(playerSpriteSheet.data, new Rectangle(new Point(16, 0), new Point(32, 16)));
            texture = facingRight;
        }
        public override void Update()
        {
            rigidbody.velocity.y -= gravityForce / 60;
            Collision();
            Move();
            Jump();
            Drag();
            stagePlayer.cameraPosition = position - (StagePlayer.viewPortPixelRect / 2) + new Point(8, 8);
        }
        private void Jump()
        {
            if (stagePlayer.inputManager.jumpDown)
            {
                if (touchingGround.bottom)
                {
                    rigidbody.velocity.y = jumpForce;
                }
                else if (touchingGround.left)
                {
                    rigidbody.velocity = wallJumpForce;
                }
                else if (touchingGround.right)
                {
                    rigidbody.velocity = wallJumpForce * new Vector2(-1, 1);
                }
            }
        }

        private void Move()
        {
            if (stagePlayer.inputManager.moveAxis == 1d)
            {
                if (rigidbody.velocity.x < maxMoveSpeed)
                {
                    rigidbody.velocity.x += moveForce / 60;
                }
                texture = facingRight;
            }
            else if (stagePlayer.inputManager.moveAxis == -1)
            {
                if (rigidbody.velocity.x > -maxMoveSpeed)
                {
                    rigidbody.velocity.x += -moveForce / 60;
                }
                texture = facingLeft;
            }
        }

        private void Drag()
        {
            if (rigidbody.velocity.x > 0)
            {
                rigidbody.velocity.x -= dragForce / 60;
                rigidbody.velocity.x = MathHelper.Clamp(rigidbody.velocity.x, 0, double.MaxValue);
            }
            else if (rigidbody.velocity.x < 0)
            {
                rigidbody.velocity.x -= -dragForce / 60;
                rigidbody.velocity.x = MathHelper.Clamp(rigidbody.velocity.x, double.MinValue, 0);
            }
        }

        private void Collision()
        {
            bool touchingGroundOnBottom = false;
            bool touchingGroundOnTop = false;
            bool touchingGroundOnLeft = false;
            bool touchingGroundOnRight = false;
            foreach (Collision collision in collisionLogger.collisions)
            {
                if (collision.otherStageItem.tag == StageItemTag.Hazzard)
                {
                    stagePlayer.playerIsDead = true;
                }
                else if (collision.otherStageItem.tag == StageItemTag.Ground)
                {
                    if (collision.sideInfo.bottom)
                    {
                        touchingGroundOnBottom = true;
                    }
                    if (collision.sideInfo.top)
                    {
                        touchingGroundOnTop = true;
                    }
                    if (collision.sideInfo.left)
                    {
                        touchingGroundOnLeft = true;
                    }
                    if (collision.sideInfo.right)
                    {
                        touchingGroundOnRight = true;
                    }
                }
            }
            touchingGround = new SideInfo(touchingGroundOnTop, touchingGroundOnBottom, touchingGroundOnLeft, touchingGroundOnRight);
        }
    }
}