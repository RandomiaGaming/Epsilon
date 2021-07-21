namespace DontMelt
{
    public sealed class BounceBox : StageItem
    {
        private const double BounceForce = 2.5f;
        public BounceBox(StagePlayer stagePlayer) : base(stagePlayer)
        {
            texture = ((TextureAsset)AssetHelper.LoadAsset("OnOffSpike.png")).data;
            collider = new Collider(this);
            rigidbody = null;
            collisionLogger = new CollisionLogger(this);
        }
        public override void Update()
        {
            foreach (Collision collision in collisionLogger.collisions)
            {
                if (collision.otherStageItem.tag == StageItemTag.Player)
                {
                    if (collision.sideInfo.top)
                    {
                        collision.otherStageItem.rigidbody.velocity.y = BounceForce;
                    }
                    else if (collision.sideInfo.bottom)
                    {
                        collision.otherStageItem.rigidbody.velocity.y = -BounceForce;
                    }
                    else if (collision.sideInfo.right)
                    {
                        collision.otherStageItem.rigidbody.velocity.x = BounceForce;
                    }
                    else if (collision.sideInfo.left)
                    {
                        collision.otherStageItem.rigidbody.velocity.x = -BounceForce;
                    }
                    return;
                }
            }
        }
    }
}