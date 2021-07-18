using System.IO;
using System;
namespace DontMelt
{
    public sealed class AudioAsset : AssetBase
    {
        public readonly AudioClip data;
        public AudioAsset(Stream sourceStream, string fullName, AudioClip data) : base(sourceStream, fullName)
        {
            if(data.sampleRate <= 0)
            {
                throw new ArgumentException();
            }
            this.data = data;
        }
    }
}
