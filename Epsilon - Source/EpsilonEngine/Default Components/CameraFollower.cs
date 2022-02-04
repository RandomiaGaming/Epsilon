using System;
using System.Collections.Generic;
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
        public override string ToString()
        {
            return $"EpsilonEngine.CameraFollower()";
        }
        protected override void Update()
        {
            Point goPos = GameObject.LocalPosition;
            if (goPos.X + Width >= GameObject.Scene.CameraPosition.X + GameObject.Scene.ViewPortSize.X - PaddingRight)
            {
                GameObject.Scene.CameraPosition = new Point(goPos.X + Width + PaddingRight - GameObject.Scene.ViewPortSize.X, GameObject.Scene.CameraPosition.Y);
            }

            if (goPos.X <= GameObject.Scene.CameraPosition.X + PaddingLeft)
            {
                GameObject.Scene.CameraPosition = new Point(goPos.X - PaddingLeft, GameObject.Scene.CameraPosition.Y);
            }



            if (goPos.Y + Height >= GameObject.Scene.CameraPosition.Y + GameObject.Scene.ViewPortSize.Y - PaddingUp)
            {
                GameObject.Scene.CameraPosition = new Point(GameObject.Scene.CameraPosition.X, goPos.Y + Height + PaddingUp - GameObject.Scene.ViewPortSize.Y);
            }

            if (goPos.Y <= GameObject.Scene.CameraPosition.Y + PaddingDown)
            {
                GameObject.Scene.CameraPosition = new Point(GameObject.Scene.CameraPosition.X, goPos.Y - PaddingDown);
            }
        }
    }
}