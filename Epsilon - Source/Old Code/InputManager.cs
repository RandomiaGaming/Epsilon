using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Epsilon
{
    public class InputManager
    {
        private bool _rightPressed;
        public bool rightPressed
        {
            get
            {
                return _rightPressed;
            }
        }
        private bool _leftPressed;
        public bool leftPressed
        {
            get
            {
                return _leftPressed;
            }
        }
        public int horizontalAxis
        {
            get
            {
                if (rightPressed && !leftPressed)
                {
                    return 1;
                }
                else if (leftPressed && !rightPressed)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
        private bool _upPressed;
        public bool upPressed
        {
            get
            {
                return _upPressed;
            }
        }
        private bool _downPressed;
        public bool downPressed
        {
            get
            {
                return _downPressed;
            }
        }
        public int verticalAxis
        {
            get
            {
                if (upPressed && !downPressed)
                {
                    return 1;
                }
                else if (downPressed && !upPressed)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
        private InputPacket CreateInputPacket()
        {
            Microsoft.Xna.Framework.Input.KeyboardState keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            List<KeyboardButton> pressedKeyboardButtons = new List<KeyboardButton>();
            foreach (Microsoft.Xna.Framework.Input.Keys key in keyboardState.GetPressedKeys())
            {
                switch (key)
                {
                    case Microsoft.Xna.Framework.Input.Keys.A:
                        pressedKeyboardButtons.Add(KeyboardButton.A);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.B:
                        pressedKeyboardButtons.Add(KeyboardButton.B);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.C:
                        pressedKeyboardButtons.Add(KeyboardButton.C);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.D:
                        pressedKeyboardButtons.Add(KeyboardButton.D);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.E:
                        pressedKeyboardButtons.Add(KeyboardButton.E);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F:
                        pressedKeyboardButtons.Add(KeyboardButton.F);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.G:
                        pressedKeyboardButtons.Add(KeyboardButton.G);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.H:
                        pressedKeyboardButtons.Add(KeyboardButton.H);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.I:
                        pressedKeyboardButtons.Add(KeyboardButton.I);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.J:
                        pressedKeyboardButtons.Add(KeyboardButton.J);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.K:
                        pressedKeyboardButtons.Add(KeyboardButton.K);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.L:
                        pressedKeyboardButtons.Add(KeyboardButton.L);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.M:
                        pressedKeyboardButtons.Add(KeyboardButton.M);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.N:
                        pressedKeyboardButtons.Add(KeyboardButton.N);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.O:
                        pressedKeyboardButtons.Add(KeyboardButton.O);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.P:
                        pressedKeyboardButtons.Add(KeyboardButton.P);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Q:
                        pressedKeyboardButtons.Add(KeyboardButton.Q);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.R:
                        pressedKeyboardButtons.Add(KeyboardButton.R);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.S:
                        pressedKeyboardButtons.Add(KeyboardButton.S);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.T:
                        pressedKeyboardButtons.Add(KeyboardButton.T);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.U:
                        pressedKeyboardButtons.Add(KeyboardButton.U);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.V:
                        pressedKeyboardButtons.Add(KeyboardButton.V);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.W:
                        pressedKeyboardButtons.Add(KeyboardButton.W);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.X:
                        pressedKeyboardButtons.Add(KeyboardButton.X);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Y:
                        pressedKeyboardButtons.Add(KeyboardButton.Y);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Z:
                        pressedKeyboardButtons.Add(KeyboardButton.Z);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad0:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad0);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad1:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad1);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad2:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad2);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad3:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad3);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad4:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad4);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad5:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad5);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad6:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad6);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad7:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad7);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad8:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad8);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumPad9:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad9);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.OemPlus:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPadPlus);
                        pressedKeyboardButtons.Add(KeyboardButton.Plus);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.OemMinus:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPadMinus);
                        pressedKeyboardButtons.Add(KeyboardButton.Minus);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.NumLock:
                        pressedKeyboardButtons.Add(KeyboardButton.NumLock);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.LeftShift:
                        pressedKeyboardButtons.Add(KeyboardButton.LeftShift);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.RightShift:
                        pressedKeyboardButtons.Add(KeyboardButton.RightShift);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.LeftControl:
                        pressedKeyboardButtons.Add(KeyboardButton.LeftControl);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.RightControl:
                        pressedKeyboardButtons.Add(KeyboardButton.RightControl);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.LeftAlt:
                        pressedKeyboardButtons.Add(KeyboardButton.LeftAlt);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.RightAlt:
                        pressedKeyboardButtons.Add(KeyboardButton.RightAlt);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.LeftWindows:
                        pressedKeyboardButtons.Add(KeyboardButton.LeftWindows);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.RightWindows:
                        pressedKeyboardButtons.Add(KeyboardButton.RightWindows);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F1:
                        pressedKeyboardButtons.Add(KeyboardButton.F1);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F2:
                        pressedKeyboardButtons.Add(KeyboardButton.F2);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F3:
                        pressedKeyboardButtons.Add(KeyboardButton.F3);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F4:
                        pressedKeyboardButtons.Add(KeyboardButton.F4);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F5:
                        pressedKeyboardButtons.Add(KeyboardButton.F5);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F6:
                        pressedKeyboardButtons.Add(KeyboardButton.F6);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F7:
                        pressedKeyboardButtons.Add(KeyboardButton.F7);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F8:
                        pressedKeyboardButtons.Add(KeyboardButton.F8);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F9:
                        pressedKeyboardButtons.Add(KeyboardButton.F9);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F10:
                        pressedKeyboardButtons.Add(KeyboardButton.F10);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F11:
                        pressedKeyboardButtons.Add(KeyboardButton.F11);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.F12:
                        pressedKeyboardButtons.Add(KeyboardButton.F12);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Back:
                        pressedKeyboardButtons.Add(KeyboardButton.Backspace);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Delete:
                        pressedKeyboardButtons.Add(KeyboardButton.Delete);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Scroll:
                        pressedKeyboardButtons.Add(KeyboardButton.ScrollLock);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Escape:
                        pressedKeyboardButtons.Add(KeyboardButton.Escape);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Tab:
                        pressedKeyboardButtons.Add(KeyboardButton.Tab);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.OemTilde:
                        pressedKeyboardButtons.Add(KeyboardButton.Tilde);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Space:
                        pressedKeyboardButtons.Add(KeyboardButton.Space);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.PrintScreen:
                        pressedKeyboardButtons.Add(KeyboardButton.PrintScreen);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Insert:
                        pressedKeyboardButtons.Add(KeyboardButton.Insert);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Home:
                        pressedKeyboardButtons.Add(KeyboardButton.Home);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.PageUp:
                        pressedKeyboardButtons.Add(KeyboardButton.PageUp);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.PageDown:
                        pressedKeyboardButtons.Add(KeyboardButton.PageDown);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.End:
                        pressedKeyboardButtons.Add(KeyboardButton.End);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.OemBackslash:
                        pressedKeyboardButtons.Add(KeyboardButton.Backslash);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Divide:
                        pressedKeyboardButtons.Add(KeyboardButton.Slash);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.OemComma:
                        pressedKeyboardButtons.Add(KeyboardButton.Comma);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.OemPeriod:
                        pressedKeyboardButtons.Add(KeyboardButton.Period);
                        pressedKeyboardButtons.Add(KeyboardButton.NumPadPoint);
                        break;
                    case Microsoft.Xna.Framework.Input.Keys.Help:
                        pressedKeyboardButtons.Add(KeyboardButton.Help);
                        break;
                }
            }

            KeyboardState dmKeyboardState = new KeyboardState(keyboardState.CapsLock, false, keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift | Microsoft.Xna.Framework.Input.Keys.RightShift), keyboardState.NumLock, pressedKeyboardButtons);



            Microsoft.Xna.Framework.Input.MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            Point position = new Point(mouseState.X, mouseState.Y);

            int scrollWheelValue = mouseState.ScrollWheelValue - lastScrollWheelValue;

            bool rightMouseButton = mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            bool leftMouseButton = mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            bool middleMouseButton = mouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;

            List<MouseButton> pressedMouseButtons = new List<MouseButton>();

            if (mouseState.XButton1 == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                pressedMouseButtons.Add(MouseButton.Button0);
            }
            if (mouseState.XButton2 == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                pressedMouseButtons.Add(MouseButton.Button1);
            }

            MouseState dmMouseState = new MouseState(position, scrollWheelValue, rightMouseButton, leftMouseButton, middleMouseButton, pressedMouseButtons);

            InputPacket iPacket = new InputPacket(dmKeyboardState, dmMouseState);

            lastScrollWheelValue = scrollWheelValue;
            return iPacket;
        }
    }
}
