using System;

namespace EpsilonEngine
{
    public static class RandomnessHelper
    {
        private static readonly Random rng = new Random();

        public static int Next()
        {
            return rng.Next();
        }
        public static int Next(int min, int max)
        {
            return rng.Next(min, max + 1);
        }
        public static int Next(int max)
        {
            return rng.Next(max + 1);
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
        public static double NextDouble()
        {
            return rng.NextDouble();
        }
    }
}