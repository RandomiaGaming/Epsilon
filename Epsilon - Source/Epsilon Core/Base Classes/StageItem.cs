using System;
namespace DontMelt
{
    public enum StageItemTag { Player, Ground, Hazzard, Untagged }
    public abstract class StageItem
    {
        public Point position = Point.Zero;
        public Texture texture = null;

        public StageItemTag tag = StageItemTag.Untagged;

        public Rigidbody rigidbody = null;
        public Collider collider = null;
        public CollisionLogger collisionLogger = null;

        public readonly StagePlayer stagePlayer = null;
        public StageItem(StagePlayer stagePlayer)
        {
            if (stagePlayer is null)
            {
                throw new NullReferenceException();
            }
            this.stagePlayer = stagePlayer;
        }
        public abstract void Update();
    }
}
