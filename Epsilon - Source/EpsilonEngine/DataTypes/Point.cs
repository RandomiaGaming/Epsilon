namespace EpsilonEngine
{
    public struct Point
    {
        #region Constants
        public static readonly Point Zero = new Point(0, 0);
        public static readonly Point One = new Point(1, 1);
        public static readonly Point NegativeOne = new Point(-1, -1);

        public static readonly Point Up = new Point(0, 1);
        public static readonly Point Down = new Point(0, -1);
        public static readonly Point Right = new Point(1, 0);
        public static readonly Point Left = new Point(-1, 0);

        public static readonly Point UpRight = new Point(1, 1);
        public static readonly Point UpLeft = new Point(-1, 1);
        public static readonly Point DownRight = new Point(1, -1);
        public static readonly Point DownLeft = new Point(-1, -1);
        #endregion
        #region Variables
        private int _x;
        private int _y;
        #endregion
        #region Properties
        public int X
        {
            get
            {
                return _x;
            }
        }
        public int Y
        {
            get
            {
                return _y;
            }
        }
        #endregion
        #region Constructors
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public Point(Microsoft.Xna.Framework.Point source)
        {
            _x = source.X;
            _y = source.Y;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Point({_x}, {_y})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Point))
            {
                return false;
            }
            else
            {
                return this == (Point)obj;
            }
        }
        public static bool operator ==(Point a, Point b)
        {
            return (a._x == b._x) && (a._y == b._y);
        }
        public static bool operator !=(Point a, Point b)
        {
            return (a._x != b._x) || (a._y != b._y);
        }
        public static Point operator +(Point a, Point b)
        {
            return new Point(a._x + b._x, a._y + b._y);
        }
        public static Point operator -(Point a, Point b)
        {
            return new Point(a._x - b._x, a._y - b._y);
        }
        public static Point operator *(Point a, Point b)
        {
            return new Point(a._x * b._x, a._y * b._y);
        }
        public static Point operator /(Point a, Point b)
        {
            return new Point(a._x / b._x, a._y / b._y);
        }
        public static Point operator +(Point a, int b)
        {
            return new Point(a._x + b, a._y + b);
        }
        public static Point operator -(Point a, int b)
        {
            return new Point(a._x - b, a._y - b);
        }
        public static Point operator *(Point a, int b)
        {
            return new Point(a._x * b, a._y * b);
        }
        public static Point operator /(Point a, int b)
        {
            return new Point(a._x / b, a._y / b);
        }
        public static Point operator +(Point a)
        {
            return a;
        }
        public static Point operator -(Point a)
        {
            return new Point(a._x * -1, a._y * -1);
        }
        #endregion
        #region Methods
        public static Microsoft.Xna.Framework.Point ToXNA(Point source)
        {
            return new Microsoft.Xna.Framework.Point(source._x, source._y);
        }
        public Microsoft.Xna.Framework.Point ToXNA()
        {
            return Point.ToXNA(this);
        }
        public Point FromXNA(Microsoft.Xna.Framework.Point source)
        {
            return new Point(source);
        }
        #endregion
    }
}