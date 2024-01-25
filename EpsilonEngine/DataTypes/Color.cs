using System;
namespace EpsilonEngine
{
    public struct Color
    {
        #region Constants
        public static readonly Color White = new Color(255, 255, 255, 255);
        public static readonly Color Black = new Color(0, 0, 0, 255);

        public static readonly Color Transparent = new Color(0, 0, 0, 0);
        public static readonly Color TransparentWhite = new Color(255, 255, 255, 0);
        public static readonly Color TransparentBlack = new Color(0, 0, 0, 0);

        public static readonly Color Red = new Color(255, 0, 0, 255);
        public static readonly Color Yellow = new Color(255, 255, 0, 255);
        public static readonly Color Green = new Color(0, 255, 0, 255);
        public static readonly Color Aqua = new Color(0, 255, 255, 255);
        public static readonly Color Blue = new Color(0, 0, 255, 255);
        public static readonly Color Pink = new Color(255, 0, 255, 255);

        public static readonly Color SoftRed = new Color(255, 150, 150, 255);
        public static readonly Color SoftYellow = new Color(255, 255, 150, 255);
        public static readonly Color SoftGreen = new Color(150, 255, 150, 255);
        public static readonly Color SoftAqua = new Color(150, 255, 255, 255);
        public static readonly Color SoftBlue = new Color(150, 150, 255, 255);
        public static readonly Color SoftPink = new Color(255, 150, 255, 255);
        #endregion
        #region Properties
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }
        public byte A { get; private set; }
        #endregion
        #region Constructors
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
        public Color(uint source)
        {
            byte[] sourceBytes = BitConverter.GetBytes(source);
            R = sourceBytes[0];
            G = sourceBytes[1];
            B = sourceBytes[2];
            A = sourceBytes[3];
        }
        public Color(Microsoft.Xna.Framework.Color source)
        {
            R = source.R;
            G = source.G;
            B = source.B;
            A = source.A;
        }
        #endregion
        #region Overrides
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
                return this == (Color)obj;
            }
        }
        public override int GetHashCode()
        {
            return BitConverter.ToInt32(new byte[4] { R, G, B, A }, 0);
        }
        public static bool operator ==(Color A, Color b)
        {
            return (A.R == b.R) && (A.G == b.G) && (A.B == b.B) && (A.A == b.A);
        }
        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        }
        #endregion
        #region Methods
        public static Color Invert(Color source)
        {
            source.R = (byte)(byte.MaxValue - source.R);
            source.G = (byte)(byte.MaxValue - source.G);
            source.B = (byte)(byte.MaxValue - source.B);
            return source;
        }
        public Color Invert()
        {
            return Invert(this);
        }
        public static uint Pack(Color source)
        {
            return BitConverter.ToUInt32(new byte[4] { source.R, source.G, source.B, source.A }, 0);
        }
        public uint Pack()
        {
            return Pack(this);
        }
        public static Color Unpack(uint source)
        {
            return new Color(source);
        }
        public static Microsoft.Xna.Framework.Color ToXNA(Color source)
        {
            return new Microsoft.Xna.Framework.Color(source.R, source.G, source.B, source.A);
        }
        public Microsoft.Xna.Framework.Color ToXNA()
        {
            return ToXNA(this);
        }
        public static Color FromXNA(Microsoft.Xna.Framework.Color source)
        {
            return new Color(source);
        }
        #endregion
    }
}