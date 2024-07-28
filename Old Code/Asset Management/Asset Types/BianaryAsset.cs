using System.IO;
using System;
namespace EpsilonEngine
{
    public sealed class BianaryAsset : AssetBase
    {
        public readonly byte[] data = null;
        public BianaryAsset(Stream sourceStream, string fullName, byte[] data) : base(sourceStream, fullName)
        {
            if(data is null)
            {
                throw new NullReferenceException();
            }
            this.data = data;
        }
    }
}
