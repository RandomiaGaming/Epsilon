using System;
using System.Reflection;
namespace EpsilonEngine
{
    public abstract class Behavior
    {
        #region Properties
        public bool Initialized { get; private set; } = false;
        public bool Destroyed { get; private set; } = false;
        public bool Updateable { get; private set; } = false;
        public bool Renderaable { get; private set; } = false;
        public Engine Engine { get; private set; } = null;
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
            Engine = Scene.Engine;

            Type thisType = GetType();
            var updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Component))
            {
                Updateable = true;
                Engine.RegisterForUpdate((PumpEvent)Delegate.CreateDelegate(typeof(PumpEvent), this, "Update", false, true));
            }

            var renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Component))
            {
                Renderaable = true;
                Scene.RegisterForRender((PumpEvent)Delegate.CreateDelegate(typeof(PumpEvent), this, "Render", false, true));
            }

            GameObject.AddComponent(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Component()";
        }
        #endregion
        #region Overridables
        //Called on the first frame after the component is created before update.
        protected virtual void Initialize()
        {

        }
        //Called every frame, before render.
        protected virtual void Update()
        {

        }
        //Called every frame after render.
        protected virtual void Render()
        {

        }
        //Called right before the component is destroyed.
        protected virtual void OnRemove()
        {

        }
        #endregion
        #region Methods
        internal void InvokeInitialize()
        {
            Initialize();
            Initialized = true;
        }
        internal void InvokeOnRemove()
        {
            OnRemove();
            Destroyed = true;
        }
        #endregion
    }
}