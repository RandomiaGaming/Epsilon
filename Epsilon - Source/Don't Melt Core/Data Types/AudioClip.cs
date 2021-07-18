using System;
namespace DontMelt
{
    public sealed class AudioClip
    {
        public readonly int sampleRate = 48000;
        private readonly byte[] data = new byte[0];
        public AudioClip(int sampleRate, byte[] data)
        {
            if (sampleRate <= 0)
            {
                throw new ArgumentException();
            }
            if (data is null)
            {
                throw new ArgumentException();
            }
            this.data = (byte[])data.Clone();
            this.sampleRate = sampleRate;
        }
        public byte[] GetData()
        {
            return (byte[])data.Clone();
        }
    }
}
