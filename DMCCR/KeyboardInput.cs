using EpsilonEngine;
namespace DMCCR
{
    public static class KeyboardInput
    {
        [RegisterHardwareInput("Space")]
        public static bool SpaceHardwareInput()
        {
            return Microsoft.Xna.Framework.Input.Keyboard.GetState()
                .IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space);
        }
        [RegisterHardwareInput("D")]
        public static bool DHardwareInput()
        {
            return Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D);
        }
        [RegisterHardwareInput("A")]
        public static bool AHardwareInput()
        {
            return Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A);
        }
        [RegisterHardwareInput("W")]
        public static bool WHardwareInput()
        {
            return Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W);
        }
        [RegisterHardwareInput("S")]
        public static bool SHardwareInput()
        {
            return Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S);
        }
    }
}