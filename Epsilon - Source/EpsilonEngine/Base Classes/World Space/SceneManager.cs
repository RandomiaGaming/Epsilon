using System;
namespace EpsilonEngine
{
    public abstract class SceneManager
    {
        #region Properties
        public bool Initialized { get; private set; } = false;
        public Game Engine { get; private set; } = null;
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
            Engine = Scene.Engine;

            Scene.AddSceneManager(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.SceneManager()";
        }
        #endregion
        #region Overridables
        public virtual void OnInitialize()
        {

        }
        public virtual void OnRemove()
        {

        }
        #endregion
        #region Methods
        public void Initialize()
        {
            OnInitialize();
            Initialized = true;
        }
        #endregion
    }
}