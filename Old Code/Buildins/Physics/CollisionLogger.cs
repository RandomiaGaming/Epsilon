using System.Collections.Generic;
namespace EpsilonEngine.Physics
{
    public sealed class CollisionLogger : Component
    {
        public List<Collision> collisions { get; private set; } = new List<Collision>();
        public List<Overlap> overlaps { get; private set; } = new List<Overlap>();

        public CollisionLogger(GameObject gameObject) : base(gameObject)
        {

        }
        public void LogCollisions(List<Collider> loadedColliders)
        {
            collisions = new List<Collision>();
            overlaps = new List<Overlap>();

            if (gameObject.collider == null)
            {
                return;
            }
            RectangleInt thisColliderShape = gameObject.collider.GetWorldShape();

            foreach (Collider loadedCollider in loadedColliders)
            {
                if (loadedCollider != gameObject.collider)
                {
                    RectangleInt otherColliderShape = loadedCollider.GetShapeInWorldShape();
                    if (loadedCollider.trigger || gameObject.collider.trigger)
                    {
                        if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                        {
                            overlaps.Add(new Overlap(gameObject.collider, loadedCollider));
                        }
                    }
                    else
                    {
                        if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                        {
                            collisions.Add(new Collision(gameObject.collider, loadedCollider, SideInfo.True));
                        }
                        else if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.max.x == otherColliderShape.min.x)
                        {
                            collisions.Add(new Collision(gameObject.collider, loadedCollider, new SideInfo(false, false, false, true)));
                        }
                        else if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x == otherColliderShape.max.x)
                        {
                            collisions.Add(new Collision(gameObject.collider, loadedCollider, new SideInfo(false, false, true, false)));
                        }
                        else if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x && thisColliderShape.max.y == otherColliderShape.min.y)
                        {
                            collisions.Add(new Collision(gameObject.collider, loadedCollider, new SideInfo(true, false, false, false)));
                        }
                        else if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x && thisColliderShape.min.y == otherColliderShape.max.y)
                        {
                            collisions.Add(new Collision(gameObject.collider, loadedCollider, new SideInfo(false, true, false, false)));
                        }
                    }
                }
            }
        }
    }
}