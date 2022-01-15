namespace EpsilonEngine
{
    public struct SideInfo
    {
        public readonly bool top;
        public readonly bool bottom;
        public readonly bool right;
        public readonly bool left;

        public static readonly SideInfo False = new SideInfo(false, false, false, false);
        public static readonly SideInfo True = new SideInfo(true, true, true, true);

        public static readonly SideInfo Top = new SideInfo(true, false, false, false);
        public static readonly SideInfo Bottom = new SideInfo(false, true, false, false);
        public static readonly SideInfo Right = new SideInfo(false, false, true, false);
        public static readonly SideInfo Left = new SideInfo(false, false, false, true);

        public SideInfo(bool top, bool bottom, bool right, bool left)
        {
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
        }
        public SideInfo(Side side)
        {
            switch (side)
            {
                case Side.Top:
                    top = true;
                    bottom = false;
                    left = false;
                    right = false;
                    break;
                case Side.Bottom:
                    top = false;
                    bottom = true;
                    left = false;
                    right = false;
                    break;
                case Side.Left:
                    top = false;
                    bottom = false;
                    left = true;
                    right = false;
                    break;
                default:
                    top = false;
                    bottom = false;
                    left = false;
                    right = true;
                    break;
            }
        }
        public SideInfo(Vector normal)
        {
            if(normal.x > 0)
            {
                right = true;
                left = false;
            }
            else if (normal.x < 0)
            {
                right = false;
                left = true;
            }
            else
            {
                right = false;
                left = false;
            }
            if (normal.y > 0)
            {
                top = true;
                bottom = false;
            }
            else if (normal.y < 0)
            {
                top = false;
                bottom = true;
            }
            else
            {
                top = false;
                bottom = false;
            }
        }
        public SideInfo(Point normal)
        {
            if (normal.x > 0)
            {
                right = true;
                left = false;
            }
            else if (normal.x < 0)
            {
                right = false;
                left = true;
            }
            else
            {
                right = false;
                left = false;
            }
            if (normal.y > 0)
            {
                top = true;
                bottom = false;
            }
            else if (normal.y < 0)
            {
                top = false;
                bottom = true;
            }
            else
            {
                top = false;
                bottom = false;
            }
        }
    }
}
