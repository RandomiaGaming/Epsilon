//Approved 09/22/2022
//Note: Vectors can contain values such as NaN and PositiveInfinity so make sure to account for extrenuous cases in your code.
namespace EpsilonEngine
{
    public struct Vector
    {
        #region Public Constants
        public static readonly Vector Zero;
        public static readonly Vector One = new(1.0, 1.0);
        public static readonly Vector PositiveOne = One;
        public static readonly Vector NegativeOne = new(-1.0, -1.0);

        public static readonly Vector Up = new(0.0, 1.0);
        public static readonly Vector Down = new(0.0, -1.0);
        public static readonly Vector Right = new(1.0, 0.0);
        public static readonly Vector Left = new(-1.0, 0.0);

        public static readonly Vector UpRight = One;
        public static readonly Vector UpLeft = new(-1.0, 1.0);
        public static readonly Vector DownRight = new(1.0, -1.0);
        public static readonly Vector DownLeft = NegativeOne;

        public static readonly Vector Infinity = new(double.PositiveInfinity, double.PositiveInfinity);
        public static readonly Vector PositiveInfinity = Infinity;
        public static readonly Vector NegativeInfinity = new(double.NegativeInfinity, double.NegativeInfinity);
        public static readonly Vector NaN = new(double.NaN, double.NaN);
        public static readonly Vector MaxValue = new(double.MaxValue, double.MaxValue);
        public static readonly Vector MinValue = new(double.MinValue, double.MinValue);
        public static readonly Vector Epsilon = new(double.Epsilon, double.Epsilon);
        #endregion
        #region Public Varialbes
        public double X;
        public double Y;
        #endregion
        #region Public Constructors
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Vector(Point source)
        {
            X = source.X;
            Y = source.Y;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Vector({X}, {Y})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Vector))
            {
                return false;
            }
            Vector a = (Vector)obj;
            return X == a.X && Y == a.Y;
        }
        public override int GetHashCode()
        {
            return unchecked(X.GetHashCode() + Y.GetHashCode());
        }
        #endregion
        #region Public Operators
        public static bool operator ==(Vector a, Vector b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }
        public static Vector operator *(Vector a, Vector b)
        {
            a.X *= b.X;
            a.Y *= b.Y;
            return a;
        }
        public static Vector operator /(Vector a, Vector b)
        {
            a.X /= b.X;
            a.Y /= b.Y;
            return a;
        }

        public static Vector operator +(Vector a, double b)
        {
            a.X += b;
            a.Y += b;
            return a;
        }
        public static Vector operator -(Vector a, double b)
        {
            a.X -= b;
            a.Y -= b;
            return a;
        }
        public static Vector operator *(Vector a, double b)
        {
            a.X *= b;
            a.Y *= b;
            return a;
        }
        public static Vector operator /(Vector a, double b)
        {
            a.X /= b;
            a.Y /= b;
            return a;
        }

        public static Vector operator +(Vector a, Point b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }
        public static Vector operator -(Vector a, Point b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }
        public static Vector operator *(Vector a, Point b)
        {
            a.X *= b.X;
            a.Y *= b.Y;
            return a;
        }
        public static Vector operator /(Vector a, Point b)
        {
            a.X /= b.X;
            a.Y /= b.Y;
            return a;
        }

        public static Vector operator +(Vector a, int b)
        {
            a.X += b;
            a.Y += b;
            return a;
        }
        public static Vector operator -(Vector a, int b)
        {
            a.X -= b;
            a.Y -= b;
            return a;
        }
        public static Vector operator *(Vector a, int b)
        {
            a.X *= b;
            a.Y *= b;
            return a;
        }
        public static Vector operator /(Vector a, int b)
        {
            a.X /= b;
            a.Y /= b;
            return a;
        }

        public static Vector operator -(Vector a)
        {
            a.X = -a.X;
            a.Y = -a.Y;
            return a;
        }
        
        public static explicit operator Vector(Point source)
        {
            Vector output;
            output.X = source.X;
            output.Y = source.Y;
            return output;
        }
        #endregion
        #region Public Methods
        public bool IsFinite()
        {
            return !(X is double.NaN || Y is double.NaN || X is double.PositiveInfinity || Y is double.PositiveInfinity || X is double.NegativeInfinity || Y is double.NegativeInfinity);
        }
        public bool ContainsNaN()
        {
            return !(X is double.NaN || Y is double.NaN);
        }
        #endregion
    }
}