﻿using System;
namespace EpsilonEngine
{
    public sealed class Collision
    {
        public readonly Collider OtherCollider = null;
        public readonly GameObject OtherGameObject = null;
        public readonly Collider ThisCollider = null;
        public readonly GameObject ThisGameObject = null;
        public readonly SideInfo SideInfo = SideInfo.False;
        public Collision(Collider thisCollider, Collider otherCollider, SideInfo sideInfo)
        {
            if (thisCollider is null)
            {
                throw new Exception("thisCollider cannot be null.");
            }
            ThisCollider = thisCollider;
            ThisGameObject = thisCollider.GameObject;

            if (otherCollider is null)
            {
                throw new Exception("otherCollider cannot be null.");
            }
            OtherCollider = otherCollider;
            OtherGameObject = otherCollider.GameObject;

            SideInfo = sideInfo;
        }
        public Collision Invert()
        {
            return new Collision(OtherCollider, ThisCollider, SideInfo.Invert());
        }
    }
}
