using System;
namespace EpsilonEngine
{
    public struct Color
    {
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

        public byte R;
        public byte G;
        public byte B;
        public byte A;
        public Color(uint packedColor)
        {
            byte[] packedColorBytes = BitConverter.GetBytes(packedColor);
            R = packedColorBytes[0];
            G = packedColorBytes[1];
            B = packedColorBytes[2];
            A = packedColorBytes[3];
        }
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }
        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        public override string ToString()
        {
            return $"EpsilonEngine.Color({R}, {G}, {B}, {A})";
        }
        public uint Pack()
        {
            return BitConverter.ToUInt32(new byte[] { R, G, B, A }, 0);
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
            return BitConverter.ToInt32(BitConverter.GetBytes(Pack()), 0);
        }
        public static bool operator ==(Color a, Color b)
        {
            return (a.R == b.R) && (a.G == b.G) && (a.B == b.B) && (a.A == b.A);
        }
        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        }
        public Microsoft.Xna.Framework.Color ToXNA()
        {
            return new Microsoft.Xna.Framework.Color(R, G, B, A);
        }
    }
}