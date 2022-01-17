using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Epsilon
{
    public static class KeyboardInput
    {
        [RegisterHardwareInput("Space")]
        public static bool SpaceHardwareInput()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);
        }
        [RegisterHardwareInput("D")]
        public static bool DHardwareInput()
        {
            return Keyboard.GetState().IsKeyDown(Keys.D);
        }
        [RegisterHardwareInput("A")]
        public static bool AHardwareInput()
        {
            return Keyboard.GetState().IsKeyDown(Keys.A);
        }
    }
}
