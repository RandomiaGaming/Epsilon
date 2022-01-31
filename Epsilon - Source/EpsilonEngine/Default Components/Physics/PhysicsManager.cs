using System;
using System.Collections.Generic;
using EpsilonEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpsilonEngine
{
    public sealed class PhysicsManager : SceneManager
    {
        private List<PhysicsLayer> _physicsLayers = new List<PhysicsLayer>();
        public PhysicsManager(Scene scene) : base(scene)
        {

        }
        public List<Collider> GetPhysicsLayer(int physicsLayer)
        {
            foreach(PhysicsLayer pl in _physicsLayers)
            {
                if(pl.Index == physicsLayer)
                {
                    return pl.Colliders;
                }
            }
            return null;
        }
        internal void ManageCollider(Collider collider, int collisionLayer)
        {
            if(collider is null)
            {
                throw new Exception("collider cannot be null.");
            }
            if(collider.PhysicsManager != this)
            {
                throw new Exception("collider belongs to a difference PhysicsManager.");
            }
            bool foundPhysicsLayer = false;
            foreach(PhysicsLayer physicsLayer in _physicsLayers)
            {
                if(physicsLayer.Index == collisionLayer)
                {
                    physicsLayer.Colliders.Add(collider);
                    foundPhysicsLayer = true;
                    break;
                }
            }
            if (!foundPhysicsLayer)
            {
                PhysicsLayer physicsLayer = new PhysicsLayer();
                physicsLayer.Index = collisionLayer;
                physicsLayer.Colliders.Add(collider);
                _physicsLayers.Add(physicsLayer);
            }
        }
    }
}
