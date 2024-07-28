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
        public CameraFollower(GameObject gameObject, int updatePriority) : base(gameObject, updatePriority, 0)
        {

        }
        public override string ToString()
        {
            return $"EpsilonEngine.CameraFollower()";
        }
        protected override void Update()
        {
            if (GameObject.PositionX + Width >= GameObject.Scene.CameraPositionX + GameObject.Scene.RenderWidth - PaddingRight)
            {
                GameObject.Scene.CameraPositionX = GameObject.PositionX + Width + PaddingRight - GameObject.Scene.RenderWidth;
            }
            else if (GameObject.PositionX <= GameObject.Scene.CameraPositionX + PaddingLeft)
            {
                GameObject.Scene.CameraPositionX = GameObject.PositionX - PaddingLeft;
            }

            if (GameObject.PositionY + Height >= GameObject.Scene.CameraPositionY + GameObject.Scene.RenderHeight - PaddingUp)
            {
                GameObject.Scene.CameraPositionY = GameObject.PositionY + Height + PaddingUp - GameObject.Scene.RenderHeight;
            }
            else if (GameObject.PositionY <= GameObject.Scene.CameraPositionY + PaddingDown)
            {
                GameObject.Scene.CameraPositionY = GameObject.PositionY - PaddingDown;
            }
        }
    }
}