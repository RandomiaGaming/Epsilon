using System;
namespace EpsilonEngine
{
    public struct Rectangle
    {
        #region Variables
        private int _minX;
        private int _minY;
        private int _maxX;
        private int _maxY;
        #endregion
        #region Properties
        public int MinX
        {
            get
            {
                return _minX;
            }
        }
        public int MinY
        {
            get
            {
                return _minY;
            }
        }
        public int MaxX
        {
            get
            {
                return _maxX;
            }
        }
        public int MaxY
        {
            get
            {
                return _maxY;
            }
        }
        public Point Min
        {
            get
            {
                return new Point(_minX, _minY);
            }
        }
        public Point Max
        {
            get
            {
                return new Point(_maxX, _maxY);
            }
        }
        public int Width
        {
            get
            {
                return _maxX - _minX + 1;
            }
        }
        public int Height
        {
            get
            {
                return _maxY - _minY + 1;
            }
        }
        public Point Size
        {
            get
            {
                return new Point(_maxX - _minX + 1, _maxY - _minY + 1);
            }
        }
        #endregion
        #region Constructors
        public Rectangle(Point min, Point max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new Exception("Max must be greater than Min.");
            }
            _minX = min.X;
            _minY = min.Y;
            _maxX = max.X;
            _maxY = max.Y;
        }
        public Rectangle(int minX, int minY, int maxX, int maxY)
        {
            if (minX > maxX)
            {
                throw new Exception("MaxX must be greater than MinX.");
            }
            if (minY > maxY)
            {
                throw new Exception("MaxY must be greater than MinY.");
            }
            _minX = minX;
            _minY = minY;
            _maxX = maxX;
            _maxY = maxY;
        }
        public Rectangle(Microsoft.Xna.Framework.Rectangle source)
        {
            _minX = source.Left;
            _minY = source.Top;
            _maxX = source.Right;
            _maxY = source.Bottom;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Rectangle({_minX}, {_minY}, {_maxX}, {_maxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Rectangle))
            {
                return false;
            }
            else
            {
                return this == (Rectangle)obj;
            }
        }
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return (a._minX == b._minX) && (a._minY == b._minY) && (a._maxX == b._maxX) && (a._maxY == b._maxY);
        }
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return (a._minX != b._minX) || (a._minY != b._minY) || (a._maxX != b._maxX) || (a._maxY != b._maxY);
        }
        #endregion
        #region Methods
        public static bool Incapsulates(Rectangle a, Point b)
        {
            if (b.X >= a._minX && b.X <= a._maxX && b.Y >= a._minY && b.Y <= a._maxY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Incapsulates(Point a)
        {
            return Incapsulates(this, a);
        }
        public static bool Incapsulates(Rectangle a, Rectangle b)
        {
            if (b._maxY <= a._maxY && b._minY >= a._minY && b._maxX <= a._maxX && b._minX >= a._minX)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Incapsulates(Rectangle a)
        {
            return Incapsulates(this, a);
        }
        public static bool Overlaps(Rectangle a, Rectangle b)
        {
            if (a._maxX < b._minX || a._minX > b._maxX || a._maxY < b._minY || a._minY > b._maxY)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Overlaps(Rectangle a)
        {
            return Overlaps(this, a);
        }
        public static Microsoft.Xna.Framework.Rectangle ToXNA(Rectangle source)
        {
            return new Microsoft.Xna.Framework.Rectangle(source._minX, source._maxY, source.Width, source.Height);
        }
        public Microsoft.Xna.Framework.Rectangle ToXNA()
        {
            return ToXNA(this);
        }
        public static Rectangle FromXNA(Microsoft.Xna.Framework.Rectangle source)
        {
            return new Rectangle(source);
        }
        #endregion
    }
}