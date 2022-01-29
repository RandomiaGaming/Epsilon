using System;
namespace EpsilonEngine
{
    public class Rectangle
    {
        public readonly Point min = Point.Zero;
        public readonly Point max = Point.One;
        public Rectangle(Point min, Point max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new ArgumentException();
            }
            this.min = min;
            this.max = max;
        }
        public Rectangle(int minX, int minY, int maxX, int maxY)
        {
            /*if (maxX < minX || maxY < minY)
            {
                throw new ArgumentException();
            }*/
            min = new Point(minX, minY);
            max = new Point(maxX, maxY);
        }
        public override string ToString()
        {
            return $"[{min}, {max}]";
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
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return (a.min == b.min && a.max == b.max);
        }
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !(a == b);
        }
        public bool Incapsulates(Point a)
        {
            if (a.X >= min.X && a.X <= max.X && a.Y >= min.Y && a.Y <= max.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Incapsulates(Rectangle a)
        {
            if (a.max.Y <= max.Y && a.min.Y >= min.Y && a.max.X <= max.X && a.min.X >= min.X)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Overlaps(Rectangle a)
        {
            if (max.X < a.min.X || min.X > a.max.X || max.Y < a.min.Y || min.Y > a.max.Y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Microsoft.Xna.Framework.Rectangle ToXNA()
        {
            return new Microsoft.Xna.Framework.Rectangle(min.X, max.Y, max.X - min.X, max.Y - min.Y);
        }
    }
}