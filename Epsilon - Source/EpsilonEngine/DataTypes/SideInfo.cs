using Microsoft.Xna.Framework;
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
        #region Variables
        private bool _right;
        private bool _top;
        private bool _left;
        private bool _bottom;
        #endregion
        #region Properties
        public bool Right
        {
            get
            {
                return _right;
            }
        }
        public bool Top
        {
            get
            {
                return _top;
            }
        }
        public bool Left
        {
            get
            {
                return _left;
            }
        }
        public bool Bottom
        {
            get
            {
                return _bottom;
            }
        }
        #endregion
        #region Constructors
        public SideInfo(bool right, bool top, bool left, bool bottom)
        {
            _right = right;
            _top = top;
            _left = left;
            _bottom = bottom;
        }
        public SideInfo(Side side)
        {
            switch (side)
            {
                case Side.Right:
                    _right = true;
                    _top = false;
                    _left = false;
                    _bottom = false;
                    break;
                case Side.Top:
                    _right = false;
                    _top = true;
                    _left = false;
                    _bottom = false;
                    break;
                case Side.Left:
                    _right = false;
                    _top = false;
                    _left = true;
                    _bottom = false;
                    break;
                default:
                    _right = false;
                    _top = false;
                    _left = false;
                    _bottom = true;
                    break;
            }
        }
        public SideInfo(Point normal)
        {
            if (normal.X > 0)
            {
                _right = true;
                _left = false;
            }
            else if (normal.X < 0)
            {
                _right = false;
                _left = true;
            }
            else
            {
                _right = false;
                _left = false;
            }

            if (normal.Y > 0)
            {
                _top = true;
                _bottom = false;
            }
            else if (normal.Y < 0)
            {
                _top = false;
                _bottom = true;
            }
            else
            {
                _top = false;
                _bottom = false;
            }
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.SideInfo({_right}, {_top}, {_left}, {_bottom})";
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
            return (a._right == b._right) && (a._top == b._top) && (a._left == b._left) && (a._bottom == b._bottom);
        }
        public static bool operator !=(SideInfo a, SideInfo b)
        {
            return (a._right != b._right) || (a._top != b._top) || (a._left != b._left) || (a._bottom != b._bottom);
        }
        #endregion
        #region Methods
        public static SideInfo Invert(SideInfo source)
        {
            return new SideInfo(!source._right, !source._top, !source._left, !source._bottom);
        }
        public SideInfo Invert()
        {
            return Invert(this);
        }
        #endregion
    }
}
