using System;
namespace Epsilon
{
    public enum StageObjectTag { Player, Ground, Hazzard, Untagged }
    public abstract class StageObject
    {
        public Point position = Point.Zero;
        public Texture texture = null;

        public StageObjectTag tag = StageObjectTag.Untagged;

        public Rigidbody rigidbody = null;
        public Collider collider = null;
        public CollisionLogger collisionLogger = null;

        public readonly StagePlayer stagePlayer = null;
        public StageObject(StagePlayer stagePlayer)
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
