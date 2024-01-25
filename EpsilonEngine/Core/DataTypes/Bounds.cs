//Approved 09/22/2022
namespace EpsilonEngine
{
    public struct Bounds
    {
        #region Public Constants
        public static readonly Bounds Zero;
        public static readonly Bounds MaxValue = new Bounds(double.MinValue, double.MinValue, double.MaxValue, double.MaxValue);
        public static readonly Bounds Infinity = new Bounds(double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity, double.PositiveInfinity);
        #endregion
        #region Public Variables
        public double MinX
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
        public double MinY
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
        public double MaxX
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
        public double MaxY
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
        public Vector Min
        {
            get => new Vector(_minX, _minY);
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
        public Vector Max
        {
            get => new Vector(_maxX, _maxY);
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
        public double Width => _maxX - _minX;
        public double Height => _maxY - _minY;
        public Vector Size => new Vector(_maxX - _minX, _maxY - _minY);
        #endregion
        #region Internal Variables
        internal double _minX;
        internal double _minY;
        internal double _maxX;
        internal double _maxY;
        #endregion
        #region Public Constructors
        public Bounds(double minX, double minY, double maxX, double maxY)
        {
            if(minX is double.NaN)
            {
                throw new System.Exception("minX cannot be NaN.");
            }
            if (maxX is double.NaN)
            {
                throw new System.Exception("maxX cannot be NaN.");
            }
            if (minX > maxX)
            {
                throw new System.Exception("MaxX must be greater than MinX.");
            }
            _minX = minX;
            _maxX = maxX;
            if (minY is double.NaN)
            {
                throw new System.Exception("minY cannot be NaN.");
            }
            if (maxY is double.NaN)
            {
                throw new System.Exception("maxY cannot be NaN.");
            }
            if (minY > maxY)
            {
                throw new System.Exception("MaxY must be greater than MinY.");
            }
            _minY = minY;
            _maxY = maxY;
        }
        public Bounds(Vector min, Vector max)
        {
            if (min.X is double.NaN || min.Y is double.NaN)
            {
                throw new System.Exception("min cannot contain NaN.");
            }
            if (max.X is double.NaN || max.Y is double.NaN)
            {
                throw new System.Exception("max cannot contain NaN.");
            }
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new System.Exception("Max must be greater or equal to than Min.");
            }
            _minX = min.X;
            _minY = min.Y;
            _maxX = max.X;
            _maxY = max.Y;
        }
        public Bounds(Rect source)
        {
            _minX = source._minX;
            _maxX = source._maxX;
            _minY = source._minY;
            _maxY = source._maxY;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Bounds({_minX}, {_minY}, {_maxX}, {_maxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Bounds))
            {
                return false;
            }
            else
            {
                Bounds a = (Bounds)obj;
                return _minX == a._minX && _minY == a._minY && _maxX == a._maxX && _maxY == a._maxY;
            }
        }
        public override int GetHashCode()
        {
            return unchecked(_minX.GetHashCode() + _minY.GetHashCode() + _maxX.GetHashCode() + _maxY.GetHashCode());
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Bounds a, Bounds b)
        {
            return a._minX == b._minX && a._minY == b._minY && a._maxX == b._maxX && a._maxY == b._maxY;
        }
        public static bool operator !=(Bounds a, Bounds b)
        {
            return a._minX != b._minX || a._minY != b._minY || a._maxX != b._maxX || a._maxY != b._maxY;
        }

        public static Bounds operator +(Bounds a, Bounds b)
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
        public static Bounds operator +(Bounds a, Vector b)
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

        public static Bounds operator -(Bounds a)
        {
            double temp = -a._minX;
            a._minX = -a._maxX;
            a._maxX = temp;
            temp = -a._minY;
            a._minY = -a._maxY;
            a._maxY = temp;
            return a;
        }

        public static explicit operator Bounds(Rect source)
        {
            Bounds output;
            output._minX = source._minX;
            output._maxX = source._maxX;
            output._minY = source._minY;
            output._maxY = source._maxY;
            return output;
        }
        #endregion
        #region Public Methods
        public bool Incapsulates(Vector a)
        {
            return a.X >= _minX && a.Y >= _minY && a.X <= _maxX && a.Y <= _maxY;
        }
        public bool Incapsulates(Bounds a)
        {
            return a._minX >= _minX && a._minY >= _minY && a._maxX <= _maxX && a._maxY <= _maxY;
        }
        public bool Overlaps(Bounds a)
        {
            return a._minX <= _maxX && a._minY <= _maxY && a._maxX >= _minX && a._maxY >= _minY;
        }
        public Bounds Invert()
        {
            Bounds output = this;
            output._minX = -_maxX;
            output._maxX = -_minX;
            output._minY = -_maxY;
            output._maxY = -_minY;
            return output;
        }
        public Bounds InvertX()
        {
            Bounds output = this;
            output._minX = -_maxX;
            output._maxX = -_minX;
            return output;
        }
        public Bounds InvertY()
        {
            Bounds output = this;
            output._minY = -_maxY;
            output._maxY = -_minY;
            return output;
        }
        #endregion
        #region Public Static Methods
        public static Bounds Combine(Bounds a, Bounds b)
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
        public static Bounds Incapsulate(Bounds a, Vector b)
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