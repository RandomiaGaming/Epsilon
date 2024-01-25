namespace EpsilonEngine
{
    public abstract class SceneManager
    {
        #region Public Variables
        public bool Destroyed { get; private set; } = false;
        public Game Game { get; private set; } = null;
        public Scene Scene { get; private set; } = null;
        public bool OverridesUpdate { get; private set; } = false;
        public int UpdatePriority { get; private set; } = 0;
        public bool OverridesRender { get; private set; } = false;
        public int RenderPriority { get; private set; } = 0;
        #endregion
        #region Constructors
        public SceneManager(Scene scene, int updatePriority, int renderPriority)
        {
            if (scene is null)
            {
                throw new System.Exception("scene cannot be null.");
            }
            Scene = scene;
            Game = Scene.Game;
            Scene.AddSceneManager(this);
            System.Type thisType = GetType();
            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(SceneManager))
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, updatePriority);
                OverridesUpdate = true;
            }
            System.Reflection.MethodInfo renderMethod = thisType.GetMethod("Render", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(SceneManager))
            {
                Scene.RenderPump.RegisterPumpEventUnsafe(Render, renderPriority);
                OverridesRender = true;
            }
        }
        #endregion
        #region Public Methods
        public void Destroy()
        {
            Scene.RemoveSceneManager(this);
            Game = null;
            Scene = null;
            Destroyed = true;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.SceneManager()";
        }
        #endregion
        #region Overridable Methods
        protected virtual void Update()
        {
        }
        protected virtual void Render()
        {
        }
        #endregion
    }
}