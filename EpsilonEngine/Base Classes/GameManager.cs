using System;
using System.Reflection;
namespace EpsilonEngine
{
    public abstract class GameManager
    {
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        #endregion
        #region Constructors
        public GameManager(Game game)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }

            Game = game;

            Game.AddGameManager(this);

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(GameManager))
            {
                Game.RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(GameManager))
            {
                Game.RegisterForRender(Render);
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.GameManager()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            Game.RemoveGameManager(this);

            Game = null;

            IsDestroyed = true;
        }
        #endregion
        #region Overridables
        protected virtual void Update()
        {

        }
        protected virtual void Render()
        {

        }
        #endregion
    }
}