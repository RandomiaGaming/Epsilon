using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class PhysicsObject : GameObject
    {
        #region Properties
        public PhysicsScene PhysicsScene { get; private set; } = null;
        public PhysicsLayer ThisPhysicsLayer { get; private set; } = null;
        public PhysicsLayer[] CollisionPhysicsLayers { get; private set; } = null;
        //Velocity is the objects move speed over time in pixels per frame.
        public float VelocityX { get; set; } = 0f;
        public float VelocityY { get; set; } = 0f;
        public Vector Velocity
        {
            get
            {
                return new Vector(VelocityX, VelocityY);
            }
            set
            {
                VelocityX = value.X;
                VelocityY = value.Y;
            }
        }
        //Bounciness is the percentage of the objects velocity that is reflected in a collision. Negative values indicate a traditional bounce.
        public float BouncynessX { get; set; } = 0f;
        public float BouncynessY { get; set; } = 0f;
        public Vector Bouncyness
        {
            get
            {
                return new Vector(BouncynessX, BouncynessY);
            }
            set
            {
                BouncynessX = value.X;
                BouncynessY = value.Y;
            }
        }
        //Subpixel stores how close the object is to moving another pixel.
        public float SubPixelX { get; private set; } = 0f;
        public float SubPixelY { get; private set; } = 0f;
        public Vector SubPixel
        {
            get
            {
                return new Vector(SubPixelX, SubPixelY);
            }
        }
        //LocalColliderRect stores the colliders local shape. This will not change with the objects position.
        public int LocalColliderMinX { get; set; } = 0;
        public int LocalColliderMinY { get; set; } = 0;
        public int LocalColliderMaxX { get; set; } = 0;
        public int LocalColliderMaxY { get; set; } = 0;
        public Rectangle LocalColliderRect
        {
            get
            {
                return new Rectangle(LocalColliderMinX, LocalColliderMinY, LocalColliderMaxX, LocalColliderMaxY);
            }
            set
            {
                LocalColliderMinX = value.MinX;
                LocalColliderMinY = value.MinY;
                LocalColliderMaxX = value.MaxX;
                LocalColliderMaxY = value.MaxY;
            }
        }
        //WorldColliderRect stores the colliders world shape. This will change with the objects position.
        public int WorldColliderMinX
        {
            get
            {
                return LocalColliderMinX + PositionX;
            }
        }
        public int WorldColliderMinY
        {
            get
            {
                return LocalColliderMinY + PositionY;
            }
        }
        public int WorldColliderMaxX
        {
            get
            {
                return LocalColliderMaxX + PositionX;
            }
        }
        public int WorldColliderMaxY
        {
            get
            {
                return LocalColliderMaxY + PositionY;
            }
        }
        public Rectangle WorldColliderRect
        {
            get
            {
                return new Rectangle(WorldColliderMinX, WorldColliderMinY, WorldColliderMaxX, WorldColliderMaxY);
            }
        }
        //SolidSides stores on which side the object blocks others from moving.
        public bool SolidTop { get; set; } = true;
        public bool SolidBottom { get; set; } = true;
        public bool SolidLeft { get; set; } = true;
        public bool SolidRight { get; set; } = true;
        public DirectionInfo SolidSides
        {
            get
            {
                return new DirectionInfo(SolidRight, SolidTop, SolidLeft, SolidBottom);
            }
            set
            {
                SolidRight = value.Right;
                SolidTop = value.Top;
                SolidLeft = value.Left;
                SolidBottom = value.Bottom;
            }
        }
        //PhaseThroughSides stores which sides the objects can phase through other solid colliders.
        public bool PhaseThroughTop { get; set; } = false;
        public bool PhaseThroughBottom { get; set; } = false;
        public bool PhaseThroughLeft { get; set; } = false;
        public bool PhaseThroughRight { get; set; } = false;
        public DirectionInfo PhaseThroughSides
        {
            get
            {
                return new DirectionInfo(PhaseThroughRight, PhaseThroughTop, PhaseThroughLeft, PhaseThroughBottom);
            }
            set
            {
                PhaseThroughRight = value.Right;
                PhaseThroughTop = value.Top;
                PhaseThroughLeft = value.Left;
                PhaseThroughBottom = value.Bottom;
            }
        }
        //PushableSides stores which directions the object can be pushed along.
        public bool PushableTop { get; set; } = false;
        public bool PushableBottom { get; set; } = false;
        public bool PushableLeft { get; set; } = false;
        public bool PushableRight { get; set; } = false;
        public DirectionInfo PushableSides
        {
            get
            {
                return new DirectionInfo(PushableRight, PushableTop, PushableLeft, PushableBottom);
            }
            set
            {
                PushableRight = value.Right;
                PushableTop = value.Top;
                PushableLeft = value.Left;
                PushableBottom = value.Bottom;
            }
        }
        //PushOthersSides stores in which directions the object will push others.
        public bool PushOthersTop { get; set; } = false;
        public bool PushOthersBottom { get; set; } = false;
        public bool PushOthersLeft { get; set; } = false;
        public bool PushOthersRight { get; set; } = false;
        public DirectionInfo PushOthersSides
        {
            get
            {
                return new DirectionInfo(PushOthersRight, PushOthersTop, PushOthersLeft, PushOthersBottom);
            }
            set
            {
                PushOthersRight = value.Right;
                PushOthersTop = value.Top;
                PushOthersLeft = value.Left;
                PushOthersBottom = value.Bottom;
            }
        }
        //DragableSides stores which directions the object can be dragged.
        public bool DragableTop { get; set; } = false;
        public bool DragableBottom { get; set; } = false;
        public bool DragableLeft { get; set; } = false;
        public bool DragableRight { get; set; } = false;
        public DirectionInfo DragableSides
        {
            get
            {
                return new DirectionInfo(DragableRight, DragableTop, DragableLeft, DragableBottom);
            }
            set
            {
                DragableRight = value.Right;
                DragableTop = value.Top;
                DragableLeft = value.Left;
                DragableBottom = value.Bottom;
            }
        }
        //DragOthersSides stores in which directions the object will drag others.
        public bool DragOthersTop { get; set; } = false;
        public bool DragOthersBottom { get; set; } = false;
        public bool DragOthersLeft { get; set; } = false;
        public bool DragOthersRight { get; set; } = false;
        public DirectionInfo DragOthersSides
        {
            get
            {
                return new DirectionInfo(DragOthersRight, DragOthersTop, DragOthersLeft, DragOthersBottom);
            }
            set
            {
                DragOthersRight = value.Right;
                DragOthersTop = value.Top;
                DragOthersLeft = value.Left;
                DragOthersBottom = value.Bottom;
            }
        }
        #endregion
        public PhysicsObject(PhysicsScene physicsScene, int physicsLayer, List<int> collisionPhysicsLayers) : base(physicsScene)
        {
            PhysicsScene = physicsScene;


            CollisionPhysicsLayerIndex = collisionPhysicsLayerIndex;
        }
        public override string ToString()
        {
            return $"EpsilonEngine.Rigidbody()";
        }
        protected override void Update()
        {
            SubPixelX += VelocityX;
            SubPixelY += VelocityY;
            int targetMoveX = (int)SubPixelX;
            int targetMoveY = (int)SubPixelY;

            if (targetMoveX == 0 && targetMoveY == 0)
            {
                //No movement occured this frame so return.
                return;
            }

            _subPixelX -= (int)_subPixelX;
            _subPixelY -= (int)_subPixelY;

            if (_collider is null)
            {
                //The object has no hitbox and can therefore move freely without fear of collisions and then return.
                GameObject.PositionX += targetMoveX;
                GameObject.PositionY += targetMoveY;
                return;
            }

            Rectangle thisColliderShape = _collider.GetWorldShape();

            if (targetMoveX > 0)
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
                            targetMoveX = 0;
                            VelocityX = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in front of us so measure how far infront.
                            int maxMove = otherColliderShape.MinX - thisColliderShape.MaxX - 1;

                            if (maxMove > targetMoveX)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMoveX = maxMove;
                                VelocityX *= BouncynessX;

                                if (targetMoveX == 0)
                                {
                                    //No need to keep looping because we hit something so close that movement is impossible.
                                    break;
                                }
                            }
                        }
                    }
                }
                //Move the GameObject.
                GameObject.PositionX += targetMoveX;
                //Remember to update the collider shape because the object moved.
                thisColliderShape = _collider.GetWorldShape();
            }
            else if (targetMoveX < 0)
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
                            targetMoveX = 0;
                            VelocityX = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in behind of us so measure how far behind.
                            int maxMove = ((thisColliderShape.MinX - otherColliderShape.MaxX) * -1) + 1;

                            if (maxMove < targetMoveX)
                            {
                                //Ignore this collision because the object we hit is too far in behind.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMoveX = maxMove;
                                VelocityX *= BouncynessX;

                                if (targetMoveX == 0)
                                {
                                    //No need to keep looping because we hit something so close that movement is impossible.
                                    break;
                                }
                            }
                        }
                    }
                }
                //Move the GameObject. 
                GameObject.PositionX += targetMoveX;
                //Remember to update the collider shape because the object moved.
                thisColliderShape = _collider.GetWorldShape();
            }



            if (targetMoveY > 0)
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
                            targetMoveY = 0;
                            VelocityY = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in front of us so measure how far infront.
                            int maxMove = otherColliderShape.MinY - thisColliderShape.MaxY - 1;

                            if (maxMove > targetMoveY)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMoveY = maxMove;
                                VelocityY *= BouncynessY;

                                if (targetMoveY == 0)
                                {
                                    //No need to keep looping because we hit something so close that movement is impossible.
                                    break;
                                }
                            }
                        }
                    }
                }
                //Move the GameObject. 
                GameObject.PositionY += targetMoveY;
            }
            else if (targetMoveY < 0)
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
                            targetMoveY = 0;
                            VelocityY = 0;
                            break;
                        }
                        else
                        {
                            //By process of elimination we know the object must be in behind of us so measure how far behind.
                            int maxMove = ((thisColliderShape.MinY - otherColliderShape.MaxY) * -1) + 1;

                            if (maxMove < targetMoveY)
                            {
                                //Ignore this collision because the object we hit is too far in behind.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                targetMoveY = maxMove;
                                VelocityY *= BouncynessY;

                                if (targetMoveY == 0)
                                {
                                    //No need to keep looping because we hit something so close that movement is impossible.
                                    break;
                                }
                            }
                        }
                    }
                }
                //Move the GameObject. 
                GameObject.PositionY += targetMoveY;
            }
        }
    }
}