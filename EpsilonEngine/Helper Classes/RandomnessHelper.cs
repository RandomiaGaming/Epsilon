using System;

namespace EpsilonEngine
{
    //Use with causion. 9 times out of 10 randomness in video games is not fun. Only use this if it actually adds something to your game.
    public static class RandomnessHelper
    {
        private static readonly Random _rng = new Random((int)DateTime.Now.Ticks);
        public static int NextInt()
        {
            return BitConverter.ToInt32(NextBytes(4), 0);
        }
        public static int NextInt(int min, int max)
        {
            if(min > max)
            {
                throw new Exception("max must be greater than min.");
            }
            if(min == max)
            {
                return min;
            }
            if(max == int.MaxValue)
            {
                return _rng.Next(min, max);
            }
            return _rng.Next(min, max + 1);
        }
        public static byte[] NextBytes(int bufferSize)
        {
            if(bufferSize < 0)
            {
                throw new Exception("buffer size must be greater than or equal to 0.");
            }
            if(bufferSize == 0)
            {
                return new byte[0];
            }
            byte[] buffer = new byte[bufferSize];
            _rng.NextBytes(buffer);
            return buffer;
        }
        //This method will never return max.
        public static double NextDouble(double min, double max)
        {
            if (min > max)
            {
                throw new Exception("max must be greater than min.");
            }
            if (min == max)
            {
                return min;
            }
            return min + (_rng.NextDouble() * (max - min));
        }
    }
}