//Approved 09/22/2022
namespace EpsilonEngine
{
    public struct Rect
    {
        #region Public Constants
        public static readonly Rect Zero;
        public static readonly Rect MaxValue = new Rect(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue);
        #endregion
        #region Public Variables
        public int MinX
        {
            get => _minX;
            set
            {
                if (value < _minX)
                {
                    throw new System.Exception("MinX must be less than or equal to MaxX.");
                }
                _minX = value;
            }
        }
        public int MinY
        {
            get => _minY;
            set
            {
                if (value > _maxY)
                {
                    throw new System.Exception("MinY must be less than or equal to MaxY.");
                }
                _minY = value;
            }
        }
        public int MaxX
        {
            get => _maxX;
            set
            {
                if (value < _minX)
                {
                    throw new System.Exception("MaxX must be greater than or equal to MinX.");
                }
                _maxX = value;
            }
        }
        public int MaxY
        {
            get => _maxY;
            set
            {
                if (value < _minY)
                {
                    throw new System.Exception("MaxY must be greater than or equal to MinY.");
                }
                _maxY = value;
            }
        }
        public Point Min
        {
            get => new Point(_minX, _minY);
            set
            {
                if (value.X > _maxX || value.Y > _maxY)
                {
                    throw new System.Exception("Min must be less than or equal to Max.");
                }
                _minX = value.X;
                _minY = value.Y;
            }
        }
        public Point Max
        {
            get => new Point(_minX, _minY);
            set
            {
                if (value.X < _minX || value.Y < _minY)
                {
                    throw new System.Exception("Max must be greater than or equal to Min.");
                }
                _maxX = value.X;
                _maxY = value.Y;
            }
        }
        public int Width => _maxX - _minX + 1;
        public int Height => _maxY - _minY + 1;
        public Point Size => new Point(_maxX - _minX + 1, _maxY - _minY + 1);
        #endregion
        #region Internal Variables
        internal int _minX;
        internal int _minY;
        internal int _maxX;
        internal int _maxY;
        #endregion
        #region Public Constructors
        public Rect(int minX, int minY, int maxX, int maxY)
        {
            if (minX > maxX)
            {
                throw new System.Exception("MaxX must be greater than MinX.");
            }
            _minX = minX;
            _maxX = maxX;
            if (minY > maxY)
            {
                throw new System.Exception("MaxY must be greater than MinY.");
            }
            _minY = minY;
            _maxY = maxY;
        }
        public Rect(Point min, Point max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new System.Exception("Max must be greater or equal to than Min.");
            }
            _minX = min.X;
            _minY = min.Y;
            _maxX = max.X;
            _maxY = max.Y;
        }
        public Rect(Bounds source)
        {
            _minX = (int)source._minX;
            _maxX = (int)source._maxX;
            _minY = (int)source._minY;
            _maxY = (int)source._maxY;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Rect({_minX}, {_minY}, {_maxX}, {_maxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Rect))
            {
                return false;
            }
            else
            {
                Rect a = (Rect)obj;
                return _minX == a._minX && _minY == a._minY && _maxX == a._maxX && _maxY == a._maxY;
            }
        }
        public override int GetHashCode()
        {
            return unchecked(_minX + _minY + _maxX + _maxY);
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Rect a, Rect b)
        {
            return a._minX == b._minX && a._minY == b._minY && a._maxX == b._maxX && a._maxY == b._maxY;
        }
        public static bool operator !=(Rect a, Rect b)
        {
            return a._minX != b._minX || a._minY != b._minY || a._maxX != b._maxX || a._maxY != b._maxY;
        }

        public static Rect operator +(Rect a, Rect b)
        {
            if (b._minX < a._minX)
            {
                a._minX = b._minX;
            }
            if (b._minY < a._minY)
            {
                a._minY = b._minY;
            }
            if (b._maxX > a._maxX)
            {
                a._maxX = b._maxX;
            }
            if (b._maxY > a._maxY)
            {
                a._maxY = b._maxY;
            }
            return a;
        }
        public static Rect operator +(Rect a, Point b)
        {
            if (b.X < a._minX)
            {
                a._minX = b.X;
            }
            if (b.Y < a._minY)
            {
                a._minY = b.Y;
            }
            if (b.X > a._maxX)
            {
                a._maxX = b.X;
            }
            if (b.Y > a._maxY)
            {
                a._maxY = b.Y;
            }
            return a;
        }

        public static Rect operator -(Rect a)
        {
            int temp = -a._minX;
            a._minX = -a._maxX;
            a._maxX = temp;
            temp = -a._minY;
            a._minY = -a._maxY;
            a._maxY = temp;
            return a;
        }

        public static explicit operator Rect(Bounds source)
        {
            Rect output;
            output._minX = (int)source._minX;
            output._maxX = (int)source._maxX;
            output._minY = (int)source._minY;
            output._maxY = (int)source._maxY;
            return output;
        }
        #endregion
        #region Public Methods
        public bool Incapsulates(Point a)
        {
            return a.X >= _minX && a.Y >= _minY && a.X <= _maxX && a.Y <= _maxY;
        }
        public bool Incapsulates(Rect a)
        {
            return a._minX >= _minX && a._minY >= _minY && a._maxX <= _maxX && a._maxY <= _maxY;
        }
        public bool Overlaps(Rect a)
        {
            return a._minX <= _maxX && a._minY <= _maxY && a._maxX >= _minX && a._maxY >= _minY;
        }
        public Rect Invert()
        {
            Rect output = this;
            output._minX = -_maxX;
            output._maxX = -_minX;
            output._minY = -_maxY;
            output._maxY = -_minY;
            return output;
        }
        public Rect InvertX()
        {
            Rect output = this;
            output._minX = -_maxX;
            output._maxX = -_minX;
            return output;
        }
        public Rect InvertY()
        {
            Rect output = this;
            output._minY = -_maxY;
            output._maxY = -_minY;
            return output;
        }
        #endregion
        #region Public Static Methods
        public static Rect Combine(Rect a, Rect b)
        {
            if (b._minX < a._minX)
            {
                a._minX = b._minX;
            }
            if (b._minY < a._minY)
            {
                a._minY = b._minY;
            }
            if (b._maxX > a._maxX)
            {
                a._maxX = b._maxX;
            }
            if (b._maxY > a._maxY)
            {
                a._maxY = b._maxY;
            }
            return a;
        }
        public static Rect Incapsulate(Rect a, Point b)
        {
            if (b.X < a._minX)
            {
                a._minX = b.X;
            }
            if (b.Y < a._minY)
            {
                a._minY = b.Y;
            }
            if (b.X > a._maxX)
            {
                a._maxX = b.X;
            }
            if (b.Y > a._maxY)
            {
                a._maxY = b.Y;
            }
            return a;
        }
        #endregion
    }
}