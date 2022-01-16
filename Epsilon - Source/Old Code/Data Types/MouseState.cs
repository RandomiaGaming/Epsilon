using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class MouseState
    {
        public static MouseState Current
        {
            get
            {
                return GetCurrent();
            }
        }
        public static MouseState GetCurrent()
        {
            Microsoft.Xna.Framework.Input.MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            Point position = new Point(mouseState.X, mouseState.Y);
            int scrollWheelValue = mouseState.ScrollWheelValue;
            bool rightButtonPressed = mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            bool leftButtonPressed = mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            bool middleButtonPressed = mouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            List<MouseButton> pressedMouseButtons = new List<MouseButton>();
            return new MouseState(position, scrollWheelValue, rightButtonPressed, leftButtonPressed, middleButtonPressed, new List<MouseButton>());
        }

        public readonly Point position = Point.Zero;
        public readonly int scrollWheelValue = 0;
        public readonly bool rightButtonPressed = false;
        public readonly bool leftButtonPressed = false;
        public readonly bool middleButtonPressed = false;
        public readonly List<MouseButton> pressedMouseButtons = new List<MouseButton>();
        public MouseState(Point position, int scrollWheelValue, bool rightButtonPressed, bool leftButtonPressed, bool middleButtonPressed, List<MouseButton> pressedMouseButtons)
        {
            if (pressedMouseButtons is null)
            {
                throw new NullReferenceException();
            }

            this.position = position;
            this.scrollWheelValue = scrollWheelValue;
            this.rightButtonPressed = rightButtonPressed;
            this.leftButtonPressed = leftButtonPressed;
            this.middleButtonPressed = middleButtonPressed;
            this.pressedMouseButtons = pressedMouseButtons;
        }
        public bool MouseButtonPressed(MouseButton targetMouseButton)
        {
            foreach(MouseButton pressedMouseButton in pressedMouseButtons)
            {
                if(pressedMouseButton == targetMouseButton)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
