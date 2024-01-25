using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class PhysicsObject : GameObject
    {
        #region Variables
        public List<PhysicsObject> _collisionsUp = new List<PhysicsObject>();
        public List<PhysicsObject> _collisionsRight = new List<PhysicsObject>();
        public List<PhysicsObject> _collisionsDown = new List<PhysicsObject>();
        public List<PhysicsObject> _collisionsLeft = new List<PhysicsObject>();
        public List<PhysicsObject> _overlaps = new List<PhysicsObject>();

        private bool _collisionsUpClean = true;
        private bool _collisionsRightClean = true;
        private bool _collisionsDownClean = true;
        private bool _collisionsLeftClean = true;
        private bool _overlapsClean = true;
        #endregion
        #region Properties
        public PhysicsScene PhysicsScene { get; private set; } = null;
        public PhysicsLayer PhysicsLayer { get; private set; } = null;
        public PhysicsLayer[] CollisionPhysicsLayers { get; set; } = null;
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
        //Bounciness is the percentage of the objects velocity that is reflected in a collision. Negative values indicate a traditional bounce.
        public float BouncynessUp { get; set; } = 0f;
        public float BouncynessLeft { get; set; } = 0f;
        public float BouncynessRight { get; set; } = 0f;
        public float BouncynessDown { get; set; } = 0f;
        //LocalColliderRect stores the colliders local shape. This will not change with the objects position.
        public int LocalColliderMinX { get; private set; } = 0;
        public int LocalColliderMinY { get; private set; } = 0;
        public int LocalColliderMaxX { get; private set; } = 0;
        public int LocalColliderMaxY { get; private set; } = 0;
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
        //SolidSides stores on which side the object blocks other objects from moving.
        public bool SolidUp { get; set; } = true;
        public bool SolidDown { get; set; } = true;
        public bool SolidLeft { get; set; } = true;
        public bool SolidRight { get; set; } = true;
        public DirectionInfo SolidSides
        {
            get
            {
                return new DirectionInfo(SolidRight, SolidUp, SolidLeft, SolidDown);
            }
            set
            {
                SolidRight = value.Right;
                SolidUp = value.Up;
                SolidLeft = value.Left;
                SolidDown = value.Down;
            }
        }
        //PhaseThroughSides stores in which directions the object can phase through other solid colliders.
        public bool PhaseThroughUp { get; set; } = false;
        public bool PhaseThroughDown { get; set; } = false;
        public bool PhaseThroughLeft { get; set; } = false;
        public bool PhaseThroughRight { get; set; } = false;
        public DirectionInfo PhaseThroughDirections
        {
            get
            {
                return new DirectionInfo(PhaseThroughRight, PhaseThroughUp, PhaseThroughLeft, PhaseThroughDown);
            }
            set
            {
                PhaseThroughRight = value.Right;
                PhaseThroughUp = value.Up;
                PhaseThroughLeft = value.Left;
                PhaseThroughDown = value.Down;
            }
        }
        //PushableSides stores in which directions the object can be pushed.
        public bool PushableUp { get; set; } = false;
        public bool PushableDown { get; set; } = false;
        public bool PushableLeft { get; set; } = false;
        public bool PushableRight { get; set; } = false;
        public DirectionInfo PushableDirections
        {
            get
            {
                return new DirectionInfo(PushableRight, PushableUp, PushableLeft, PushableDown);
            }
            set
            {
                PushableRight = value.Right;
                PushableUp = value.Up;
                PushableLeft = value.Left;
                PushableDown = value.Down;
            }
        }
        //PushOthersSides stores in which directions the object will push others.
        public bool PushOthersUp { get; set; } = false;
        public bool PushOthersDown { get; set; } = false;
        public bool PushOthersLeft { get; set; } = false;
        public bool PushOthersRight { get; set; } = false;
        public DirectionInfo PushOthersDirections
        {
            get
            {
                return new DirectionInfo(PushOthersRight, PushOthersUp, PushOthersLeft, PushOthersDown);
            }
            set
            {
                PushOthersRight = value.Right;
                PushOthersUp = value.Up;
                PushOthersLeft = value.Left;
                PushOthersDown = value.Down;
            }
        }
        //LogCollisionSides stores in which direction we record collisions to the collision list.
        public bool LogCollisionsUp { get; set; } = false;
        public bool LogCollisionsDown { get; set; } = false;
        public bool LogCollisionsLeft { get; set; } = false;
        public bool LogCollisionsRight { get; set; } = false;
        public DirectionInfo LogCollisionSides
        {
            get
            {
                return new DirectionInfo(LogCollisionsRight, LogCollisionsUp, LogCollisionsLeft, LogCollisionsDown);
            }
            set
            {
                LogCollisionsRight = value.Right;
                LogCollisionsUp = value.Up;
                LogCollisionsLeft = value.Left;
                LogCollisionsDown = value.Down;
            }
        }
        //LogOverlaps stores weather or not this object logs overlaps to the overlap list.
        public bool LogOverlaps = false;
        //DragableSides stores on which sides the object can be dragged by other objects.
        public bool DragableUp { get; set; } = false;
        public bool DragableDown { get; set; } = false;
        public bool DragableLeft { get; set; } = false;
        public bool DragableRight { get; set; } = false;
        public DirectionInfo DragableSides
        {
            get
            {
                return new DirectionInfo(DragableRight, DragableUp, DragableLeft, DragableDown);
            }
            set
            {
                DragableRight = value.Right;
                DragableUp = value.Up;
                DragableLeft = value.Left;
                DragableDown = value.Down;
            }
        }
        //DragOthersSides stores on which sides the object will latch on to and drag other objects.
        public bool DragOthersUp { get; set; } = false;
        public bool DragOthersDown { get; set; } = false;
        public bool DragOthersLeft { get; set; } = false;
        public bool DragOthersRight { get; set; } = false;
        public DirectionInfo DragOthersSides
        {
            get
            {
                return new DirectionInfo(DragOthersRight, DragOthersUp, DragOthersLeft, DragOthersDown);
            }
            set
            {
                DragOthersRight = value.Right;
                DragOthersUp = value.Up;
                DragOthersLeft = value.Left;
                DragOthersDown = value.Down;
            }
        }
        #endregion
        #region Constructors
        public PhysicsObject(PhysicsScene physicsScene, PhysicsLayer physicsLayer) : base(physicsScene)
        {
            if (physicsScene is null)
            {
                throw new Exception("physicsScene cannot be null.");
            }
            PhysicsScene = physicsScene;

            if (physicsLayer is null)
            {
                throw new Exception("physicsLayer cannot be null.");
            }
            if (physicsLayer.PhysicsScene != physicsScene)
            {
                throw new Exception("physicsScene cannot be null.");
            }
            PhysicsLayer = physicsLayer;

            Game.RegisterForUpdate(PhysicsUpdate);

            PhysicsScene.AddPhysicsObject(this);

            PhysicsLayer.AddPhysicsObject(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.PhysicsObject()";
        }
        #endregion
        #region Methods
        private bool _moving = false;

        public Point PhysicsMove(Point moveDistance)
        {
            int outputX;
            if (moveDistance.X < 0)
            {
                outputX = PhysicsMoveLeftUnsafe(moveDistance.X * -1);
            }
            else
            {
                outputX = PhysicsMoveRightUnsafe(moveDistance.X);
            }

            int outputY;
            if (moveDistance.Y < 0)
            {
                outputY = PhysicsMoveLeftUnsafe(moveDistance.Y * -1);
            }
            else
            {
                outputY = PhysicsMoveRightUnsafe(moveDistance.Y);
            }

            moveDistance.X = outputX;
            moveDistance.Y = outputY;
            return moveDistance;
        }

        public int PhysicsMoveXAxis(int moveDistance)
        {
            if (moveDistance == 0)
            {
                return 0;
            }
            else if (moveDistance < 0)
            {
                return PhysicsMoveLeftUnsafe(moveDistance * -1);
            }
            else
            {
                return PhysicsMoveRightUnsafe(moveDistance);
            }
        }
        public int PhysicsMoveYAxis(int moveDistance)
        {
            if (moveDistance == 0)
            {
                return 0;
            }
            else if (moveDistance < 0)
            {
                return PhysicsMoveDownUnsafe(moveDistance * -1);
            }
            else
            {
                return PhysicsMoveUpUnsafe(moveDistance);
            }
        }

        public int PhysicsMoveUp(int moveDistance)
        {
            if (moveDistance < 0)
            {
                throw new Exception("moveDistance must be greater than or equal to 0.");
            }
            return PhysicsMoveUpUnsafe(moveDistance);
        }
        public int PhysicsMoveDown(int moveDistance)
        {
            if (moveDistance < 0)
            {
                throw new Exception("moveDistance must be greater than or equal to 0.");
            }
            return PhysicsMoveDownUnsafe(moveDistance);
        }
        public int PhysicsMoveRight(int moveDistance)
        {
            if (moveDistance < 0)
            {
                throw new Exception("moveDistance must be greater than or equal to 0.");
            }
            return PhysicsMoveRightUnsafe(moveDistance);
        }
        public int PhysicsMoveLeft(int moveDistance)
        {
            if (moveDistance < 0)
            {
                throw new Exception("moveDistance must be greater than or equal to 0.");
            }
            return PhysicsMoveLeftUnsafe(moveDistance);
        }



        public int PhysicsMoveXAxisUnsafe(int moveDistance)
        {
            if (moveDistance < 0)
            {
                return PhysicsMoveLeftUnsafe(moveDistance * -1);
            }
            else
            {
                return PhysicsMoveRightUnsafe(moveDistance);
            }
        }
        public int PhysicsMoveYAxisUnsafe(int moveDistance)
        {
            if (moveDistance < 0)
            {
                return PhysicsMoveDownUnsafe(moveDistance * -1);
            }
            else
            {
                return PhysicsMoveUpUnsafe(moveDistance);
            }
        }

        public int PhysicsMoveUpUnsafe(int moveDistance)
        {
            if (_moving)
            {
                return 0;
            }

            _moving = true;

            if (CollisionPhysicsLayers is null || PhaseThroughUp)
            {
                PositionY += moveDistance;
                _moving = false;
                return moveDistance;
            }

            Rectangle thisColliderShape = WorldColliderRect;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collsionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collsionPhysicsObject != this && collsionPhysicsObject.SolidDown)
                    {
                        Rectangle otherColliderShape = collsionPhysicsObject.WorldColliderRect;

                        if (otherColliderShape.MinX > thisColliderShape.MaxX || otherColliderShape.MaxX < thisColliderShape.MinX)
                        {
                            //Ignore this physics object because it is too far to the left or right for a collision.
                        }
                        else if (otherColliderShape.MaxY < thisColliderShape.MinY)
                        {
                            //Ignore this physics object because it is too below us for a collision.
                        }
                        else if (otherColliderShape.MinY > thisColliderShape.MaxY)
                        {
                            //This physics object must be in front of us.
                            int maxMove = otherColliderShape.MinY - thisColliderShape.MaxY - 1;

                            if (maxMove < moveDistance && PushOthersUp && collsionPhysicsObject.PushableUp)
                            {
                                int pushRequest = moveDistance - maxMove;
                                int pushDistance = collsionPhysicsObject.PhysicsMoveUpUnsafe(pushRequest);
                                maxMove = maxMove + pushDistance;
                            }

                            if (maxMove > moveDistance)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                moveDistance = maxMove;
                            }
                        }
                        else
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            moveDistance = 0;
                        }
                    }
                }
            }

            _moving = false;

            PositionY += moveDistance;
            return moveDistance;
        }
        public int PhysicsMoveRightUnsafe(int moveDistance)
        {
            if (_moving)
            {
                return 0;
            }

            _moving = true;

            if (CollisionPhysicsLayers is null || PhaseThroughRight)
            {
                PositionX += moveDistance;
                _moving = false;
                return moveDistance;
            }

            Rectangle thisColliderShape = WorldColliderRect;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collsionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collsionPhysicsObject != this && collsionPhysicsObject.SolidLeft)
                    {
                        Rectangle otherColliderShape = collsionPhysicsObject.WorldColliderRect;

                        if (otherColliderShape.MinY > thisColliderShape.MaxY || otherColliderShape.MaxY < thisColliderShape.MinY)
                        {
                            //Ignore this physics object because it is too far to the left or right for a collision.
                        }
                        else if (otherColliderShape.MaxX < thisColliderShape.MinX)
                        {
                            //Ignore this physics object because it is too below us for a collision.
                        }
                        else if (otherColliderShape.MinX > thisColliderShape.MaxX)
                        {
                            //This physics object must be in front of us.
                            int maxMove = otherColliderShape.MinX - thisColliderShape.MaxX - 1;

                            if (maxMove < moveDistance && PushOthersRight && collsionPhysicsObject.PushableRight)
                            {
                                int pushRequest = moveDistance - maxMove;
                                int pushDistance = collsionPhysicsObject.PhysicsMoveRightUnsafe(pushRequest);
                                maxMove = maxMove + pushDistance;
                            }

                            if (maxMove > moveDistance)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                moveDistance = maxMove;
                            }
                        }
                        else
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            moveDistance = 0;
                        }
                    }
                }
            }

            _moving = false;

            PositionX += moveDistance;
            return moveDistance;
        }
        public int PhysicsMoveDownUnsafe(int moveDistance)
        {
            if (_moving)
            {
                return 0;
            }

            _moving = true;

            if (CollisionPhysicsLayers is null || PhaseThroughDown)
            {
                PositionY -= moveDistance;
                _moving = false;
                return moveDistance;
            }

            Rectangle thisColliderShape = WorldColliderRect;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collsionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collsionPhysicsObject != this && collsionPhysicsObject.SolidUp)
                    {
                        Rectangle otherColliderShape = collsionPhysicsObject.WorldColliderRect;

                        if (otherColliderShape.MinX > thisColliderShape.MaxX || otherColliderShape.MaxX < thisColliderShape.MinX)
                        {
                            //Ignore this physics object because it is too far to the left or right for a collision.
                        }
                        else if (otherColliderShape.MinY > thisColliderShape.MaxY)
                        {
                            //Ignore this physics object because it is too below us for a collision.
                        }
                        else if (otherColliderShape.MaxY < thisColliderShape.MinY)
                        {
                            //This physics object must be in front of us.
                            int maxMove = thisColliderShape.MinY - otherColliderShape.MaxY - 1;

                            if (maxMove < moveDistance && PushOthersDown && collsionPhysicsObject.PushableDown)
                            {
                                int pushRequest = moveDistance - maxMove;
                                int pushDistance = collsionPhysicsObject.PhysicsMoveDownUnsafe(pushRequest);
                                maxMove = maxMove + pushDistance;
                            }

                            if (maxMove > moveDistance)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                moveDistance = maxMove;
                            }
                        }
                        else
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            moveDistance = 0;
                        }
                    }
                }
            }

            _moving = false;

            PositionY -= moveDistance;
            return moveDistance;
        }
        public int PhysicsMoveLeftUnsafe(int moveDistance)
        {
            if (_moving)
            {
                return 0;
            }

            _moving = true;

            if (CollisionPhysicsLayers is null || PhaseThroughLeft)
            {
                PositionX -= moveDistance;
                _moving = false;
                return moveDistance;
            }

            Rectangle thisColliderShape = WorldColliderRect;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collsionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collsionPhysicsObject != this && collsionPhysicsObject.SolidRight)
                    {
                        Rectangle otherColliderShape = collsionPhysicsObject.WorldColliderRect;

                        if (otherColliderShape.MinY > thisColliderShape.MaxY || otherColliderShape.MaxY < thisColliderShape.MinY)
                        {
                            //Ignore this physics object because it is too far to the left or right for a collision.
                        }
                        else if (otherColliderShape.MinX > thisColliderShape.MaxX)
                        {
                            //Ignore this physics object because it is too below us for a collision.
                        }
                        else if (otherColliderShape.MaxX < thisColliderShape.MinX)
                        {
                            //This physics object must be in front of us.
                            int maxMove = thisColliderShape.MinX - otherColliderShape.MaxX - 1;

                            if (maxMove < moveDistance && PushOthersLeft && collsionPhysicsObject.PushableLeft)
                            {
                                int pushRequest = moveDistance - maxMove;
                                int pushDistance = collsionPhysicsObject.PhysicsMoveLeftUnsafe(pushRequest);
                                maxMove = maxMove + pushDistance;
                            }

                            if (maxMove > moveDistance)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                moveDistance = maxMove;
                            }
                        }
                        else
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            moveDistance = 0;
                        }
                    }
                }
            }

            _moving = false;

            PositionX -= moveDistance;
            return moveDistance;
        }
        #endregion
        #region Internals
        private void PhysicsUpdate()
        {
            if (VelocityY != 0)
            {
                SubPixelY += VelocityY;
                int targetMoveY = (int)SubPixelY;

                if (targetMoveY != 0)
                {
                    SubPixelY -= targetMoveY;

                    if (targetMoveY < 0)
                    {
                        if (PhysicsMoveDownUnsafe(targetMoveY * -1) != targetMoveY * -1)
                        {
                            VelocityY *= BouncynessDown;
                        }
                    }
                    else
                    {
                        if (PhysicsMoveUpUnsafe(targetMoveY) != targetMoveY)
                        {
                            VelocityY *= BouncynessUp;
                        }
                    }
                }
            }

            if (VelocityX != 0)
            {
                SubPixelX += VelocityX;
                int targetMoveX = (int)SubPixelX;

                if (targetMoveX != 0)
                {
                    SubPixelX -= targetMoveX;

                    if (targetMoveX < 0)
                    {
                        if (PhysicsMoveLeftUnsafe(targetMoveX * -1) != targetMoveX * -1)
                        {
                            VelocityX *= BouncynessLeft;
                        }
                    }
                    else
                    {
                        if (PhysicsMoveRightUnsafe(targetMoveX) != targetMoveX)
                        {
                            VelocityX *= BouncynessRight;
                        }
                    }
                }
            }

            if (LogCollisionsUp)
            {
                CheckCollisionsUp();
            }
            if (LogCollisionsRight)
            {
                CheckCollisionsRight();
            }
            if (LogCollisionsDown)
            {
                CheckCollisionsDown();
            }
            if (LogCollisionsLeft)
            {
                CheckCollisionsLeft();
            }
        }
        private void CheckCollisionsUp()
        {
            _collisionsUp = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || PhaseThroughUp)
            {
                return;
            }

            Rectangle thisColliderShape = WorldColliderRect;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collsionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collsionPhysicsObject != this && collsionPhysicsObject.SolidDown)
                    {
                        Rectangle otherColliderShape = collsionPhysicsObject.WorldColliderRect;

                        if (otherColliderShape.MinX <= thisColliderShape.MaxX && otherColliderShape.MaxX >= thisColliderShape.MinX && otherColliderShape.MinY == thisColliderShape.MaxY + 1)
                        {
                            _collisionsUp.Add(collsionPhysicsObject);
                        }
                    }
                }
            }
        }
        private void CheckCollisionsRight()
        {
            _collisionsRight = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || PhaseThroughRight)
            {
                return;
            }

            Rectangle thisColliderShape = WorldColliderRect;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collsionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collsionPhysicsObject != this && collsionPhysicsObject.SolidDown)
                    {
                        Rectangle otherColliderShape = collsionPhysicsObject.WorldColliderRect;

                        if (otherColliderShape.MinY <= thisColliderShape.MaxY && otherColliderShape.MaxY >= thisColliderShape.MinY && otherColliderShape.MinX == thisColliderShape.MaxX + 1)
                        {
                            _collisionsRight.Add(collsionPhysicsObject);
                        }
                    }
                }
            }
        }
        private void CheckCollisionsDown()
        {
            _collisionsDown = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || PhaseThroughDown)
            {
                return;
            }

            Rectangle thisColliderShape = WorldColliderRect;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collsionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collsionPhysicsObject != this && collsionPhysicsObject.SolidDown)
                    {
                        Rectangle otherColliderShape = collsionPhysicsObject.WorldColliderRect;

                        if (otherColliderShape.MinX <= thisColliderShape.MaxX && otherColliderShape.MaxX >= thisColliderShape.MinX && otherColliderShape.MaxY + 1 == thisColliderShape.MinY)
                        {
                            _collisionsDown.Add(collsionPhysicsObject);
                        }
                    }
                }
            }
        }
        private void CheckCollisionsLeft()
        {
            _collisionsLeft = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || PhaseThroughLeft)
            {
                return;
            }

            Rectangle thisColliderShape = WorldColliderRect;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collsionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collsionPhysicsObject != this && collsionPhysicsObject.SolidDown)
                    {
                        Rectangle otherColliderShape = collsionPhysicsObject.WorldColliderRect;

                        if (otherColliderShape.MinY <= thisColliderShape.MaxY && otherColliderShape.MaxY >= thisColliderShape.MinY && otherColliderShape.MaxX + 1 == thisColliderShape.MinX)
                        {
                            _collisionsLeft.Add(collsionPhysicsObject);
                        }
                    }
                }
            }
        }
        #endregion
    }
}