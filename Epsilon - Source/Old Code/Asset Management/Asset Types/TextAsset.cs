using System.IO;
namespace EpsilonEngine
{
    public sealed class TextAsset : AssetBase
    {
        public readonly string data = null;
        public TextAsset(Stream sourceStream, string fullName, string data) : base(sourceStream, fullName)
        {
            this.data = data;
        }
    }
}
