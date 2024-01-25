using System;
using System.Collections.Generic;
namespace Epsilon
{
    public sealed class MouseState
    {
        public readonly Point position = Point.Zero;
        public readonly int scrollWheelDelta = 0;
        public readonly bool rightButtonPressed = false;
        public readonly bool leftButtonPressed = false;
        public readonly bool middleButtonPressed = false;
        public readonly List<MouseButton> pressedMouseButtons = new List<MouseButton>();
        public MouseState(Point position, int scrollWheelDelta, bool rightButtonPressed, bool leftButtonPressed, bool middleButtonPressed, List<MouseButton> pressedMouseButtons)
        {
            if (pressedMouseButtons is null)
            {
                throw new NullReferenceException();
            }

            this.position = position;
            this.scrollWheelDelta = scrollWheelDelta;
            this.rightButtonPressed = rightButtonPressed;
            this.leftButtonPressed = leftButtonPressed;
            this.middleButtonPressed = middleButtonPressed;
            this.pressedMouseButtons = pressedMouseButtons;
        }
        public bool GetMouseButtonState(MouseButton targetMouseButton)
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
