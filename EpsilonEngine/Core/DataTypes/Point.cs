//Approved 09/22/2022
namespace EpsilonEngine
{
    public struct Point
    {
        #region Public Constants
        public static readonly Point Zero;
        public static readonly Point One = new(1, 1);
        public static readonly Point PositiveOne = One;
        public static readonly Point NegativeOne = new(-1, -1);

        public static readonly Point Up = new(0, 1);
        public static readonly Point Down = new(0, -1);
        public static readonly Point Right = new(1, 0);
        public static readonly Point Left = new(-1, 0);

        public static readonly Point UpRight = One;
        public static readonly Point UpLeft = new(-1, 1);
        public static readonly Point DownRight = new(1, -1);
        public static readonly Point DownLeft = NegativeOne;

        public static readonly Point MaxValue = new(int.MaxValue, int.MaxValue);
        public static readonly Point MinValue = new(int.MinValue, int.MinValue);
        #endregion
        #region Public Varialbes
        public int X;
        public int Y;
        #endregion
        #region Public Constructors
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
        #endregion
        #region Public Overrides
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
                Point a = (Point)obj;
                return X == a.X && Y == a.Y;
            }
        }
        public override int GetHashCode()
        {
            return unchecked(X + Y);
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X || a.Y != b.Y;
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

        public static Point operator -(Point a)
        {
            a.X = -a.X;
            a.Y = -a.Y;
            return a;
        }

        public static explicit operator Point(Vector source)
        {
            Point output;
            output.X = (int)source.X;
            output.Y = (int)source.Y;
            return output;
        }
        #endregion
    }
}