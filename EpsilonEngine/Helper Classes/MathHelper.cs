using System;
namespace EpsilonEngine
{
    public static class MathHelper
    {
        #region Clamp
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException();
            }
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentException();
            }
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
        public static double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException();
            }
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
        #endregion
        #region LoopClamp
        public static int LoopClamp(int value, int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException();
            }
            int loopCount = (value - min) / (max - min);
            return value - (loopCount * (max - min));
        }
        public static float LoopClamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentException();
            }
            int loopCount = (int)((value - min) / (max - min));
            return value - (loopCount * (max - min));
        }
        public static double LoopClamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException();
            }
            int loopCount = (int)((value - min) / (max - min));
            return value - (loopCount * (max - min));
        }
        #endregion
        #region Abs
        public static int Abs(int value)
        {
            if (value < 0)
            {
                return value * -1;
            }
            else
            {
                return value;
            }
        }
        public static float Abs(float value)
        {
            if (value < 0)
            {
                return value * -1;
            }
            else
            {
                return value;
            }
        }
        public static double Abs(double value)
        {
            if (value < 0)
            {
                return value * -1;
            }
            else
            {
                return value;
            }
        }
        #endregion
        #region Min
        public static int Min(int A, int B)
        {
            if (A < B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static float Min(float A, float B)
        {
            if (A < B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static double Min(double A, double B)
        {
            if (A < B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        #endregion
        #region Max
        public static int Max(int A, int B)
        {
            if (A > B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static float Max(float A, float B)
        {
            if (A > B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        public static double Max(double A, double B)
        {
            if (A > B)
            {
                return A;
            }
            else
            {
                return B;
            }
        }
        #endregion
        #region Sqrt
        public static int Sqrt(int value)
        {
            return (int)Math.Sqrt(value);
        }
        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
        }
        public static double Sqrt(double value)
        {
            return Math.Sqrt(value);
        }
        #endregion
        #region Lerp
        public static int Lerp(float sample, int a, int b)
        {
            return a + (int)((b - a) * sample);
        }
        public static float Lerp(float sample, float a, float b)
        {
            return a + ((b - a) * sample);
        }
        public static double Lerp(float sample, double a, double b)
        {
            return a + ((b - a) * sample);
        }
        public static int Lerp(double sample, int a, int b)
        {
            return a + (int)((b - a) * sample);
        }
        public static float Lerp(double sample, float a, float b)
        {
            return a + (float)((b - a) * sample);
        }
        public static double Lerp(double sample, double a, double b)
        {
            return a + ((b - a) * sample);
        }
        #endregion
    }
}