using System;
using System.IO;

namespace DontMelt
{
    public sealed class StageAsset : AssetBase
    {
        public readonly StageData data = null;
        public StageAsset(Stream sourceStream, string fullName, StageData data) : base(sourceStream, fullName)
        {
            if(data is null)
            {
                throw new NullReferenceException();
            }
            this.data = data;
        }
    }
}
