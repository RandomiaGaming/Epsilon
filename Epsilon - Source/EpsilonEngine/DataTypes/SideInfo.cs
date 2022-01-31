using System;
namespace EpsilonEngine
{
    public struct SideInfo
    {
        #region Constants
        public static readonly SideInfo True = new SideInfo(true, true, true, true);
        public static readonly SideInfo False = new SideInfo(false, false, false, false);

        public static readonly SideInfo RightConst = new SideInfo(false, false, true, false);
        public static readonly SideInfo TopConst = new SideInfo(true, false, false, false);
        public static readonly SideInfo LeftConst = new SideInfo(false, false, false, true);
        public static readonly SideInfo BottomConst = new SideInfo(false, true, false, false);
        #endregion
        #region Properties
        public bool Right;
        public bool Top;
        public bool Left;
        public bool Bottom;
        #endregion
        #region Constructors
        public SideInfo(bool right, bool top, bool left, bool bottom)
        {
            Right = right;
            Top = top;
            Left = left;
            Bottom = bottom;
        }
        public SideInfo(Side side)
        {
            if (side == Side.Right)
            {
                Right = true;
                Top = false;
                Left = false;
                Bottom = false;
            }
            else if (side == Side.Top)
            {
                Right = false;
                Top = true;
                Left = false;
                Bottom = false;
            }
            else if (side == Side.Left)
            {
                Right = false;
                Top = false;
                Left = true;
                Bottom = false;
            }
            else if (side == Side.Bottom)
            {
                Right = false;
                Top = false;
                Left = false;
                Bottom = true;
            }
            else
            {
                throw new Exception("side must be a valid Side.");
            }
        }
        public SideInfo(Point normal)
        {
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

            if (normal.Y > 0)
            {
                Top = true;
                Bottom = false;
            }
            else if (normal.Y < 0)
            {
                Top = false;
                Bottom = true;
            }
            else
            {
                Top = false;
                Bottom = false;
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.SideInfo({Right}, {Top}, {Left}, {Bottom})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(SideInfo))
            {
                return false;
            }
            else
            {
                return this == (SideInfo)obj;
            }
        }
        public static bool operator ==(SideInfo a, SideInfo b)
        {
            return (a.Right == b.Right) && (a.Top == b.Top) && (a.Left == b.Left) && (a.Bottom == b.Bottom);
        }
        public static bool operator !=(SideInfo a, SideInfo b)
        {
            return (a.Right != b.Right) || (a.Top != b.Top) || (a.Left != b.Left) || (a.Bottom != b.Bottom);
        }
        #endregion
        #region Methods
        public static SideInfo Invert(SideInfo source)
        {
            return new SideInfo(!source.Right, !source.Top, !source.Left, !source.Bottom);
        }
        public SideInfo Invert()
        {
            return Invert(this);
        }
        #endregion
    }
}
