using System;
using System.IO;

namespace DontMelt
{
    public static class EEMetaDecoder
    {
        private static byte[] EncodeTexture(Texture data)
        {
            byte[] widthBytes = BitConverter.GetBytes(data.width);
            byte[] heightBytes = BitConverter.GetBytes(data.height);
            Color[] dataColors = data.GetData();
            byte[] dataBytes = new byte[data.width * data.height * 4];
            for (int i = 0; i < dataColors.Length; i++)
            {
                int baseIndex = i * 4;
                Color pixelColor = dataColors[i];
                dataBytes[baseIndex] = pixelColor.r;
                dataBytes[baseIndex + 1] = pixelColor.g;
                dataBytes[baseIndex + 2] = pixelColor.b;
                dataBytes[baseIndex + 3] = pixelColor.a;
            }
            byte[] output = new byte[8 + dataBytes.Length];
            Array.Copy(widthBytes, 0, output, 0, 4);
            Array.Copy(heightBytes, 0, output, 4, 4);
            Array.Copy(dataBytes, 0, output, 8, dataBytes.Length);
            return output;
        }
        private static Texture DecodeTexture(byte[] data)
        {
            if (data.Length < 4)
            {
                throw new ArgumentException();
            }

            byte[] widthBytes = new byte[2];
            Array.Copy(data, 0, widthBytes, 0, 2);
            ushort width = BitConverter.ToUInt16(widthBytes, 0);

            byte[] heightBytes = new byte[2];
            Array.Copy(data, 4, heightBytes, 0, 2);
            ushort height = BitConverter.ToUInt16(heightBytes, 0);

            if ((data.Length - 4) / 4 != width * height)
            {
                throw new ArgumentException();
            }

            byte[] pixelDataBytes = new byte[data.Length - 8];
            Array.Copy(data, 8, pixelDataBytes, 0, data.Length - 8);
            Color[] pixelData = new Color[width * height];

            for (int i = 0; i < pixelData.Length; i++)
            {
                int baseIndex = i * 4;
                pixelData[i] = new Color(pixelDataBytes[baseIndex], pixelDataBytes[baseIndex + 1], pixelDataBytes[baseIndex + 2], pixelDataBytes[baseIndex + 3]);
            }

            Texture output = new Texture(width, height, pixelData);
            return output;
        }
        [RegisterAssetCodec(new string[] { "EEM", "EEMeta" })]
        public static AssetBase DecodeEEMetaAsset(Stream sourceStream, string fullName)
        {
            byte[] dataBytes = new byte[sourceStream.Length];
            sourceStream.Read(dataBytes, 0, (int)sourceStream.Length);
            return new TextureAsset(sourceStream, fullName, DecodeTexture(dataBytes));
        }
    }
}