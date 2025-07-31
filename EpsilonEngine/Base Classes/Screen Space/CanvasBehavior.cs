using System;
using System.Reflection;
namespace EpsilonEngine
{
    public abstract class CanvasBehavior
    {
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        public Canvas Canvas { get; private set; } = null;
        #endregion
        #region Constructors
        public CanvasBehavior(Canvas canvas)
        {
            if (canvas is null)
            {
                throw new Exception("canvas cannot be null.");
            }

            Canvas = canvas;
            Game = Canvas.Game;

            Canvas.AddCanvasBehavior(this);

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(SceneManager))
            {
                Game.RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(SceneManager))
            {
                Game.RegisterForRender(Render);
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.CanvasBehavior()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            Canvas.RemoveCanvasBehavior(this);

            Game = null;
            Canvas = null;

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