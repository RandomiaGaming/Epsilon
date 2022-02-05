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
            int positionX = GameObject.WorldPositionX;
            int positionY = GameObject.WorldPositionY;

            if (positionX + Width >= GameObject.Scene.CameraPositionX + GameObject.Scene.Width - PaddingRight)
            {
                GameObject.Scene.CameraPositionX = positionX + Width + PaddingRight - GameObject.Scene.Width;
            }
            else if (positionX <= GameObject.Scene.CameraPositionX + PaddingLeft)
            {
                GameObject.Scene.CameraPositionX = positionX - PaddingLeft;
            }

            if (positionY + Height >= GameObject.Scene.CameraPositionY + GameObject.Scene.Height - PaddingUp)
            {
                GameObject.Scene.CameraPositionY = positionY + Height + PaddingUp - GameObject.Scene.Height;
            }
            else if (positionY <= GameObject.Scene.CameraPositionY + PaddingDown)
            {
                GameObject.Scene.CameraPositionY = positionY - PaddingDown;
            }
        }
    }
}