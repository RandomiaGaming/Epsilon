using System;
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
        internal void InvokeUpdate()
        {
            Update();
        }
        internal void InvokeRender()
        {
            Render();
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