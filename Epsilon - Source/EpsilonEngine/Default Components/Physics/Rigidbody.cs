using System;
namespace EpsilonEngine
{
    public sealed class Rigidbody : Component
    {
        #region Variables
        private float _subPixelX = 0f;
        private float _subPixelY = 0f;
        private PhysicsLayer _physicsLayer = null;
        private Collider _collider = null;
        private PhysicsManager _physicsManager = null;
        #endregion
        #region Properties
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
        public int CollisionPhysicsLayerIndex { get; private set; } = 0;
        #endregion
        public Rigidbody(GameObject gameObject, int collisionPhysicsLayerIndex) : base(gameObject)
        {
            CollisionPhysicsLayerIndex = collisionPhysicsLayerIndex;

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
            _subPixelX += VelocityX;
            _subPixelY += VelocityY;
            int targetMoveX = (int)_subPixelX;
            int targetMoveY = (int)_subPixelY;

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
                GameObject.LocalPositionX += targetMoveX;
                GameObject.LocalPositionY += targetMoveY;
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
                GameObject.LocalPositionX += targetMoveX;
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
                GameObject.LocalPositionX += targetMoveX;
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
                GameObject.LocalPositionY += targetMoveY;
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
                GameObject.LocalPositionY += targetMoveY;
            }
        }
    }
}