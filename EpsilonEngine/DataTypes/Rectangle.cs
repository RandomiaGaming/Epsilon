using System;
namespace EpsilonEngine
{
    public struct Rectangle
    {
        #region Constants
        public static readonly Rectangle Zero = new Rectangle(0, 0, 0, 0);
        public static readonly Rectangle One = new Rectangle(0, 0, 1, 1);
        public static readonly Rectangle NegativeOne = new Rectangle(-1, -1, 0, 0);

        public static readonly Rectangle UpRight = new Rectangle(0, 0, 1, 1);
        public static readonly Rectangle UpLeft = new Rectangle(-1, 0, 0, 1);
        public static readonly Rectangle DownRight = new Rectangle(0, -1, 1, 0);
        public static readonly Rectangle DownLeft = new Rectangle(-1, -1, 0, 0);
        #endregion
        #region Properties
        public int MinX { get; private set; }
        public int MinY { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public Point Min
        {
            get
            {
                return new Point(MinX, MinY);
            }
        }
        public Point Max
        {
            get
            {
                return new Point(MaxX, MaxY);
            }
        }
        public int Width
        {
            get
            {
                return MaxX - MinX + 1;
            }
        }
        public int Height
        {
            get
            {
                return MaxY - MinY + 1;
            }
        }
        public Point Size
        {
            get
            {
                return new Point(Width, Height);
            }
        }
        #endregion
        #region Constructors
        public Rectangle(int minX, int minY, int maxX, int maxY)
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
        public Rectangle(Point min, Point max)
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
        public Rectangle(Microsoft.Xna.Framework.Rectangle source)
        {
            MinX = source.Left;
            MinY = source.Top;
            MaxX = source.Right;
            MaxY = source.Bottom;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Rectangle({MinX}, {MinY}, {MaxX}, {MaxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Rectangle))
            {
                return false;
            }
            else
            {
                return this == (Rectangle)obj;
            }
        }
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return (a.MinX == b.MinX) && (a.MinY == b.MinY) && (a.MaxX == b.MaxX) && (a.MaxY == b.MaxY);
        }
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !(a == b);
        }
        #endregion
        #region Methods
        public static bool Incapsulates(Rectangle a, Point b)
        {
            return b.X >= a.MinX && b.X <= a.MaxX && b.Y >= a.MinY && b.Y <= a.MaxY;
        }
        public bool Incapsulates(Point a)
        {
            return Incapsulates(this, a);
        }
        public static bool Incapsulates(Rectangle a, Rectangle b)
        {
            return b.MaxY <= a.MaxY && b.MinY >= a.MinY && b.MaxX <= a.MaxX && b.MinX >= a.MinX;
        }
        public bool Incapsulates(Rectangle a)
        {
            return Incapsulates(this, a);
        }
        public static bool Overlaps(Rectangle a, Rectangle b)
        {
            return a.MaxX >= b.MinX && a.MinX <= b.MaxX && a.MaxY >= b.MinY && a.MinY <= b.MaxY;
        }
        public bool Overlaps(Rectangle a)
        {
            return Overlaps(this, a);
        }
        public static Microsoft.Xna.Framework.Rectangle ToXNA(Rectangle source)
        {
            return new Microsoft.Xna.Framework.Rectangle(source.MinX, source.MaxY, source.Width, source.Height);
        }
        public Microsoft.Xna.Framework.Rectangle ToXNA()
        {
            return ToXNA(this);
        }
        public static Rectangle FromXNA(Microsoft.Xna.Framework.Rectangle source)
        {
            return new Rectangle(source);
        }
        #endregion
    }
}