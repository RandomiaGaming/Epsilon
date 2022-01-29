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

            if (targetMove.X == 0 && targetMove.Y == 0)
            {
                //No movement occured this frame so return.
                return;
            }

            if (_collider is null)
            {
                //The object has no hitbox and can therefore move freely without fear of collisions and then return.
                GameObject.Position += targetMove;
                return;
            }

            Rectangle thisColliderShape = _collider.GetWorldShape();

            if (targetMove.X > 0)
            {
                //We are moving right along the x-axis.
                foreach (Collider otherCollider in _physicsManager._managedColliders)
                {
                    if (otherCollider == _collider)
                    {
                        //We can safely ignore this collider because it belongs to us.
                    }
                    else
                    {
                        Rectangle otherColliderShape = otherCollider.GetWorldShape();

                        if (otherColliderShape.min.Y > thisColliderShape.max.Y || otherColliderShape.max.Y < thisColliderShape.min.Y)
                        {
                            //Ignore because object is too high or low for a collision.
                        }
                        else if (otherColliderShape.max.X < thisColliderShape.min.X)
                        {
                            //Ignore because object is behind us.
                        }
                        else if (otherColliderShape.max.X >= thisColliderShape.min.X && otherColliderShape.min.X <= thisColliderShape.max.X)
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            targetMove.X = 0;
                            velocity.X = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in front of us so measure how far infront.
                            int maxMove = otherColliderShape.min.X - thisColliderShape.max.X - 1;

                            if (maxMove > targetMove.X)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMove.X = maxMove;
                                velocity.X = 0;

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
                GameObject.Position = new Point(GameObject.Position.X + targetMove.X, GameObject.Position.Y);
                //Remember to update the collider shape because the object moved.
                thisColliderShape = _collider.GetWorldShape();
            }
            else if(targetMove.X < 0)
            {
                //By process of elimination We are moving left along the x-axis.
                foreach (Collider otherCollider in _physicsManager._managedColliders)
                {
                    if (otherCollider == _collider)
                    {
                        //We can safely ignore this collider because it belongs to us.
                    }
                    else
                    {
                        Rectangle otherColliderShape = otherCollider.GetWorldShape();

                        if (otherColliderShape.min.Y > thisColliderShape.max.Y || otherColliderShape.max.Y < thisColliderShape.min.Y)
                        {
                            //Ignore because object is too high or low for a collision.
                        }
                        else if (otherColliderShape.min.X > thisColliderShape.max.X)
                        {
                            //Ignore because object is infront of us.
                        }
                        else if (otherColliderShape.max.X >= thisColliderShape.min.X && otherColliderShape.min.X <= thisColliderShape.max.X)
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            targetMove.X = 0;
                            velocity.X = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in behind of us so measure how far behind.
                            int maxMove = ((thisColliderShape.min.X - otherColliderShape.max.X) * -1) + 1;

                            if (maxMove < targetMove.X)
                            {
                                //Ignore this collision because the object we hit is too far in behind.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMove.X = maxMove;
                                velocity.X = 0;

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
                GameObject.Position = new Point(GameObject.Position.X + targetMove.X, GameObject.Position.Y);
                //Remember to update the collider shape because the object moved.
                thisColliderShape = _collider.GetWorldShape();
            }



            if (targetMove.Y > 0)
            {
                //We are moving right along the x-axis.
                foreach (Collider otherCollider in _physicsManager._managedColliders)
                {
                    if (otherCollider == _collider)
                    {
                        //We can safely ignore this collider because it belongs to us.
                    }
                    else
                    {
                        Rectangle otherColliderShape = otherCollider.GetWorldShape();

                        if (otherColliderShape.min.X > thisColliderShape.max.X || otherColliderShape.max.X < thisColliderShape.min.X)
                        {
                            //Ignore because object is too high or low for a collision.
                        }
                        else if (otherColliderShape.max.Y < thisColliderShape.min.Y)
                        {
                            //Ignore because object is behind us.
                        }
                        else if (otherColliderShape.max.Y >= thisColliderShape.min.Y && otherColliderShape.min.Y <= thisColliderShape.max.Y)
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            targetMove.Y = 0;
                            velocity.Y = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in front of us so measure how far infront.
                            int maxMove = otherColliderShape.min.Y - thisColliderShape.max.Y - 1;

                            if (maxMove > targetMove.Y)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMove.Y = maxMove;
                                velocity.Y = 0;

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
                GameObject.Position = new Point(GameObject.Position.X, GameObject.Position.Y + targetMove.Y);
            }
            else if (targetMove.Y < 0)
            {
                //By process of elimination We are moving left along the x-axis.
                foreach (Collider otherCollider in _physicsManager._managedColliders)
                {
                    if (otherCollider == _collider)
                    {
                        //We can safely ignore this collider because it belongs to us.
                    }
                    else
                    {
                        Rectangle otherColliderShape = otherCollider.GetWorldShape();

                        if (otherColliderShape.min.X > thisColliderShape.max.X || otherColliderShape.max.X < thisColliderShape.min.X)
                        {
                            //Ignore because object is too high or low for a collision.
                        }
                        else if (otherColliderShape.min.Y > thisColliderShape.max.Y)
                        {
                            //Ignore because object is infront of us.
                        }
                        else if (otherColliderShape.max.Y >= thisColliderShape.min.Y && otherColliderShape.min.Y <= thisColliderShape.max.Y)
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            targetMove.Y = 0;
                            velocity.Y = velocity.Y * -1;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in behind of us so measure how far behind.
                            int maxMove = ((thisColliderShape.min.Y - otherColliderShape.max.Y) * -1) + 1;

                            if (maxMove < targetMove.Y)
                            {
                                //Ignore this collision because the object we hit is too far in behind.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMove.Y = maxMove;
                                velocity.Y = velocity.Y *-1;

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
                GameObject.Position = new Point(GameObject.Position.X, GameObject.Position.Y + targetMove.Y);
            }
        }
    }
}