using System.IO;
using System;
namespace DontMelt
{
    public sealed class TextureAsset : AssetBase
    {
        public readonly Texture data = null;
        public TextureAsset(Stream sourceStream, string fullName, Texture data) : base(sourceStream, fullName)
        {
            if(data is null)
            {
                throw new NullReferenceException();
            }
            if(data.width <= 0 || data.height <= 0)
            {
                throw new ArgumentException();
            }
            this.data = data;
        }
    }
}
