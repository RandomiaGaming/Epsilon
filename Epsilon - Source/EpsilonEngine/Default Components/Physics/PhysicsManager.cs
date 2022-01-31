using System;
using System.Collections.Generic;
using EpsilonEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpsilonEngine
{
    public sealed class PhysicsManager : SceneManager
    {
        private Collider[] _managedColliders = new Collider[0];
        public PhysicsManager(Scene scene) : base(scene)
        {

        }
        public Collider[] GetManagedColliders()
        {
            return _managedColliders;
        }
        public override string ToString()
        {
            return $"EpsilonEngine.PhysicsManager()";
        }
        internal void ManageCollider(Collider collider)
        {
            if (collider is null)
            {
                throw new Exception("collider cannot be null.");
            }

            if (collider.PhysicsManager != this)
            {
                throw new Exception("collider belongs to a difference PhysicsManager.");
            }

            Collider[] old = _managedColliders;
            _managedColliders = new Collider[_managedColliders.Length + 1];
            Array.Copy(old, 0, _managedColliders, 0, old.Length);
            _managedColliders[_managedColliders.Length - 1] = collider;
        }
    }
}
