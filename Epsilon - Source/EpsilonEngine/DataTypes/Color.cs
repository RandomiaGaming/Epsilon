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
        #region Variables
        private byte _r;
        private byte _g;
        private byte _b;
        private byte _a;
        #endregion
        #region Properties
        public byte R
        {
            get
            {
                return _r;
            }
        }
        public byte G
        {
            get
            {
                return _g;
            }
        }
        public byte B
        {
            get
            {
                return _b;
            }
        }
        public byte A
        {
            get
            {
                return _a;
            }
        }
        #endregion
        #region Constructors
        public Color(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;
            _a = byte.MaxValue;
        }
        public Color(byte r, byte g, byte b, byte a)
        {
            _r = r;
            _g = g;
            _b = b;
            _a = a;
        }
        public Color(uint source)
        {
            byte[] sourceBytes = BitConverter.GetBytes(source);
            _r = sourceBytes[0];
            _g = sourceBytes[1];
            _b = sourceBytes[2];
            _a = sourceBytes[3];
        }
        public Color(Microsoft.Xna.Framework.Color source)
        {
            _r = source.R;
            _g = source.G;
            _b = source.B;
            _a = source.A;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Color({_r}, {_g}, {_b}, {_a})";
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
            return BitConverter.ToInt32(new byte[4] { _r, _g, _b, _a }, 0);
        }
        public static bool operator ==(Color a, Color b)
        {
            return (a._r == b._r) && (a._g == b._g) && (a._b == b._b) && (a._a == b._a);
        }
        public static bool operator !=(Color a, Color b)
        {
            return (a._r != b._r) || (a._g != b._g) || (a._b != b._b) || (a._a != b._a);
        }
        #endregion
        #region Methods
        public static Color Invert(Color source)
        {
            return new Color((byte)(byte.MaxValue - source._r), (byte)(byte.MaxValue - source._g), (byte)(byte.MaxValue - source._b), source._a);
        }
        public Color Invert()
        {
            return new Color((byte)(byte.MaxValue - _r), (byte)(byte.MaxValue - _g), (byte)(byte.MaxValue - _b), _a);
        }
        public static uint Pack(Color source)
        {
            return BitConverter.ToUInt32(new byte[4] { source._r, source._g, source._b, source._a }, 0);
        }
        public uint Pack()
        {
            return BitConverter.ToUInt32(new byte[4] { _r, _g, _b, _a }, 0);
        }
        public static Color Unpack(uint source)
        {
            return new Color(source);
        }
        public static Microsoft.Xna.Framework.Color ToXNA(Color source)
        {
            return new Microsoft.Xna.Framework.Color(source._r, source._g, source._b, source._a);
        }
        public Microsoft.Xna.Framework.Color ToXNA()
        {
            return Color.ToXNA(this);
        }
        public static Color FromXNA(Microsoft.Xna.Framework.Color source)
        {
            return new Color(source);
        }
        #endregion
    }
}