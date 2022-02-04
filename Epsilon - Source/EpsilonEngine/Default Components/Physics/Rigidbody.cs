﻿using System;
namespace EpsilonEngine
{
    public sealed class Rigidbody : Component
    {
        private Vector subPixel = Vector.Zero;
        public Vector velocity = Vector.Zero;
        public Vector bouncyness = Vector.Zero;
        public int CollisionPhysicsLayerIndex { get; private set; } = 0;
        private PhysicsLayer _physicsLayer = null;
        private Collider _collider = null;
        private PhysicsManager _physicsManager = null;
        public Rigidbody(GameObject gameObject, int collisionPhysicsLayerIndex) : base(gameObject)
        {
            CollisionPhysicsLayerIndex = collisionPhysicsLayerIndex;
        }
        protected override void Initialize()
        {
            _physicsManager = Scene.GetSceneManager<PhysicsManager>();

            if (_physicsManager is null)
            {
                throw new Exception("Cannot use physics components in a scene without a physics manager.");
            }

            _collider = GameObject.GetComponent<Collider>();

            _physicsLayer = _physicsManager.GetPhysicsLayer(CollisionPhysicsLayerIndex);
        }
        public override string ToString()
        {
            return $"EpsilonEngine.Rigidbody()";
        }
        protected override void Update()
        {
            subPixel += velocity;
            Point targetMove = new Point((int)subPixel.X, (int)subPixel.Y);

            if (targetMove.X == 0 && targetMove.Y == 0)
            {
                //No movement occured this frame so return.
                return;
            }

            subPixel -= new Vector((int)subPixel.X, (int)subPixel.Y);

            if (_collider is null)
            {
                //The object has no hitbox and can therefore move freely without fear of collisions and then return.
                GameObject.LocalPosition += targetMove;
                return;
            }

            Rectangle thisColliderShape = _collider.GetWorldShape();

            if (targetMove.X > 0)
            {
                //We are moving right along the x-axis.
                foreach (Collider otherCollider in _physicsLayer.ManagedColliders)
                {
                    if (otherCollider != _collider && otherCollider.SideCollision.Left)
                    {
                        Rectangle otherColliderShape = otherCollider.GetWorldShape();

                        if (otherColliderShape.MinY > thisColliderShape.MaxY || otherColliderShape.MaxY < thisColliderShape.MinY)
                        {
                            //Ignore because object is too high or low for a collision.
                        }
                        else if (otherColliderShape.MaxX < thisColliderShape.MinX)
                        {
                            //Ignore because object is behind us.
                        }
                        else if (otherColliderShape.MaxX >= thisColliderShape.MinX && otherColliderShape.MinX <= thisColliderShape.MaxX)
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            targetMove = new Point(0, targetMove.Y);
                            velocity.X = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in front of us so measure how far infront.
                            int maxMove = otherColliderShape.MinX - thisColliderShape.MaxX - 1;

                            if (maxMove > targetMove.X)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMove = new Point(maxMove, targetMove.Y); ;
                                velocity.X = velocity.X * bouncyness.X;

                                if (targetMove.X == 0)
                                {
                                    //No need to keep looping because we hit something so close that movement is impossible.
                                    break;
                                }
                            }
                        }
                    }
                }
                //Move the GameObject. 
                GameObject.LocalPosition = new Point(GameObject.LocalPosition.X + targetMove.X, GameObject.LocalPosition.Y);
                //Remember to update the collider shape because the object moved.
                thisColliderShape = _collider.GetWorldShape();
            }
            else if (targetMove.X < 0)
            {
                //By process of elimination We are moving left along the x-axis.
                foreach (Collider otherCollider in _physicsLayer.ManagedColliders)
                {
                    if (otherCollider != _collider && otherCollider.SideCollision.Right)
                    {
                        Rectangle otherColliderShape = otherCollider.GetWorldShape();

                        if (otherColliderShape.MinY > thisColliderShape.MaxY || otherColliderShape.MaxY < thisColliderShape.MinY)
                        {
                            //Ignore because object is too high or low for a collision.
                        }
                        else if (otherColliderShape.MinX > thisColliderShape.MaxX)
                        {
                            //Ignore because object is infront of us.
                        }
                        else if (otherColliderShape.MaxX >= thisColliderShape.MinX && otherColliderShape.MinX <= thisColliderShape.MaxX)
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            targetMove = new Point(0, targetMove.Y); ;
                            velocity.X = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in behind of us so measure how far behind.
                            int maxMove = ((thisColliderShape.MinX - otherColliderShape.MaxX) * -1) + 1;

                            if (maxMove < targetMove.X)
                            {
                                //Ignore this collision because the object we hit is too far in behind.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMove = new Point(maxMove, targetMove.Y); ;
                                velocity.X = velocity.X * bouncyness.X;

                                if (targetMove.X == 0)
                                {
                                    //No need to keep looping because we hit something so close that movement is impossible.
                                    break;
                                }
                            }
                        }
                    }
                }
                //Move the GameObject. 
                GameObject.LocalPosition = new Point(GameObject.LocalPosition.X + targetMove.X, GameObject.LocalPosition.Y);
                //Remember to update the collider shape because the object moved.
                thisColliderShape = _collider.GetWorldShape();
            }



            if (targetMove.Y > 0)
            {
                //We are moving right along the x-axis.
                foreach (Collider otherCollider in _physicsLayer.ManagedColliders)
                {
                    if (otherCollider != _collider && otherCollider.SideCollision.Bottom)
                    {
                        Rectangle otherColliderShape = otherCollider.GetWorldShape();

                        if (otherColliderShape.MinX > thisColliderShape.MaxX || otherColliderShape.MaxX < thisColliderShape.MinX)
                        {
                            //Ignore because object is too high or low for a collision.
                        }
                        else if (otherColliderShape.MaxY < thisColliderShape.MinY)
                        {
                            //Ignore because object is behind us.
                        }
                        else if (otherColliderShape.MaxY >= thisColliderShape.MinY && otherColliderShape.MinY <= thisColliderShape.MaxY)
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            targetMove = new Point(targetMove.X, 0);
                            velocity.Y = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in front of us so measure how far infront.
                            int maxMove = otherColliderShape.MinY - thisColliderShape.MaxY - 1;

                            if (maxMove > targetMove.Y)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMove = new Point(targetMove.X, maxMove);
                                velocity.Y = velocity.Y * bouncyness.Y;

                                if (targetMove.Y == 0)
                                {
                                    //No need to keep looping because we hit something so close that movement is impossible.
                                    break;
                                }
                            }
                        }
                    }
                }
                //Move the GameObject. 
                GameObject.LocalPosition = new Point(GameObject.LocalPosition.X, GameObject.LocalPosition.Y + targetMove.Y);
            }
            else if (targetMove.Y < 0)
            {
                //By process of elimination We are moving left along the x-axis.
                foreach (Collider otherCollider in _physicsLayer.ManagedColliders)
                {
                    if (otherCollider != _collider && otherCollider.SideCollision.Top)
                    {
                        Rectangle otherColliderShape = otherCollider.GetWorldShape();

                        if (otherColliderShape.MinX > thisColliderShape.MaxX || otherColliderShape.MaxX < thisColliderShape.MinX)
                        {
                            //Ignore because object is too high or low for a collision.
                        }
                        else if (otherColliderShape.MinY > thisColliderShape.MaxY)
                        {
                            //Ignore because object is infront of us.
                        }
                        else if (otherColliderShape.MaxY >= thisColliderShape.MinY && otherColliderShape.MinY <= thisColliderShape.MaxY)
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            targetMove = new Point(targetMove.X, 0);
                            velocity.Y = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in behind of us so measure how far behind.
                            int maxMove = ((thisColliderShape.MinY - otherColliderShape.MaxY) * -1) + 1;

                            if (maxMove < targetMove.Y)
                            {
                                //Ignore this collision because the object we hit is too far in behind.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMove = new Point(targetMove.X, maxMove);
                                velocity.Y = velocity.Y * bouncyness.Y;

                                if (targetMove.Y == 0)
                                {
                                    //No need to keep looping because we hit something so close that movement is impossible.
                                    break;
                                }
                            }
                        }
                    }
                }
                //Move the GameObject. 
                GameObject.LocalPosition = new Point(GameObject.LocalPosition.X, GameObject.LocalPosition.Y + targetMove.Y);
            }
        }
    }
}