//Approved 09/22/2022
namespace EpsilonEngine
{
    public struct Color
    {
        #region Public Constants
        public static readonly Color White = new(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color Black = new(byte.MinValue, byte.MinValue, byte.MinValue, byte.MaxValue);
        public static readonly Color Grey = new(150, 150, 150, byte.MaxValue);
        public static readonly Color DarkGrey = new(100, 100, 100, byte.MaxValue);
        public static readonly Color LightGrey = new(200, 200, 200, byte.MaxValue);

        public static readonly Color Transparent;
        public static readonly Color TransparentWhite = new(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MinValue);
        public static readonly Color TransparentBlack;

        public static readonly Color Red = new(byte.MaxValue, byte.MinValue, byte.MinValue, byte.MaxValue);
        public static readonly Color Yellow = new(byte.MaxValue, byte.MaxValue, byte.MinValue, byte.MaxValue);
        public static readonly Color Green = new(byte.MinValue, byte.MaxValue, byte.MinValue, byte.MaxValue);
        public static readonly Color Aqua = new(byte.MinValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color Blue = new(byte.MinValue, byte.MinValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color Pink = new(byte.MaxValue, byte.MinValue, byte.MaxValue, byte.MaxValue);

        public static readonly Color SoftRed = new(byte.MaxValue, 150, 150, byte.MaxValue);
        public static readonly Color SoftYellow = new(byte.MaxValue, byte.MaxValue, 150, byte.MaxValue);
        public static readonly Color SoftGreen = new(150, byte.MaxValue, 150, byte.MaxValue);
        public static readonly Color SoftAqua = new(150, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static readonly Color SoftBlue = new(150, 150, byte.MaxValue, byte.MaxValue);
        public static readonly Color SoftPink = new(byte.MaxValue, 150, byte.MaxValue, byte.MaxValue);
        #endregion
        #region Public Variables
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        #endregion
        #region Public Constructors
        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = byte.MaxValue;
        }
        public Color(uint packedValue)
        {
            R = (byte)packedValue;
            G = (byte)(packedValue >> 8);
            B = (byte)(packedValue >> 16);
            A = (byte)(packedValue >> 24);
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Color({R}, {G}, {B}, {A})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Color))
            {
                return false;
            }
            else
            {
                Color a = (Color)obj;
                return R == a.R && G == a.G && B == a.B && A == a.A;
            }
        }
        public override int GetHashCode()
        {
            return R | (G << 8) | (B << 16) | (A << 24);
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Color a, Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;
        }
        public static bool operator !=(Color a, Color b)
        {
            return a.R != b.R || a.G != b.G || a.B != b.B || a.A != b.A;
        }

        public static Color operator -(Color a)
        {
            a.R = (byte)(a.R ^ byte.MaxValue);
            a.G = (byte)(a.G ^ byte.MaxValue);
            a.B = (byte)(a.B ^ byte.MaxValue);
            return a;
        }

        public static explicit operator Color(uint source)
        {
            Color output;
            output.R = (byte)source;
            output.G = (byte)(source >> 8);
            output.B = (byte)(source >> 16);
            output.A = (byte)(source >> 24);
            return output;
        }
        public static explicit operator uint(Color source)
        {
            return source.R | ((uint)source.G << 8) | ((uint)source.B << 16) | ((uint)source.A << 24);
        }
        #endregion
        #region Public Methods
        public uint Pack()
        {
            return R | ((uint)G << 8) | ((uint)B << 16) | ((uint)A << 24);
        }
        public Color Invert()
        {
            Color output = this;
            output.R = (byte)(R ^ byte.MaxValue);
            output.G = (byte)(G ^ byte.MaxValue);
            output.B = (byte)(B ^ byte.MaxValue);
            return output;
        }
        #endregion
        #region Public Static Methods
        //Note: Mixes two colors with respect to alpha.
        public static Color Mix(Color front, Color back)
        {
            int tempA = front.A ^ byte.MaxValue;
            int temp0 = (255 * front.A) + (255 * back.A) - (front.A * back.A);
            int temp1 = 255 * front.A;
            int tempHue = back.R * back.A;
            front.R = (byte)((temp1 * front.R / temp0) + (((255 * tempHue) - (front.A * tempHue)) / temp0));
            tempHue = back.G * back.A;
            front.G = (byte)((temp1 * front.G / temp0) + (((255 * tempHue) - (front.A * tempHue)) / temp0));
            tempHue = back.B * back.A;
            front.B = (byte)((temp1 * front.B / temp0) + (((255 * tempHue) - (front.A * tempHue)) / temp0));
            front.A = (byte)((tempA - (back.A * tempA / 255)) ^ 255);
            return front;
        }
        //Note: Mixes two colors by averaging their R, G, B, and A values.
        public static Color Average(Color a, Color b)
        {
            a.R = (byte)((a.R + b.R) >> 1);
            a.G = (byte)((a.G + b.G) >> 1);
            a.B = (byte)((a.B + b.B) >> 1);
            a.A = (byte)((a.A + b.A) >> 1);
            return a;
        }
        //Note: Mixes two colors by averaging their R, G, and B values. The alpha of the output is always 255.
        public static Color AverageRGB(Color a, Color b)
        {
            a.R = (byte)((a.R + b.R) >> 1);
            a.G = (byte)((a.G + b.G) >> 1);
            a.B = (byte)((a.B + b.B) >> 1);
            a.A = byte.MaxValue;
            return a;
        }
        #endregion
    }
}