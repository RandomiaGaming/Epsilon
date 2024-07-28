using System.Collections.Generic;
namespace EpsilonEngine.Physics
{
    public sealed class Rigidbody : Component
    {
        private Vector2 subPixel = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        public Rigidbody(GameObject gameObject) : base(gameObject)
        {

        }
        public void TickMovement(List<Collider> loadedColliders)
        {
            subPixel += velocity;
            Vector2Int targetMove = new Vector2Int((int)subPixel.x, (int)subPixel.y);
            subPixel -= new Vector2((int)subPixel.x, (int)subPixel.y);

            if (gameObject.collider != null && !gameObject.collider.trigger)
            {
                RectangleInt thisColliderShape = gameObject.collider.GetWorldShape();
                if (targetMove.x > 0)
                {
                    for (int i = 0; i < loadedColliders.Count; i++)
                    {
                        if (loadedColliders[i] != gameObject.collider && !loadedColliders[i].trigger)
                        {
                            RectangleInt otherColliderShape = loadedColliders[i].GetShapeInWorldShape();
                            if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y)
                            {
                                if (thisColliderShape.min.x < otherColliderShape.max.x)
                                {
                                    int maxMove = MathHelper.Min(targetMove.x, otherColliderShape.min.x - thisColliderShape.max.x);
                                    if (maxMove != targetMove.x)
                                    {
                                        velocity.x = 0;
                                    }
                                    targetMove.x = MathHelper.Clamp(maxMove, 0, int.MaxValue);
                                }
                            }
                        }
                    }
                }
                else if (targetMove.x < 0)
                {
                    for (int i = 0; i < loadedColliders.Count; i++)
                    {
                        if (loadedColliders[i] != gameObject.collider && !loadedColliders[i].trigger)
                        {
                            RectangleInt otherColliderShape = loadedColliders[i].GetShapeInWorldShape();
                            if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y)
                            {
                                if (thisColliderShape.max.x > otherColliderShape.min.x)
                                {
                                    int maxMove = MathHelper.Max(targetMove.x, otherColliderShape.max.x - thisColliderShape.min.x);
                                    if (maxMove != targetMove.x)
                                    {
                                        velocity.x = 0;
                                    }
                                    targetMove.x = MathHelper.Clamp(maxMove, int.MinValue, 0);
                                }
                            }
                        }
                    }
                }
                gameObject.position.x += targetMove.x;
                thisColliderShape = gameObject.collider.GetWorldShape();
                if (targetMove.y > 0)
                {
                    for (int i = 0; i < loadedColliders.Count; i++)
                    {
                        if (loadedColliders[i] != gameObject.collider && !loadedColliders[i].trigger)
                        {
                            RectangleInt otherColliderShape = loadedColliders[i].GetShapeInWorldShape();
                            if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                            {
                                if (thisColliderShape.min.y < otherColliderShape.max.y)
                                {
                                    int maxMove = MathHelper.Min(targetMove.y, otherColliderShape.min.y - thisColliderShape.max.y);
                                    if (maxMove != targetMove.y)
                                    {
                                        velocity.y = 0;
                                    }
                                    targetMove.y = MathHelper.Clamp(maxMove, 0, int.MaxValue);
                                }
                            }
                        }
                    }
                }
                else if (targetMove.y < 0)
                {
                    for (int i = 0; i < loadedColliders.Count; i++)
                    {
                        if (loadedColliders[i] != gameObject.collider && !loadedColliders[i].trigger)
                        {
                            RectangleInt otherColliderShape = loadedColliders[i].GetShapeInWorldShape();
                            if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                            {
                                if (thisColliderShape.max.y > otherColliderShape.min.y)
                                {
                                    int maxMove = MathHelper.Max(targetMove.y, otherColliderShape.max.y - thisColliderShape.min.y);
                                    if (maxMove != targetMove.y)
                                    {
                                        velocity.y = 0;
                                    }
                                    targetMove.y = MathHelper.Clamp(maxMove, int.MinValue, 0);
                                }
                            }
                        }
                    }
                }
            }
            gameObject.position.y += targetMove.y;
        }
    }
}