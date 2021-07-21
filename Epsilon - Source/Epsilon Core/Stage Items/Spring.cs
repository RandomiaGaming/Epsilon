using System.Collections.Generic;
namespace DontMelt
{
    public sealed class Spring : StageItem
    {
        private Texture down = null;
        private Texture up = null;
        private long upTimer = 0;
        public Spring(StagePlayer stagePlayer) : base(stagePlayer)
        {
            collider = new Collider(this)
            {
                trigger = true,
                shape = new Rectangle(Point.Zero, new Point(16, 6)),
                sideCollision = SideInfo.True,
            };

            rigidbody = new Rigidbody(this)
            {
                velocity = Vector2.Zero,
            };

            collisionLogger = new CollisionLogger(this);

            position = Point.Zero;

            TextureAsset springSpriteSheet = (TextureAsset)AssetHelper.LoadAsset("Spring.png");
            down = TextureHelper.SubTexture(springSpriteSheet.data, new Rectangle(new Point(0, 0), new Point(16, 16)));
            up = TextureHelper.SubTexture(springSpriteSheet.data, new Rectangle(new Point(16, 0), new Point(32, 16)));
            texture = down;
        }
        public override void Update()
        {
            foreach (Overlap o in collisionLogger.overlaps)
            {
                if (o.otherStageItem != null && o.otherStageItem.GetType().IsAssignableFrom(typeof(Player)))
                {
                    o.otherStageItem.rigidbody.velocity.y = 3;
                    upTimer = 30;
                }
            }

            if (upTimer > 0)
            {
                upTimer--;
                texture = up;
            }
            else
            {
                texture = down;
                upTimer = 0;
            }
        }
    }
}