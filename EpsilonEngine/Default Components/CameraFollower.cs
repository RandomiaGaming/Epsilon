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
            if (GameObject.PositionX + Width >= GameObject.Scene.CameraPositionX + GameObject.Scene.Width - PaddingRight)
            {
                GameObject.Scene.CameraPositionX = GameObject.PositionX + Width + PaddingRight - GameObject.Scene.Width;
            }
            else if (GameObject.PositionX <= GameObject.Scene.CameraPositionX + PaddingLeft)
            {
                GameObject.Scene.CameraPositionX = GameObject.PositionX - PaddingLeft;
            }

            if (GameObject.PositionY + Height >= GameObject.Scene.CameraPositionY + GameObject.Scene.Height - PaddingUp)
            {
                GameObject.Scene.CameraPositionY = GameObject.PositionY + Height + PaddingUp - GameObject.Scene.Height;
            }
            else if (GameObject.PositionY <= GameObject.Scene.CameraPositionY + PaddingDown)
            {
                GameObject.Scene.CameraPositionY = GameObject.PositionY - PaddingDown;
            }
        }
    }
}