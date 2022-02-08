using System;
namespace EpsilonEngine
{
    public struct DirectionInfo
    {
        #region Constants
        public static readonly DirectionInfo True = new DirectionInfo(true, true, true, true);
        public static readonly DirectionInfo False = new DirectionInfo(false, false, false, false);

        public static readonly DirectionInfo UpConst = new DirectionInfo(true, false, false, false);
        public static readonly DirectionInfo DownConst = new DirectionInfo(false, true, false, false);
        public static readonly DirectionInfo LeftConst = new DirectionInfo(false, false, true, false);
        public static readonly DirectionInfo RightConst = new DirectionInfo(false, false, false, true);
        #endregion
        #region Properties
        public bool Up { get; private set; }
        public bool Down { get; private set; }
        public bool Left { get; private set; }
        public bool Right { get; private set; }
        #endregion
        #region Constructors
        public DirectionInfo(bool up, bool down, bool left, bool right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
        public DirectionInfo(Direction side)
        {
            if (side == Direction.Up)
            {
                Up = true;
                Down = false;
                Left = false;
                Right = false;
            }
            else if (side == Direction.Down)
            {
                Up = false;
                Down = true;
                Left = false;
                Right = false;
            }
            else if (side == Direction.Left)
            {
                Up = false;
                Down = false;
                Left = true;
                Right = false;
            }
            else if (side == Direction.Right)
            {
                Up = false;
                Down = false;
                Left = false;
                Right = true;
            }
            else
            {
                throw new Exception("direction must be a valid Direction.");
            }
        }
        public DirectionInfo(Point normal)
        {
            if (normal.Y > 0)
            {
                Up = true;
                Down = false;
            }
            else if (normal.Y < 0)
            {
                Up = false;
                Down = true;
            }
            else
            {
                Up = false;
                Down = false;
            }

            if (normal.X > 0)
            {
                Right = true;
                Left = false;
            }
            else if (normal.X < 0)
            {
                Right = false;
                Left = true;
            }
            else
            {
                Right = false;
                Left = false;
            }
        }
        public DirectionInfo(Vector normal)
        {
            if (normal.Y > 0)
            {
                Up = true;
                Down = false;
            }
            else if (normal.Y < 0)
            {
                Up = false;
                Down = true;
            }
            else
            {
                Up = false;
                Down = false;
            }

            if (normal.X > 0)
            {
                Right = true;
                Left = false;
            }
            else if (normal.X < 0)
            {
                Right = false;
                Left = true;
            }
            else
            {
                Right = false;
                Left = false;
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.SideInfo({Up}, {Down}, {Left}, {Right})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(DirectionInfo))
            {
                return false;
            }
            else
            {
                return this == (DirectionInfo)obj;
            }
        }
        public static bool operator ==(DirectionInfo a, DirectionInfo b)
        {
            return (a.Up == b.Up) && (a.Down == b.Down) && (a.Left == b.Left) && (a.Right == b.Right);
        }
        public static bool operator !=(DirectionInfo a, DirectionInfo b)
        {
            return !(a == b);
        }
        #endregion
        #region Methods
        public static DirectionInfo Invert(DirectionInfo source)
        {
            return new DirectionInfo(!source.Up, !source.Down, !source.Left, !source.Right);
        }
        public DirectionInfo Invert()
        {
            return Invert(this);
        }
        public static DirectionInfo Flip(DirectionInfo source)
        {
            return new DirectionInfo(source.Down, source.Up, source.Right, source.Left);
        }
        public DirectionInfo Flip()
        {
            return Flip(this);
        }
        #endregion
    }
}
