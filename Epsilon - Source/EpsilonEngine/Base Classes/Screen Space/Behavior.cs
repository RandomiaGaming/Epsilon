using System;
using System.Reflection;
namespace EpsilonEngine
{
    public abstract class Behavior
    {
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        public Canvas Canvas { get; private set; } = null;
        public Element Element { get; private set; } = null;
        #endregion
        #region Constructors
        public Behavior(Element element)
        {
            if (element is null)
            {
                throw new Exception("element cannot be null.");
            }

            Element = element;
            Canvas = Element.Canvas;
            Game = Canvas.Game;

            Element.AddBehavior(this);

            Type thisType = GetType();

            MethodInfo updateMethod = thisType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateMethod.DeclaringType != typeof(Behavior))
            {
                Game.RegisterForUpdate(Update);
            }

            MethodInfo renderMethod = thisType.GetMethod("Render", BindingFlags.NonPublic | BindingFlags.Instance);
            if (renderMethod.DeclaringType != typeof(Behavior))
            {
                Game.RegisterForRender(Render);
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Behavior()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            Element.RemoveBehavior(this);

            Game = null;
            Canvas = null;
            Element = null;

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