//Approved 09/22/2022
namespace EpsilonEngine
{
    public static class MathHelper
    {
        #region Public Constants
        public const double Tau = 6.2831853071795862;
        public const double PI = 3.1415926535897931;
        public const double E = 2.7182818284590451;
        public const double Sqrt2 = 1.4142135623731;
        public const double Sqrt3 = 1.73205080756888;
        public const double Cbrt2 = 1.25992104989487;
        public const double Cbrt3 = 1.44224957030741;
        public const double RadToDeg = 57.2957795130823;
        public const double DegToRad = 0.0174532925199433;
        public const double Sin45Deg = 0.707106781186547;
        public const double SinPIOver4 = 0.707106781186547;
        public const double Sin60Deg = 0.866025403784439;
        public const double SinPIOver3 = 0.866025403784439;
        #endregion
        #region Public Static Methods
        public static double Abs(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 0.0)
            {
                return -input;
            }
            return input;
        }
        public static double ACos(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input > 1.0)
            {
                throw new System.Exception("input must be less than or equal to 1.");
            }
            if (input < -1.0)
            {
                throw new System.Exception("input must be greater than or equal to -1.");
            }
            return System.Math.Acos(input);
        }
        public static double ASin(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input > 1.0)
            {
                throw new System.Exception("input must be less than or equal to 1.");
            }
            if (input < -1.0)
            {
                throw new System.Exception("input must be greater than or equal to -1.");
            }
            return System.Math.Asin(input);
        }
        public static double ATan(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Atan(input);
        }
        public static double Cbrt(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 0.0)
            {
                return -System.Math.Pow(-input, 0.33333333333333331);
            }
            return System.Math.Pow(input, 0.33333333333333331);
        }
        public static double Ceil(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Ceiling(input);
        }
        public static double Clamp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (input > max)
            {
                return max;
            }
            else if (input < min)
            {
                return min;
            }
            return input;
        }
        public static double Clamp01(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input > 1.0)
            {
                return 1.0;
            }
            else if (input < 0.0)
            {
                return 0.0;
            }
            return input;
        }
        public static double LoopClamp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (min == max)
            {
                return min;
            }
            double span = max - min;
            return input - (System.Math.Floor((input - min) / span) * span);
        }
        public static double LoopClamp01(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return input - System.Math.Floor(input);
        }
        public static double PingPongClamp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (min == max)
            {
                return min;
            }
            double loopCount = System.Math.Floor(input / (max - min));
            if (input < 0.0)
            {
                if (loopCount % 2.0 == -1.0)
                {
                    return max - (input - (loopCount * (max - min)));
                }
                return min + (input - (loopCount * (max - min)));
            }
            else if (loopCount % 2.0 == 1.0)
            {
                return max - (input - (loopCount * (max - min)));
            }
            return min + (input - (loopCount * (max - min)));
        }
        public static double PingPongClamp01(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            double loopCount = System.Math.Floor(input);
            if (input < 0.0)
            {
                if (loopCount % 2.0 == -1.0)
                {
                    return 1.0 - input + loopCount;
                }
                return input - loopCount;
            }
            else if (loopCount % 2.0 == 1.0)
            {
                return 1.0 - input + loopCount;
            }
            return input - loopCount;
        }
        public static double Cos(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Cos(input);
        }
        public static double Rem(double dividend, double divisor)
        {
            if (dividend is double.NaN || dividend is double.PositiveInfinity || dividend is double.NegativeInfinity)
            {
                throw new System.Exception("dividend must be a real number.");
            }
            if (divisor is double.NaN || divisor is double.PositiveInfinity || divisor is double.NegativeInfinity)
            {
                throw new System.Exception("divisor must be a real number.");
            }
            if (divisor == 0.0)
            {
                throw new System.Exception("divisor cannot be 0.");
            }
            double quotient = dividend / divisor;
            if (quotient < 0.0)
            {
                quotient = -System.Math.Floor(-quotient);
            }
            else
            {
                quotient = System.Math.Floor(quotient);
            }
            return dividend - (quotient * divisor);
        }
        public static double Exp(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Exp(input);
        }
        public static double Floor(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Floor(input);
        }
        public static double Ln(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 0.0)
            {
                throw new System.Exception("input must be greater than or equal to 0.");
            }
            return System.Math.Log(input);
        }
        public static double Log(double input, double logBase)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (logBase is double.NaN || logBase is double.PositiveInfinity || logBase is double.NegativeInfinity)
            {
                throw new System.Exception("logBase must be a real number.");
            }
            if (input < 0.0)
            {
                throw new System.Exception("input must be greater than or equal to 0.");
            }
            if (logBase == 1.0)
            {
                throw new System.Exception("logBase cannot be 1.");
            }
            if (logBase <= 0.0)
            {
                throw new System.Exception("logBase must be greater than 0.");
            }
            return System.Math.Log(input, logBase);
        }
        public static double Log2(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 0.0)
            {
                throw new System.Exception("input must be greater than or equal to 0.");
            }
            return System.Math.Log(input, 2.0);
        }
        public static double Log10(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 0.0)
            {
                throw new System.Exception("input must be greater than or equal to 0.");
            }
            return System.Math.Log(input, 10.0);
        }
        public static double Max(double a, double b)
        {
            if (a is double.NaN || a is double.PositiveInfinity || a is double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b is double.NaN || b is double.PositiveInfinity || b is double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a < b)
            {
                return b;
            }
            return a;
        }
        public static double Min(double a, double b)
        {
            if (a is double.NaN || a is double.PositiveInfinity || a is double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b is double.NaN || b is double.PositiveInfinity || b is double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a > b)
            {
                return b;
            }
            return a;
        }
        public static double Pow(double powBase, double exponent)
        {
            if (powBase is double.NaN || powBase is double.PositiveInfinity || powBase is double.NegativeInfinity)
            {
                throw new System.Exception("powBase must be a real number.");
            }
            if (exponent is double.NaN || exponent is double.PositiveInfinity || exponent is double.NegativeInfinity)
            {
                throw new System.Exception("exponent must be a real number.");
            }
            if (powBase < 0.0 && exponent != System.Math.Floor(exponent))
            {
                throw new System.Exception("If powBase is less than 0 then exponent must be an integer.");
            }
            return System.Math.Pow(powBase, exponent);
        }
        public static double Recip(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input == 0.0)
            {
                throw new System.Exception("input cannot be 0.");
            }
            return 1.0 / input;
        }
        public static double Round(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Round(input);
        }
        public static double Sign(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input is 0.0)
            {
                return 0.0;
            }
            else if (input < 0.0)
            {
                return -1.0;
            }
            return 1.0;
        }
        public static double Sin(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Sin(input);
        }
        public static bool IsInt(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return input == System.Math.Floor(input);
        }
        public static double Sqrt(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Sqrt(input);
        }
        public static double Tan(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Tan(input);
        }
        public static double ConstInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            return min + (System.Math.Floor(input + 0.5) * (max - min));
        }
        public static double ConstLoopInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (input < 0.0)
            {
                if (-2.0 * input % 2.0 <= 1.0)
                {
                    return min;
                }
                else
                {
                    return max;
                }
            }
            else if (2.0 * input % 2.0 <= 1.0)
            {
                return min;
            }
            else
            {
                return max;
            }
        }
        public static double ConstPingPongInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (input < 0.0)
            {
                if ((0.5 - input) % 2.0 <= 1.0)
                {
                    return min;
                }
                else
                {
                    return max;
                }
            }
            else if ((input + 0.5) % 2.0 <= 1.0)
            {
                return min;
            }
            else
            {
                return max;
            }
        }
        public static double LinInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            return min + (input * (max - min));
        }
        public static double LinLoopInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            input = input - System.Math.Floor(input);
            return min + (input * (max - min));
        }
        public static double LinPingPongInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            if (min == max)
            {
                return min;
            }
            double loopCount = System.Math.Floor(input);
            if (input < 0.0)
            {
                if (loopCount % 2.0 == -1.0)
                {
                    return max - ((input - loopCount) * (max - min));
                }
                return min + ((input - loopCount) * (max - min));
            }
            else if (loopCount % 2.0 == 1.0)
            {
                return max - ((input - loopCount) * (max - min));
            }
            return min + ((input - loopCount) * (max - min));
        }
        public static double SinInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            double range = max - min;
            double halfRange = range / 2.0;
            double inputFloor = System.Math.Floor(input);
            return (halfRange * System.Math.Sin((input - inputFloor - 0.5) * PI)) + halfRange + min + (inputFloor * range);
        }
        public static double SinLoopInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            double range = max - min;
            double halfRange = range / 2.0;
            double inputFloor = System.Math.Floor(input);
            return (halfRange * System.Math.Sin((input - inputFloor - 0.5) * PI)) + halfRange + min;
        }
        public static double SinPingPongInterp(double input, double min, double max)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (min is double.NaN || min is double.PositiveInfinity || min is double.NegativeInfinity)
            {
                throw new System.Exception("min must be a real number.");
            }
            if (max is double.NaN || max is double.PositiveInfinity || max is double.NegativeInfinity)
            {
                throw new System.Exception("max must be a real number.");
            }
            if (min > max)
            {
                throw new System.Exception("min cannot be greater than max.");
            }
            double range = max - min;
            double halfRange = range / 2.0;
            return (halfRange * System.Math.Sin((input - 0.5) * PI)) + halfRange + min;
        }
        public static double Csc(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return 1.0 / System.Math.Sin(input);
        }
        public static double Sec(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return 1.0 / System.Math.Cos(input);
        }
        public static double Cot(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return 1.0 / System.Math.Tan(input);
        }
        public static double ACsc(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 1.0 && input > -1.0)
            {
                throw new System.Exception("input must be less than or equal to -1 or greater than or equal to 1.");
            }
            return System.Math.Asin(1.0 / input);
        }
        public static double ASec(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 1.0 && input > -1.0)
            {
                throw new System.Exception("input must be less than or equal to -1 or greater than or equal to 1.");
            }
            return System.Math.Acos(1.0 / input);
        }
        public static double ACot(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return System.Math.Atan(1.0 / input);
        }
        public static double Fact(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 0.0)
            {
                throw new System.Exception("input must be greater than or equal to 0.");
            }
            if (input != System.Math.Floor(input))
            {
                throw new System.Exception("input must be an integer.");
            }
            double output = 1.0;
            while (input > 0.0)
            {
                output *= input;
                input--;
            }
            return output;
        }
        public static double Sqr(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return input * input;
        }
        public static double Cube(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return input * input * input;
        }
        public static double MinMag(double a, double b)
        {
            if (a is double.NaN || a is double.PositiveInfinity || a is double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b is double.NaN || b is double.PositiveInfinity || b is double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a >= 0.0 && b >= 0.0)
            {
                if (a > b)
                {
                    return b;
                }
                return a;
            }
            else if (a < 0.0 && b < 0.0)
            {
                if (a < b)
                {
                    return b;
                }
                return a;
            }
            else if (a >= 0.0 && b < 0.0)
            {
                if (a > -b)
                {
                    return b;
                }
                return a;
            }
            else if (a < -b)
            {
                return b;
            }
            return a;
        }
        public static double MaxMag(double a, double b)
        {
            if (a is double.NaN || a is double.PositiveInfinity || a is double.NegativeInfinity)
            {
                throw new System.Exception("a must be a real number.");
            }
            if (b is double.NaN || b is double.PositiveInfinity || b is double.NegativeInfinity)
            {
                throw new System.Exception("b must be a real number.");
            }
            if (a >= 0.0 && b >= 0.0)
            {
                if (a < b)
                {
                    return b;
                }
                return a;
            }
            else if (a < 0.0 && b < 0.0)
            {
                if (a > b)
                {
                    return b;
                }
                return a;
            }
            else if (a >= 0.0 && b < 0.0)
            {
                if (a < -b)
                {
                    return b;
                }
                return a;
            }
            else if (a > -b)
            {
                return b;
            }
            return a;
        }
        public static double CeilMag(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 0.0)
            {
                return -System.Math.Ceiling(-input);
            }
            return System.Math.Ceiling(input);
        }
        public static double FloorMag(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            if (input < 0.0)
            {
                return -System.Math.Floor(-input);
            }
            return System.Math.Floor(input);
        }
        public static bool IsRealNumber(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                return false;
            }
            return true;
        }
        public static double RadToDegAcc(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return (input * 180.0) / PI;
        }
        public static double DegToRadAcc(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return (input * PI) / 180.0;
        }
        public static double AngleClamp(double input)
        {
            if (input is double.NaN || input is double.PositiveInfinity || input is double.NegativeInfinity)
            {
                throw new System.Exception("input must be a real number.");
            }
            return input - (System.Math.Floor((input + PI) / Tau) * Tau);
        }
        public static double AngleNonLoopDif(double a, double b)
        {
            double dif = a - b;
            if (dif < 0)
            {
                dif = -dif;
            }
            return dif - (System.Math.Floor((dif + PI) / Tau) * Tau);
        }
        #endregion
        #region Under Construction
        /*
                public static double AngleLoopDif(double a, double b)
                {
                    double dif = a - b;
                    return dif - (System.Math.Floor((dif + PI) / Tau) * Tau);
                }

                //Stuff below belongs in GeometryHelper..
                //CartisianToPolar
                //PolarToCartisian
                //Rotate Vector
                public static Vector RotateVector(Vector input, double rotation)
                {
                    double ca = System.Math.Cos((rotation * PI) / 180);
                    double sa = System.Math.Sin(rotation * DegToRad);
                    return new Vector(ca * input.X - sa * input.Y, sa * input.X + ca * input.Y);
                }
                //DisToLine
                //ClosestPointOnLine
                //PointInTri
                //ClosestPointInTri
                //DistanceToTri
                //ClosestPointInCircle
                //DistanceToCircle
                public static double VectorAngle(Vector vector)
                {
                    return System.Math.Atan2(vector.Y, vector.X);
                }
                public static double VectorMag(Vector input)
                {
                    return System.Math.Sqrt((input.X * input.X) + (input.Y * input.Y));
                }
                public static Vector ClampUnitCircle(Vector input)
                {
                    double distance = System.Math.Sqrt((input.X * input.X) + (input.Y * input.Y));
                    return new Vector(input.X / distance, input.Y / distance);
                }
                */
        #endregion
    }
}