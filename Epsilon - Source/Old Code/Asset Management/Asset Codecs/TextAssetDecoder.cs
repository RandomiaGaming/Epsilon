using System.IO;
using System.Text;
namespace EpsilonEngine
{
    public static class TextAssetDecoder
    {
        //Yes there are more text file extensions than the ones listed here but I'm lazy.
        [RegisterAssetCodec(new string[] { "TXT", "TEXT", "HTML", "JSON", "YLM", "XML", "MD" })]
        public static TextAsset LoadTextAsset(Stream sourceStream, string fullName)
        {
            byte[] assetBytes = new byte[(int)sourceStream.Length];
            sourceStream.Read(assetBytes, 0, (int)sourceStream.Length);
            string output = Encoding.ASCII.GetString(assetBytes);
            return new TextAsset(sourceStream, fullName, output);
        }
    }
}