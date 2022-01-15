using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace EpsilonEngine
{
    public class InputManager
    {
        private KeyboardState primaryKeyboardState = null;
        private MouseState primaryMouseState = null;
        private int lastScrollWheelValue = 0;
        public Game game
        {
            get
            {
                return _game;
            }
        }
        private Game _game = null;
        internal InputManager(Game game)
        {
            if (game is null)
            {
                throw new NullReferenceException();
            }
            _game = game;
        }

        public List<KeyboardState> GetKeyboardStates()
        {
            if (primaryKeyboardState is null)
            {
                return new List<KeyboardState>();
            }
            return new List<KeyboardState>() { primaryKeyboardState };
        }
        public List<MouseState> GetMouseStates()
        {
            if (primaryMouseState is null)
            {
                return new List<MouseState>();
            }
            return new List<MouseState>() { primaryMouseState };
        }
        public KeyboardState GetPrimaryKeyboardState()
        {
            return primaryKeyboardState;
        }
        public MouseState GetPrimaryMouseState()
        {
            return primaryMouseState;
        }
        private void UpdateInput()
        {
            Microsoft.Xna.Framework.Input.KeyboardState keyboardState = Keyboard.GetState();
            List<KeyboardButton> pressedKeyboardButtons = new List<KeyboardButton>();
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                switch (key)
                {
                    case Keys.A:
                        pressedKeyboardButtons.Add(KeyboardButton.A);
                        break;
                    case Keys.B:
                        pressedKeyboardButtons.Add(KeyboardButton.B);
                        break;
                    case Keys.C:
                        pressedKeyboardButtons.Add(KeyboardButton.C);
                        break;
                    case Keys.D:
                        pressedKeyboardButtons.Add(KeyboardButton.D);
                        break;
                    case Keys.E:
                        pressedKeyboardButtons.Add(KeyboardButton.E);
                        break;
                    case Keys.F:
                        pressedKeyboardButtons.Add(KeyboardButton.F);
                        break;
                    case Keys.G:
                        pressedKeyboardButtons.Add(KeyboardButton.G);
                        break;
                    case Keys.H:
                        pressedKeyboardButtons.Add(KeyboardButton.H);
                        break;
                    case Keys.I:
                        pressedKeyboardButtons.Add(KeyboardButton.I);
                        break;
                    case Keys.J:
                        pressedKeyboardButtons.Add(KeyboardButton.J);
                        break;
                    case Keys.K:
                        pressedKeyboardButtons.Add(KeyboardButton.K);
                        break;
                    case Keys.L:
                        pressedKeyboardButtons.Add(KeyboardButton.L);
                        break;
                    case Keys.M:
                        pressedKeyboardButtons.Add(KeyboardButton.M);
                        break;
                    case Keys.N:
                        pressedKeyboardButtons.Add(KeyboardButton.N);
                        break;
                    case Keys.O:
                        pressedKeyboardButtons.Add(KeyboardButton.O);
                        break;
                    case Keys.P:
                        pressedKeyboardButtons.Add(KeyboardButton.P);
                        break;
                    case Keys.Q:
                        pressedKeyboardButtons.Add(KeyboardButton.Q);
                        break;
                    case Keys.R:
                        pressedKeyboardButtons.Add(KeyboardButton.R);
                        break;
                    case Keys.S:
                        pressedKeyboardButtons.Add(KeyboardButton.S);
                        break;
                    case Keys.T:
                        pressedKeyboardButtons.Add(KeyboardButton.T);
                        break;
                    case Keys.U:
                        pressedKeyboardButtons.Add(KeyboardButton.U);
                        break;
                    case Keys.V:
                        pressedKeyboardButtons.Add(KeyboardButton.V);
                        break;
                    case Keys.W:
                        pressedKeyboardButtons.Add(KeyboardButton.W);
                        break;
                    case Keys.X:
                        pressedKeyboardButtons.Add(KeyboardButton.X);
                        break;
                    case Keys.Y:
                        pressedKeyboardButtons.Add(KeyboardButton.Y);
                        break;
                    case Keys.Z:
                        pressedKeyboardButtons.Add(KeyboardButton.Z);
                        break;
                    case Keys.NumPad0:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad0);
                        break;
                    case Keys.NumPad1:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad1);
                        break;
                    case Keys.NumPad2:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad2);
                        break;
                    case Keys.NumPad3:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad3);
                        break;
                    case Keys.NumPad4:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad4);
                        break;
                    case Keys.NumPad5:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad5);
                        break;
                    case Keys.NumPad6:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad6);
                        break;
                    case Keys.NumPad7:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad7);
                        break;
                    case Keys.NumPad8:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad8);
                        break;
                    case Keys.NumPad9:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPad9);
                        break;
                    case Keys.OemPlus:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPadPlus);
                        pressedKeyboardButtons.Add(KeyboardButton.Plus);
                        break;
                    case Keys.OemMinus:
                        pressedKeyboardButtons.Add(KeyboardButton.NumPadMinus);
                        pressedKeyboardButtons.Add(KeyboardButton.Minus);
                        break;
                    case Keys.NumLock:
                        pressedKeyboardButtons.Add(KeyboardButton.NumLock);
                        break;
                    case Keys.LeftShift:
                        pressedKeyboardButtons.Add(KeyboardButton.LeftShift);
                        break;
                    case Keys.RightShift:
                        pressedKeyboardButtons.Add(KeyboardButton.RightShift);
                        break;
                    case Keys.LeftControl:
                        pressedKeyboardButtons.Add(KeyboardButton.LeftControl);
                        break;
                    case Keys.RightControl:
                        pressedKeyboardButtons.Add(KeyboardButton.RightControl);
                        break;
                    case Keys.LeftAlt:
                        pressedKeyboardButtons.Add(KeyboardButton.LeftAlt);
                        break;
                    case Keys.RightAlt:
                        pressedKeyboardButtons.Add(KeyboardButton.RightAlt);
                        break;
                    case Keys.LeftWindows:
                        pressedKeyboardButtons.Add(KeyboardButton.LeftWindows);
                        break;
                    case Keys.RightWindows:
                        pressedKeyboardButtons.Add(KeyboardButton.RightWindows);
                        break;
                    case Keys.F1:
                        pressedKeyboardButtons.Add(KeyboardButton.F1);
                        break;
                    case Keys.F2:
                        pressedKeyboardButtons.Add(KeyboardButton.F2);
                        break;
                    case Keys.F3:
                        pressedKeyboardButtons.Add(KeyboardButton.F3);
                        break;
                    case Keys.F4:
                        pressedKeyboardButtons.Add(KeyboardButton.F4);
                        break;
                    case Keys.F5:
                        pressedKeyboardButtons.Add(KeyboardButton.F5);
                        break;
                    case Keys.F6:
                        pressedKeyboardButtons.Add(KeyboardButton.F6);
                        break;
                    case Keys.F7:
                        pressedKeyboardButtons.Add(KeyboardButton.F7);
                        break;
                    case Keys.F8:
                        pressedKeyboardButtons.Add(KeyboardButton.F8);
                        break;
                    case Keys.F9:
                        pressedKeyboardButtons.Add(KeyboardButton.F9);
                        break;
                    case Keys.F10:
                        pressedKeyboardButtons.Add(KeyboardButton.F10);
                        break;
                    case Keys.F11:
                        pressedKeyboardButtons.Add(KeyboardButton.F11);
                        break;
                    case Keys.F12:
                        pressedKeyboardButtons.Add(KeyboardButton.F12);
                        break;
                    case Keys.Back:
                        pressedKeyboardButtons.Add(KeyboardButton.Backspace);
                        break;
                    case Keys.Delete:
                        pressedKeyboardButtons.Add(KeyboardButton.Delete);
                        break;
                    case Keys.Scroll:
                        pressedKeyboardButtons.Add(KeyboardButton.ScrollLock);
                        break;
                    case Keys.Escape:
                        pressedKeyboardButtons.Add(KeyboardButton.Escape);
                        break;
                    case Keys.Tab:
                        pressedKeyboardButtons.Add(KeyboardButton.Tab);
                        break;
                    case Keys.OemTilde:
                        pressedKeyboardButtons.Add(KeyboardButton.Tilde);
                        break;
                    case Keys.Space:
                        pressedKeyboardButtons.Add(KeyboardButton.Space);
                        break;
                    case Keys.PrintScreen:
                        pressedKeyboardButtons.Add(KeyboardButton.PrintScreen);
                        break;
                    case Keys.Insert:
                        pressedKeyboardButtons.Add(KeyboardButton.Insert);
                        break;
                    case Keys.Home:
                        pressedKeyboardButtons.Add(KeyboardButton.Home);
                        break;
                    case Keys.PageUp:
                        pressedKeyboardButtons.Add(KeyboardButton.PageUp);
                        break;
                    case Keys.PageDown:
                        pressedKeyboardButtons.Add(KeyboardButton.PageDown);
                        break;
                    case Keys.End:
                        pressedKeyboardButtons.Add(KeyboardButton.End);
                        break;
                    case Keys.OemBackslash:
                        pressedKeyboardButtons.Add(KeyboardButton.Backslash);
                        break;
                    case Keys.Divide:
                        pressedKeyboardButtons.Add(KeyboardButton.Slash);
                        break;
                    case Keys.OemComma:
                        pressedKeyboardButtons.Add(KeyboardButton.Comma);
                        break;
                    case Keys.OemPeriod:
                        pressedKeyboardButtons.Add(KeyboardButton.Period);
                        pressedKeyboardButtons.Add(KeyboardButton.NumPadPoint);
                        break;
                    case Keys.Help:
                        pressedKeyboardButtons.Add(KeyboardButton.Help);
                        break;
                }
            }

            primaryKeyboardState = new KeyboardState(keyboardState.CapsLock, false, keyboardState.IsKeyDown(Keys.LeftShift | Keys.RightShift), keyboardState.NumLock, pressedKeyboardButtons);



            Microsoft.Xna.Framework.Input.MouseState mouseState = Mouse.GetState();

            Point position = new Point(mouseState.X, mouseState.Y);

            int scrollWheelValue = mouseState.ScrollWheelValue - lastScrollWheelValue;

            bool rightMouseButton = mouseState.RightButton == ButtonState.Pressed;
            bool leftMouseButton = mouseState.LeftButton == ButtonState.Pressed;
            bool middleMouseButton = mouseState.MiddleButton == ButtonState.Pressed;

            List<MouseButton> pressedMouseButtons = new List<MouseButton>();

            if (mouseState.XButton1 == ButtonState.Pressed)
            {
                pressedMouseButtons.Add(MouseButton.Button0);
            }
            if (mouseState.XButton2 == ButtonState.Pressed)
            {
                pressedMouseButtons.Add(MouseButton.Button1);
            }

            primaryMouseState = new MouseState(position, scrollWheelValue, rightMouseButton, leftMouseButton, middleMouseButton, pressedMouseButtons);

            lastScrollWheelValue = scrollWheelValue;
        }
    }
}
