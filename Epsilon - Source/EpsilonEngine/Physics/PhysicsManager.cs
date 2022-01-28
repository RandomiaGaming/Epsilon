using System;
using System.Collections.Generic;
using EpsilonEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpsilonEngine
{
    public sealed class PhysicsManager : SceneManager
    {
        public List<Collider> _managedColliders = new List<Collider>();
        public PhysicsManager(Scene scene) : base(scene)
        {

        }
    }
}
