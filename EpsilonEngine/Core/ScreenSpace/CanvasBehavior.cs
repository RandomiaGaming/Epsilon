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
                throw new System.Exception("canvas cannot be null.");
            }
            Canvas = canvas;
            Game = Canvas.Game;
            Canvas.AddCanvasBehavior(this);
            System.Type thisType = GetType();
            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(SceneManager))
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, 0);
            }
            System.Reflection.MethodInfo renderMethod = thisType.GetMethod("Render", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(SceneManager))
            {
                Game.RenderPump.RegisterPumpEventUnsafe(Render);
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