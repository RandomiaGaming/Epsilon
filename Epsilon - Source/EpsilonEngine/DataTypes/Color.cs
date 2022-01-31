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
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        #endregion
        #region Constructors
        public Color(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = byte.MaxValue;
        }
        public Color(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }
        public Color(uint source)
        {
            byte[] sourceBytes = BitConverter.GetBytes(source);
            this.R = sourceBytes[0];
            this.G = sourceBytes[1];
            this.B = sourceBytes[2];
            this.A = sourceBytes[3];
        }
        public Color(Microsoft.Xna.Framework.Color source)
        {
            this.R = source.R;
            this.G = source.G;
            this.B = source.B;
            this.A = source.A;
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
            return (a.R != b.R) || (a.G != b.G) || (a.B != b.B) || (a.A != b.A);
        }
        #endregion
        #region Methods
        public static Color Invert(Color source)
        {
            source.R = (byte)(byte.MaxValue - source.R);
            source.G = (byte)(byte.MaxValue - source.G);
            source.B = (byte)(byte.MaxValue - source.B);
            source.A = (byte)(byte.MaxValue - source.A);
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