//Approved 09/22/2022
namespace EpsilonEngine
{
    public static class RandomnessHelper
    {
        #region Public Constants
        public static readonly System.Random RNG = new System.Random();
        #endregion
        #region Public Static Methods
        public static byte[] NextBytes(int bufferSize)
        {
            if (bufferSize < 0)
            {
                throw new System.Exception("bufferSize must be greater than or equal to 0.");
            }
            if (bufferSize is 0)
            {
                return new byte[0];
            }
            byte[] buffer = new byte[bufferSize];
            RNG.NextBytes(buffer);
            return buffer;
        }
        public static int NextInt(int min, int max)
        {
            if (min > max)
            {
                throw new System.Exception("max must be greater or equal to min.");
            }
            if (max == int.MaxValue)
            {
                throw new System.Exception("max must be less than int.MaxValue.");
            }
            if (min == max)
            {
                return min;
            }
            return RNG.Next(min, max + 1);
        }
        public static double NextDouble(double min, double max)
        {
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min >= max)
            {
                throw new System.Exception("max must be greater than or equal to min.");
            }
            if (min == max)
            {
                return min;
            }
            return (RNG.NextDouble() * (max - min)) + min;
        }
        #endregion
    }
}