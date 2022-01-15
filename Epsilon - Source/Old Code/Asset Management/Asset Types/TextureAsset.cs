using System.IO;
using System;
namespace EpsilonEngine
{
    public sealed class TextureAsset : AssetBase
    {
        public readonly Texture data;
        public TextureAsset(Stream sourceStream, string fullName, Texture data) : base(sourceStream, fullName)
        {
            this.data = data;
        }
    }
}
