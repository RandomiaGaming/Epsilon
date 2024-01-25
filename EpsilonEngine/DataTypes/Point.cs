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
        #region Properties
        public int X { get; set; }
        public int Y { get; set; }
        #endregion
        #region Constructors
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point(Vector source)
        {
            X = (int)source.X;
            Y = (int)source.Y;
        }
        public Point(Microsoft.Xna.Framework.Point source)
        {
            X = source.X;
            Y = source.Y;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Point({X}, {Y})";
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
            return (a.X == b.X) && (a.Y == b.Y);
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
        public static Point operator +(Point a, Point b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }
        public static Point operator -(Point a, Point b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }
        public static Point operator *(Point a, Point b)
        {
            a.X *= b.X;
            a.Y *= b.Y;
            return a;
        }
        public static Point operator /(Point a, Point b)
        {
            a.X /= b.X;
            a.Y /= b.Y;
            return a;
        }
        public static Point operator +(Point a, int b)
        {
            a.X += b;
            a.Y += b;
            return a;
        }
        public static Point operator -(Point a, int b)
        {
            a.X -= b;
            a.Y -= b;
            return a;
        }
        public static Point operator *(Point a, int b)
        {
            a.X *= b;
            a.Y *= b;
            return a;
        }
        public static Point operator /(Point a, int b)
        {
            a.X /= b;
            a.Y /= b;
            return a;
        }
        public static Point operator +(Point a)
        {
            return a;
        }
        public static Point operator -(Point a)
        {
            a.X *= -1;
            a.Y *= -1;
            return a;
        }
        public static explicit operator Point(Vector source)
        {
            return new Point(source);
        }
        #endregion
        #region Methods
        public static Microsoft.Xna.Framework.Point ToXNA(Point source)
        {
            return new Microsoft.Xna.Framework.Point(source.X, source.Y);
        }
        public Microsoft.Xna.Framework.Point ToXNA()
        {
            return ToXNA(this);
        }
        public Point FromXNA(Microsoft.Xna.Framework.Point source)
        {
            return new Point(source);
        }
        #endregion
    }
}