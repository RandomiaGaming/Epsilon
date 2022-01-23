using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Epsilon
{
    public sealed class BouncyBoi : StageObject
    {
        private const float moveForce = -0.025f;
        private const float bounceForce = 0.075f;
        private const float gravityForce = -0.000025f;

        public float velocityX = 0;
        public float velocityY = 0;

        private float positionX = 0;
        private float positionY = 40;

        private Texture2D texture;
        private Color color;
        public BouncyBoi(Stage stage, Texture2D texture, Color color) : base(stage)
        {
            this.texture = texture;
            this.color = color;
        }
        protected override void OnUpdate()
        {
            velocityY += gravityForce;

            positionX += velocityX;
            positionY += velocityY;

            if (positionY < Stage.CameraPosition.Y)
            {
                positionY = Stage.CameraPosition.Y;
                velocityY = bounceForce;
            }
            else if (positionY >= Stage.CameraPosition.Y + Stage.ViewportSize.Y - 16)
            {
                positionY = Stage.CameraPosition.Y + Stage.ViewportSize.Y - 16 - 1;
                velocityY = 0;
            }

            if (positionX <= Stage.CameraPosition.X)
            {
                velocityX = moveForce * -1;
                positionX = Stage.CameraPosition.X;
            }
            else if (positionX >= Stage.CameraPosition.X + Stage.ViewportSize.X - 8)
            {
                velocityX = moveForce;
                positionX = Stage.CameraPosition.X + Stage.ViewportSize.X - 8 - 1;
            }

            Position = new Point((int)positionX, (int)positionY);
        }
        protected override void OnRender()
        {
            base.DrawTexture(texture, Point.Zero, color);
        }
    }
}
