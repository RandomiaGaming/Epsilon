namespace EpsilonEngine
{
    public struct Vector
    {
        #region Constants
        public static readonly Vector Zero = new Vector(0, 0);
        public static readonly Vector One = new Vector(1, 1);
        public static readonly Vector NegativeOne = new Vector(-1, -1);

        public static readonly Vector Up = new Vector(0, 1);
        public static readonly Vector Down = new Vector(0, -1);
        public static readonly Vector Right = new Vector(1, 0);
        public static readonly Vector Left = new Vector(-1, 0);

        public static readonly Vector UpRight = new Vector(1, 1);
        public static readonly Vector UpLeft = new Vector(-1, 1);
        public static readonly Vector DownRight = new Vector(1, -1);
        public static readonly Vector DownLeft = new Vector(-1, -1);
        #endregion
        #region Properties
        public float X { get; private set; }
        public float Y { get; private set; }
        #endregion
        #region Constructors
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector(Point source)
        {
            X = source.X;
            Y = source.Y;
        }
        public Vector(Microsoft.Xna.Framework.Vector2 source)
        {
            X = source.X;
            Y = source.Y;
        }
        #endregion
        #region Overrides
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
            else
            {
                return this == (Vector)obj;
            }
        }
        public static bool operator ==(Vector a, Vector b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return (a.X != b.X) || (a.Y != b.Y);
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
        public static Vector operator +(Vector a)
        {
            return a;
        }
        public static Vector operator -(Vector a)
        {
            a.X *= -1;
            a.Y *= -1;
            return a;
        }
        public static explicit operator Vector(Point source)
        {
            return new Vector(source);
        }
        #endregion
        #region Methods
        public static Microsoft.Xna.Framework.Vector2 ToXNA(Vector source)
        {
            return new Microsoft.Xna.Framework.Vector2(source.X, source.Y);
        }
        public Microsoft.Xna.Framework.Vector2 ToXNA()
        {
            return ToXNA(this);
        }
        public Vector FromXNA(Microsoft.Xna.Framework.Vector2 source)
        {
            return new Vector(source);
        }
        #endregion
    }
}