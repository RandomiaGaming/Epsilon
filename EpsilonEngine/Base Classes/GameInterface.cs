using System;
namespace EpsilonEngine
{
    internal sealed class GameInterface : Microsoft.Xna.Framework.Game
    {
        public Game Game { get; private set; } = null;
        public GameInterface(Game game)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }
            Game = game;
        }
        public void Destroy()
        {
            Game = null;
        }
        protected sealed override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.UpdateCallback();
        }
        public sealed override string ToString()
        {
            return $"EpsilonEngine.GameInterface()";
        }
    }
}