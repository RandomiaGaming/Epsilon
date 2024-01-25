using System;
using System.Reflection;
namespace EpsilonEngine
{
    public abstract class Component
    {
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        public Scene Scene { get; private set; } = null;
        public GameObject GameObject { get; private set; } = null;
        #endregion
        #region Constructors
        public Component(GameObject gameObject)
        {
            if (gameObject is null)
            {
                throw new Exception("gameObject cannot be null.");
            }

            GameObject = gameObject;
            Scene = GameObject.Scene;
            Game = Scene.Game;

            GameObject.AddComponent(this);

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Component))
            {
                Game.RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Component))
            {
                Game.RegisterForRender(Render);
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Component()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            GameObject.RemoveComponent(this);

            Game = null;
            Scene = null;
            GameObject = null;

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