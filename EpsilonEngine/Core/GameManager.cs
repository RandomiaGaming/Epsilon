namespace EpsilonEngine
{
    public abstract class GameManager
    {
        #region Public Variables
        public Game Game { get; private set; } = null;
        public bool MarkedForDestruction { get; private set; } = false;
        public bool Destroyed { get; private set; } = false;
        public bool OverridesUpdate { get; private set; } = false;
        public int UpdatePriority { get; private set; } = 0;
        public bool OverridesDraw { get; private set; } = false;
        public int DrawPriority { get; private set; } = 0;
        #endregion
        #region Constructors
        public GameManager(Game game, int updatePriority, int drawPriority)
        {
            if (game is null)
            {
                throw new System.Exception("game cannot be null.");
            }
            Game = game;
            Game.AddGameManager(this);
            System.Type thisType = GetType();
            System.Reflection.MethodInfo updateMethod = thisType.GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(GameManager))
            {
                OverridesUpdate = true;
                Game.UpdatePump.RegisterPumpEventUnsafe(Update, updatePriority);
            }
            System.Reflection.MethodInfo drawMethod = thisType.GetMethod("Draw", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (drawMethod.DeclaringType != typeof(GameManager))
            {
                OverridesDraw = true;
                Game.DrawPump.RegisterPumpEventUnsafe(Draw, drawPriority);
            }
            game.InitializationPump.RegisterPumpEventUnsafe(Initialize);
        }
        #endregion
        #region Public Methods
        public void MarkForDestruction()
        {
            if (MarkedForDestruction)
            {
                throw new System.Exception("gameManager has already been marked for destruction.");
            }
            if (Destroyed)
            {
                throw new System.Exception("gameManager has already been destroyed.");
            }
            Game.OnDestroyPump.RegisterPumpEventUnsafe(OnDestroy);
            Game.DestructionPump.RegisterPumpEventUnsafe(Destroy);
            MarkedForDestruction = true;
        }
        #endregion
        #region Private Methods
        private void Destroy()
        {
            Game.RemoveGameManager(this);
            if (OverridesUpdate)
            {
                Game.UpdatePump.UnregisterPumpEventUnsafe(Update);
            }
            if (OverridesDraw)
            {
                Game.DrawPump.UnregisterPumpEventUnsafe(Draw);
            }
            Game = null;
            Destroyed = true;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.GameManager()";
        }
        #endregion
        #region Overridable Methods
        protected virtual void Initialize()
        {
        }
        protected virtual void Update()
        {
        }
        protected virtual void Draw()
        {
        }
        protected virtual void OnDestroy()
        {
        }
        #endregion
    }
}