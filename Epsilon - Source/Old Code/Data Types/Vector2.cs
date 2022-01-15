namespace Epsilon
{
    public struct Vector2
    {
        public double x;
        public double y;
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return $"({x}, {y})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Vector2))
            {
                return false;
            }
            else
            {
                return this == (Vector2)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public static Vector2 operator +(Vector2 a, Point b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Point b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, Point b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }
        public static Vector2 operator /(Vector2 a, Point b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public static Vector2 operator +(Vector2 a, double b)
        {
            return new Vector2(a.x + b, a.y + b);
        }
        public static Vector2 operator -(Vector2 a, double b)
        {
            return new Vector2(a.x - b, a.y - b);
        }
        public static Vector2 operator *(Vector2 a, double b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator /(Vector2 a, double b)
        {
            return new Vector2(a.x / b, a.y / b);
        }

        public static Vector2 operator +(Vector2 a, int b)
        {
            return new Vector2(a.x + b, a.y + b);
        }
        public static Vector2 operator -(Vector2 a, int b)
        {
            return new Vector2(a.x - b, a.y - b);
        }
        public static Vector2 operator *(Vector2 a, int b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator /(Vector2 a, int b)
        {
            return new Vector2(a.x / b, a.y / b);
        }

        public static Vector2 operator +(Vector2 a)
        {
            return a;
        }
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(a.x * -1, a.y * -1);
        }

        public static explicit operator Vector2(Point a)
        {
            return new Vector2(a.x, a.y);
        }
    }
}