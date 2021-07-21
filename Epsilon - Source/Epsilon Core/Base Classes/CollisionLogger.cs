using System;
using System.Collections.Generic;
namespace DontMelt
{
    public sealed class CollisionLogger
    {
        public List<Collision> collisions { get; private set; } = new List<Collision>();
        public List<Overlap> overlaps { get; private set; } = new List<Overlap>();

        public readonly StageItem stageItem = null;
        public CollisionLogger(StageItem stageItem)
        {
            if (stageItem is null)
            {
                throw new NullReferenceException();
            }
            this.stageItem = stageItem;
        }
        public void LogCollisions(List<Collider> loadedColliders)
        {
            collisions = new List<Collision>();
            overlaps = new List<Overlap>();

            if (stageItem.collider == null)
            {
                return;
            }
            Rectangle thisColliderShape = stageItem.collider.GetWorldShape();

            foreach (Collider loadedCollider in loadedColliders)
            {
                if (loadedCollider != stageItem.collider)
                {
                    Rectangle otherColliderShape = loadedCollider.GetWorldShape();
                    if (loadedCollider.trigger || stageItem.collider.trigger)
                    {
                        if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                        {
                            overlaps.Add(new Overlap(stageItem.collider, loadedCollider));
                        }
                    }
                    else
                    {
                        if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                        {
                            collisions.Add(new Collision(stageItem.collider, loadedCollider, SideInfo.True));
                        }
                        else if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.max.x == otherColliderShape.min.x)
                        {
                            collisions.Add(new Collision(stageItem.collider, loadedCollider, new SideInfo(false, false, false, true)));
                        }
                        else if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x == otherColliderShape.max.x)
                        {
                            collisions.Add(new Collision(stageItem.collider, loadedCollider, new SideInfo(false, false, true, false)));
                        }
                        else if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x && thisColliderShape.max.y == otherColliderShape.min.y)
                        {
                            collisions.Add(new Collision(stageItem.collider, loadedCollider, new SideInfo(true, false, false, false)));
                        }
                        else if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x && thisColliderShape.min.y == otherColliderShape.max.y)
                        {
                            collisions.Add(new Collision(stageItem.collider, loadedCollider, new SideInfo(false, true, false, false)));
                        }
                    }
                }
            }
        }
    }
}