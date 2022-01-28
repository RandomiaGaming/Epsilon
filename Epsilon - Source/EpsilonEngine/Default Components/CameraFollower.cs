using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace EpsilonEngine
{
    public sealed class CameraFollower : Component
    {
        public int Width = 0;
        public int Height = 0;
        public int PaddingRight = 0;
        public int PaddingLeft = 0;
        public int PaddingUp = 0;
        public int PaddingDown = 0;
        public CameraFollower(GameObject gameObject) : base(gameObject)
        {

        }
        protected override void Update()
        {
            if (GameObject.Position.X + Width >= GameObject.Scene.CameraPosition.X + GameObject.Scene.ViewPortSize.X - PaddingRight)
            {
                GameObject.Scene.CameraPosition = new Point(GameObject.Position.X + Width + PaddingRight - GameObject.Scene.ViewPortSize.X, GameObject.Scene.CameraPosition.Y);
            }

            if (GameObject.Position.X <= GameObject.Scene.CameraPosition.X + PaddingLeft)
            {
                GameObject.Scene.CameraPosition = new Point(GameObject.Position.X - PaddingLeft, GameObject.Scene.CameraPosition.Y);
            }



            if (GameObject.Position.Y + Height >= GameObject.Scene.CameraPosition.Y + GameObject.Scene.ViewPortSize.Y - PaddingUp)
            {
                GameObject.Scene.CameraPosition = new Point(GameObject.Scene.CameraPosition.X, GameObject.Position.Y + Height + PaddingUp - GameObject.Scene.ViewPortSize.Y);
            }

            if (GameObject.Position.Y <= GameObject.Scene.CameraPosition.Y + PaddingDown)
            {
                GameObject.Scene.CameraPosition = new Point(GameObject.Scene.CameraPosition.X, GameObject.Position.Y - PaddingDown);
            }
        }
    }
}