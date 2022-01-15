using System.Collections.Generic;
using System;
namespace EpsilonEngine
{
    public sealed class KeyboardState
    {
        public readonly bool capsLockState = false;
        public readonly bool scrollLockState = false;
        public readonly bool shiftState = false;
        public readonly bool numLockState = true;
        public readonly List<KeyboardButton> pressedButtons = new List<KeyboardButton>();

        public KeyboardState(bool capsLockState, bool scrollLockState, bool shiftState, bool numLockState, List<KeyboardButton> pressedButtons)
        {
            if(pressedButtons is null)
            {
                throw new NullReferenceException();
            }

            this.capsLockState = capsLockState;
            this.scrollLockState = scrollLockState;
            this.shiftState = shiftState;
            this.numLockState = numLockState;
            this.pressedButtons = pressedButtons;
        }
        public bool GetKeyboardButtonState(KeyboardButton targetKeyboardButton)
        {
            foreach(KeyboardButton pressedKeyboardButton in pressedButtons)
            {
                if(pressedKeyboardButton == targetKeyboardButton)
                {
                    return true;
                }
            }
            return false;
        }
    }
}