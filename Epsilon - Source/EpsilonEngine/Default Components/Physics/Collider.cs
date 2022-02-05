﻿using System;
namespace EpsilonEngine
{
    public sealed class Collider : Component
    {
        public Rectangle Rect { get; set; } = new Rectangle(Point.Zero, new Point(15, 15));
        public PhysicsManager PhysicsManager { get; private set; } = null;
        public int PhysicsLayerIndex { get; private set; } = 0;
        public SideInfo SideCollision { get; set; } = SideInfo.True;
        public Collider(GameObject gameObject, PhysicsManager physicsManager, int physicsLayerIndex) : base(gameObject)
        {
            if (physicsManager is null)
            {
                throw new Exception("physicsManager cannot be null.");
            }

            if (physicsManager.Scene != gameObject.Scene)
            {
                throw new Exception("physicsManager belongs to a different Scene.");
            }

            PhysicsManager = physicsManager;

            PhysicsLayerIndex = physicsLayerIndex;

            PhysicsManager.ManageCollider(this);
        }
        public override string ToString()
        {
            return $"EpsilonEngine.Collider({PhysicsManager}, {PhysicsLayerIndex})";
        }
        public Rectangle GetWorldShape()
        {
            int gameObjectWorldPositionX = GameObject.WorldPositionX;
            int gameObjectWorldPositionY = GameObject.WorldPositionY;

            return new Rectangle(Rect.MinX + gameObjectWorldPositionX, Rect.MinY + gameObjectWorldPositionY, Rect.MaxX + gameObjectWorldPositionX, Rect.MaxY + gameObjectWorldPositionY);
        }
    }
}