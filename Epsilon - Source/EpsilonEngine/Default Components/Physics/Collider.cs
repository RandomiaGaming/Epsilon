﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace EpsilonEngine
{
    public sealed class Collider : Component
    {
        public Rectangle shape = new Rectangle(Point.Zero, new Point(16, 16));
        private PhysicsManager _physicsManager = null;
        private int _physicsLayer = 0;
        public PhysicsManager PhysicsManager
        {
            get
            {
                return _physicsManager;
            }
        }
        public Collider(GameObject gameObject, PhysicsManager physicsManager, int physicsLayer) : base(gameObject)
        {
            if (physicsManager is null)
            {
                throw new Exception("physicsManager cannot be null.");
            }

            if (physicsManager.Scene != gameObject.Scene)
            {
                throw new Exception("physicsManager belongs to a different Scene.");
            }

            _physicsManager = physicsManager;

            _physicsManager.ManageCollider(this, physicsLayer);
            _physicsLayer = physicsLayer;
        }
        public Rectangle GetWorldShape()
        {
            return new Rectangle(shape.Min + GameObject.Position, shape.Max + GameObject.Position);
        }
    }
}