using System.IO;
using System.Text;
namespace DontMelt
{
    public static class TextAssetDecoder
    {
        [RegisterAssetCodec(new string[] { "TXT", "TEXT", "HTML", "JSON", "YLM", "XML", "MD" })]
        public static TextAsset DecodeTextAsset(Stream resourceStream, string fullName)
        {
            byte[] assetBytes = new byte[(int)resourceStream.Length];
            resourceStream.Read(assetBytes, 0, (int)resourceStream.Length);
            string output = Encoding.ASCII.GetString(assetBytes);
            return new TextAsset(resourceStream, fullName, output);
        }
    }
}
