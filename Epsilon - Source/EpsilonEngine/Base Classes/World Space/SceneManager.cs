using System;
namespace EpsilonEngine
{
    public abstract class SceneManager
    {
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        public Scene Scene { get; private set; } = null;
        #endregion
        #region Constructors
        public SceneManager(Scene scene)
        {
            if (scene is null)
            {
                throw new Exception("scene cannot be null.");
            }

            Scene = scene;
            Game = Scene.Game;

            Scene.AddSceneManager(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.SceneManager()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            Scene.RemoveSceneManager(this);

            Game = null;
            Scene = null;

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