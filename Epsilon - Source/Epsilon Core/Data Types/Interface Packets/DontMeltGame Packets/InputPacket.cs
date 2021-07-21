using System;
namespace DontMelt
{
    public sealed class InputPacket
    {
        public readonly KeyboardState keyboardState = null;
        public readonly MouseState mouseState = null;
        public InputPacket(KeyboardState keyboardState, MouseState mouseState)
        {
            if(keyboardState is null)
            {
                throw new NullReferenceException();
            }
            this.keyboardState = keyboardState;
            if(mouseState is null)
            {
                throw new NullReferenceException();
            }
            this.mouseState = mouseState;
        }
    }
}
