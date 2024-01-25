using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace EpsilonCore
{
    public sealed class AssetLoader
    {
        private Epsilon _epsilon = null;
        public Epsilon epsilon
        {
            get
            {
                return _epsilon;
            }
            private set
            {
                _epsilon = value;
            }
        }
        public AssetLoader(Epsilon epsilon)
        {
            if (epsilon is null)
            {
                throw new NullReferenceException("Epsilon cannot be null.");
            }
            this.epsilon = epsilon;
        }
    }
}
