using System;
namespace DontMelt
{
    public struct Color
    {
        public static readonly Color White = new Color(255, 255, 255, 255);
        public static readonly Color Black = new Color(0, 0, 0, 255);
        public static readonly Color Clear = new Color(255, 255, 255, 0);
        public static readonly Color Red = new Color(255, 0, 0, 255);
        public static readonly Color Green = new Color(0, 255, 0, 255);
        public static readonly Color Blue = new Color(0, 0, 255, 255);
        public static readonly Color Pink = new Color(255, 0, 255, 255);

        public byte r;
        public byte g;
        public byte b;
        public byte a;
        public Color(int r, int g, int b)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
            a = 255;
        }
        public Color(int r, int g, int b, int a)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
            this.a = (byte)a;
        }
        public override string ToString()
        {
            return $"({r}, {g}, {b}, {a})";
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
            return base.GetHashCode();
        }
        public static bool operator ==(Color a, Color b)
        {
            return (a.r == b.r) && (a.g == b.g) && (a.b == b.b) && (a.a == b.a);
        }
        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        } 
    }
}