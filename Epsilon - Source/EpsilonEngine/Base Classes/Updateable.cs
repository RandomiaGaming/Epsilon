using System;

namespace EpsilonEngine
{
    public abstract class Updateable
    {
        #region Constructors
        public Updateable() { }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Updateable()";
        }
        #endregion
        #region Methods
        internal abstract void Update();
        #endregion
    }
}