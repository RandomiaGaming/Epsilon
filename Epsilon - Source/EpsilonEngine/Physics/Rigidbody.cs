using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace EpsilonEngine
{
    public sealed class Rigidbody : Component
    {
        private Vector2 subPixel = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        private Collider _collider = null;
        private PhysicsManager _physicsManager = null;
        public Rigidbody(GameObject gameObject) : base(gameObject)
        {

        }
        protected override void Initialize()
        {
            _physicsManager = Scene.GetSceneManager<PhysicsManager>();

            if (_physicsManager is null)
            {
                throw new Exception("Cannot use physics components in a scene without a physics manager.");
            }

            _collider = GameObject.GetComponent<Collider>();
        }
        protected override void Update()
        {
            subPixel += velocity;
            Point targetMove = new Point((int)subPixel.X, (int)subPixel.Y);
            subPixel -= new Vector2((int)subPixel.X, (int)subPixel.Y);

            if (targetMove == Point.Zero)
            {
                return;
            }

            if (_collider != null)
            {
                Rectangle thisColliderShape = _collider.GetWorldShape();

                foreach (Collider otherCollider in _physicsManager._managedColliders)
                {
                    if (otherCollider != _collider)
                    {
                        if (targetMove.X > 0)
                        {
                            Rectangle otherColliderShape = otherCollider.GetWorldShape();
                            if (otherColliderShape.Top <= thisColliderShape.Bottom && thisColliderShape.Top >= otherColliderShape.Bottom && otherColliderShape.Right >= thisColliderShape.Left)
                            {
                                int maxMove = otherColliderShape.Left - thisColliderShape.Right;
                                targetMove.X = MathHelper.Min(maxMove, targetMove.X);
                                if (targetMove.X == 0)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Rectangle otherColliderShape = otherCollider.GetWorldShape();
                            if (otherColliderShape.Top <= thisColliderShape.Bottom && thisColliderShape.Top <= otherColliderShape.Bottom && otherColliderShape.Left <= thisColliderShape.Right)
                            {
                                int maxMove = otherColliderShape.Right - thisColliderShape.Left;
                                targetMove.X = MathHelper.Max(maxMove, targetMove.X);
                                if (targetMove.X == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            GameObject.Position += new Point(targetMove.X, 0);

            if (_collider != null)
            {
                Rectangle thisColliderShape = _collider.GetWorldShape();

                foreach (Collider otherCollider in _physicsManager._managedColliders)
                {
                    if (otherCollider != _collider)
                    {
                        if (targetMove.Y > 0)
                        {
                            Rectangle otherColliderShape = otherCollider.GetWorldShape();
                            if (otherColliderShape.Left <= thisColliderShape.Right && otherColliderShape.Left <= thisColliderShape.Right && otherColliderShape.Top >= thisColliderShape.Bottom)
                            {
                                int maxMove = otherColliderShape.Bottom - thisColliderShape.Top;
                                targetMove.Y = MathHelper.Min(maxMove, targetMove.Y);
                                if (targetMove.Y == 0)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Rectangle otherColliderShape = otherCollider.GetWorldShape();
                            if (otherColliderShape.Left <= thisColliderShape.Right && otherColliderShape.Left <= thisColliderShape.Right && otherColliderShape.Bottom <= thisColliderShape.Top)
                            {
                                int maxMove = otherColliderShape.Top - thisColliderShape.Bottom;
                                targetMove.Y = MathHelper.Max(maxMove, targetMove.Y);
                                if (targetMove.Y == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            GameObject.Position += new Point(0, targetMove.Y);
        }
    }
}