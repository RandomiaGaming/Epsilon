using System;
namespace Epsilon
{
    public class Rectangle
    {
        public readonly Point min = Point.Zero;
        public readonly Point max = Point.One;
        public Rectangle(Point min, Point max)
        {
            if (min.x > max.x || min.y > max.y)
            {
                throw new ArgumentException();
            }
            this.min = min;
            this.max = max;
        }
        public Rectangle(int minX, int minY, int maxX, int maxY)
        {
            if (maxX < minX || maxY < minY)
            {
                throw new ArgumentException();
            }
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
            if (a.x >= min.x && a.x <= max.x && a.y >= min.y && a.y <= max.y)
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
            if (a.max.y <= max.y && a.min.y >= min.y && a.max.x <= max.x && a.min.x >= min.x)
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
            if (max.x < a.min.x || min.x > a.max.x || max.y < a.min.y || min.y > a.max.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}