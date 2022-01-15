using System.IO;
using System;
namespace EpsilonEngine
{
    public abstract class AssetBase
    {
        public readonly Stream sourceStream = null;
        public readonly string fullName = "";
        public AssetBase(Stream sourceStream, string fullName)
        {
            if(sourceStream is null)
            {
                throw new NullReferenceException();
            }
            this.sourceStream = sourceStream;
            if(fullName is null)
            {
                throw new NullReferenceException();
            }
            if(fullName == "")
            {
                throw new ArgumentException();
            }
            this.fullName = fullName;
        }
        public sealed override string ToString()
        {
            return $"({fullName})";
        }
        public sealed override bool Equals(object obj)
        {
            if (obj is null || obj.GetType().IsAssignableFrom(typeof(AssetBase)))
            {
                return false;
            }
            return fullName == ((AssetBase)obj).fullName;
        }
        public sealed override int GetHashCode()
        {
            return fullName.GetHashCode();
        }
    }
}
