using System;

namespace EpsilonEngine
{
    //Use with causion. 9 times out of 10 randomness in video games is not fun. Only use this if it actually adds something to your game.
    public static class RandomnessHelper
    {
        private static readonly Random rng = new Random();
        public static int NextInt()
        {
            return BitConverter.ToInt32(NextBytes(4), 0);
        }
        public static int NextInt(int min, int max)
        {
            return rng.Next(min, max + 1);
        }
        public static byte[] NextBytes(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            rng.NextBytes(buffer);
            return buffer;
        }
        public static void NextBytes(byte[] buffer)
        {
            rng.NextBytes(buffer);
        }
    }
}