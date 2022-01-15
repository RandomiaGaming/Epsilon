using System.IO;
namespace EpsilonEngine
{
    public static class BinaryAssetCodec
    {
        [RegisterAssetCodec(new string[] { "BIN", "DUMP", "DMP" })]
        public static BianaryAsset DecodeBinaryAsset(Stream sourceStream, string fullName)
        {
            byte[] output = new byte[(int)sourceStream.Length];
            sourceStream.Read(output, 0, (int)sourceStream.Length);
            return new BianaryAsset(sourceStream, fullName, output);
        }
    }
}