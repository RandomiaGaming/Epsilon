namespace EpsilonEngine
{
    public struct Point
    {
        public int X;
        public int Y;

        public static readonly Point Zero = new Point(0, 0);
        public static readonly Point One = new Point(1, 1);
        public static readonly Point NegativeOne = new Point(-1, -1);
        public static readonly Point Up = new Point(0, 1);
        public static readonly Point Down = new Point(0, -1);
        public static readonly Point Right = new Point(1, 0);
        public static readonly Point Left = new Point(-1, 0);
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Microsoft.Xna.Framework.Point ToXNA()
        {
            return new Microsoft.Xna.Framework.Point(X, Y);
        }
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
        public override int GetHashCode()
        {
            return base.GetHashCode();
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
            return new Point(a.X + b.X, a.Y + b.Y);
        }
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
        public static Point operator *(Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y);
        }
        public static Point operator /(Point a, Point b)
        {
            return new Point(a.X / b.X, a.Y / b.Y);
        }
        /*
        public static Vector2 operator +(Point a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Point a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Point a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }
        public static Vector2 operator /(Point a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public static Vector2 operator +(Point a, double b)
        {
            return new Vector2(a.x + b, a.y + b);
        }
        public static Vector2 operator -(Point a, double b)
        {
            return new Vector2(a.x - b, a.y - b);
        }
        public static Vector2 operator *(Point a, double b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator /(Point a, double b)
        {
            return new Vector2(a.x / b, a.y / b);
        }*/

        public static Point operator +(Point a, int b)
        {
            return new Point(a.X + b, a.Y + b);
        }
        public static Point operator -(Point a, int b)
        {
            return new Point(a.X - b, a.Y - b);
        }
        public static Point operator *(Point a, int b)
        {
            return new Point(a.X * b, a.Y * b);
        }
        public static Point operator /(Point a, int b)
        {
            return new Point(a.X / b, a.Y / b);
        }

        public static Point operator +(Point a)
        {
            return a;
        }
        public static Point operator -(Point a)
        {
            return new Point(a.X * -1, a.Y * -1);
        }

        /*
        public static explicit operator Point(Vector2 a)
        {
            return new Point((int)a.x, (int)a.y);
        }
        */
    }
}