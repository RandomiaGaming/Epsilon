using System;
namespace EpsilonEngine
{
    public struct Bounds
    {
        #region Constants
        public static readonly Bounds Zero = new Bounds(0, 0, 0, 0);
        public static readonly Bounds One = new Bounds(0, 0, 1, 1);
        public static readonly Bounds NegativeOne = new Bounds(-1, -1, 0, 0);

        public static readonly Bounds UpRight = new Bounds(0, 0, 1, 1);
        public static readonly Bounds UpLeft = new Bounds(-1, 0, 0, 1);
        public static readonly Bounds DownRight = new Bounds(0, -1, 1, 0);
        public static readonly Bounds DownLeft = new Bounds(-1, -1, 0, 0);
        #endregion
        #region Properties
        public float MinX { get; private set; }
        public float MinY { get; private set; }
        public float MaxX { get; private set; }
        public float MaxY { get; private set; }
        public Vector Min
        {
            get
            {
                return new Vector(MinX, MinY);
            }
        }
        public Vector Max
        {
            get
            {
                return new Vector(MaxX, MaxY);
            }
        }
        public float Width
        {
            get
            {
                return MaxX - MinX + 1;
            }
        }
        public float Height
        {
            get
            {
                return MaxY - MinY + 1;
            }
        }
        public Vector Size
        {
            get
            {
                return new Vector(MaxX - MinX + 1f, MaxY - MinY + 1f);
            }
        }
        #endregion
        #region Constructors
        public Bounds(float minX, float minY, float maxX, float maxY)
        {
            if (minX > maxX)
            {
                throw new Exception("MaxX must be greater than MinX.");
            }
            MinX = minX;
            MaxX = maxX;
            if (minY > maxY)
            {
                throw new Exception("MaxY must be greater than MinY.");
            }
            MinY = minY;
            MaxY = maxY;
        }
        public Bounds(Vector min, Vector max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new Exception("Max must be greater than Min.");
            }
            MinX = min.X;
            MinY = min.Y;
            MaxX = max.X;
            MaxY = max.Y;
        }
        public Bounds(Microsoft.Xna.Framework.BoundingBox source)
        {
            MinX = source.Min.X;
            MinY = source.Min.Y;
            MaxX = source.Max.X;
            MaxY = source.Max.Y;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Bounds({MinX}, {MinY}, {MaxX}, {MaxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Bounds))
            {
                return false;
            }
            else
            {
                return this == (Bounds)obj;
            }
        }
        public static bool operator ==(Bounds a, Bounds b)
        {
            return (a.MinX == b.MinX) && (a.MinY == b.MinY) && (a.MaxX == b.MaxX) && (a.MaxY == b.MaxY);
        }
        public static bool operator !=(Bounds a, Bounds b)
        {
            return !(a == b);
        }
        #endregion
        #region Methods
        public static bool Incapsulates(Bounds a, Vector b)
        {
            return b.X >= a.MinX && b.X <= a.MaxX && b.Y >= a.MinY && b.Y <= a.MaxY;
        }
        public bool Incapsulates(Vector a)
        {
            return Incapsulates(this, a);
        }
        public static bool Incapsulates(Bounds a, Bounds b)
        {
            return b.MaxY <= a.MaxY && b.MinY >= a.MinY && b.MaxX <= a.MaxX && b.MinX >= a.MinX;
        }
        public bool Incapsulates(Bounds a)
        {
            return Incapsulates(this, a);
        }
        public static bool Overlaps(Bounds a, Bounds b)
        {
            return a.MaxX >= b.MinX && a.MinX <= b.MaxX && a.MaxY >= b.MinY && a.MinY <= b.MaxY;
        }
        public bool Overlaps(Bounds a)
        {
            return Overlaps(this, a);
        }
        public static Microsoft.Xna.Framework.BoundingBox ToXNA(Bounds source)
        {
            return new Microsoft.Xna.Framework.BoundingBox(new Microsoft.Xna.Framework.Vector3(source.MinX, source.MinY, 0f), new Microsoft.Xna.Framework.Vector3(source.MaxX, source.MaxY, 0f));
        }
        public Microsoft.Xna.Framework.BoundingBox ToXNA()
        {
            return ToXNA(this);
        }
        public static Bounds FromXNA(Microsoft.Xna.Framework.BoundingBox source)
        {
            return new Bounds(source);
        }
        #endregion
    }
}