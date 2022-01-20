using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Epsilon
{
    public sealed class Rigidbody
    {
        private Vector2 subPixel = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;

        public readonly StageObject stageItem = null;
        public Rigidbody(StageObject stageItem)
        {
            if (stageItem is null)
            {
                throw new NullReferenceException();
            }
            this.stageItem = stageItem;
        }
        public void TickMovement(List<Collider> loadedColliders)
        {
            subPixel += velocity;
            Point targetMove = new Point((int)subPixel.X, (int)subPixel.Y);
            subPixel -= new Vector2((int)subPixel.X, (int)subPixel.Y);

            if (stageItem.collider != null && !stageItem.collider.trigger)
            {
                Rectangle thisColliderShape = stageItem.collider.GetWorldShape();
                if (targetMove.X > 0)
                {
                    for (int i = 0; i < loadedColliders.Count; i++)
                    {
                        if (loadedColliders[i] != stageItem.collider && !loadedColliders[i].trigger)
                        {
                            Rectangle otherColliderShape = loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.Y < otherColliderShape.max.Y && thisColliderShape.max.Y > otherColliderShape.min.Y)
                            {
                                if (thisColliderShape.min.X < otherColliderShape.max.X)
                                {
                                    int maxMove = MathHelper.Min(targetMove.X, otherColliderShape.min.X - thisColliderShape.max.X);
                                    if (maxMove != targetMove.X)
                                    {
                                        velocity.X = 0;
                                    }
                                    targetMove.X = MathHelper.Clamp(maxMove, 0, int.MaxValue);
                                }
                            }
                        }
                    }
                }
                else if (targetMove.X < 0)
                {
                    for (int i = 0; i < loadedColliders.Count; i++)
                    {
                        if (loadedColliders[i] != stageItem.collider && !loadedColliders[i].trigger)
                        {
                            Rectangle otherColliderShape = loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.Y < otherColliderShape.max.Y && thisColliderShape.min.Y > otherColliderShape.min.Y)
                            {
                                if (thisColliderShape.max.X > otherColliderShape.min.X)
                                {
                                    int maxMove = MathHelper.Max(targetMove.X, otherColliderShape.max.X - thisColliderShape.min.X);
                                    if (maxMove != targetMove.X)
                                    {
                                        velocity.X = 0;
                                    }
                                    targetMove.X = MathHelper.Clamp(maxMove, int.MinValue, 0);
                                }
                            }
                        }
                    }
                }
                stageItem.position.X += targetMove.X;
                thisColliderShape = stageItem.collider.GetWorldShape();
                if (targetMove.Y > 0)
                {
                    for (int i = 0; i < loadedColliders.Count; i++)
                    {
                        if (loadedColliders[i] != stageItem.collider && !loadedColliders[i].trigger)
                        {
                            Rectangle otherColliderShape = loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.X < otherColliderShape.max.X && thisColliderShape.max.X > otherColliderShape.min.X)
                            {
                                if (thisColliderShape.min.Y < otherColliderShape.max.Y)
                                {
                                    int maxMove = MathHelper.Min(targetMove.Y, otherColliderShape.min.Y - thisColliderShape.max.Y);
                                    if (maxMove != targetMove.Y)
                                    {
                                        velocity.Y = 0;
                                    }
                                    targetMove.Y = MathHelper.Clamp(maxMove, 0, int.MaxValue);
                                }
                            }
                        }
                    }
                }
                else if (targetMove.Y < 0)
                {
                    for (int i = 0; i < loadedColliders.Count; i++)
                    {
                        if (loadedColliders[i] != stageItem.collider && !loadedColliders[i].trigger)
                        {
                            Rectangle otherColliderShape = loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.X < otherColliderShape.max.X && thisColliderShape.max.X > otherColliderShape.min.X)
                            {
                                if (thisColliderShape.max.Y > otherColliderShape.min.Y)
                                {
                                    int maxMove = MathHelper.Max(targetMove.Y, otherColliderShape.max.Y - thisColliderShape.min.Y);
                                    if (maxMove != targetMove.Y)
                                    {
                                        velocity.Y = 0;
                                    }
                                    targetMove.Y = MathHelper.Clamp(maxMove, int.MinValue, 0);
                                }
                            }
                        }
                    }
                }
            }
            stageItem.position.Y += targetMove.Y;
        }
    }
}