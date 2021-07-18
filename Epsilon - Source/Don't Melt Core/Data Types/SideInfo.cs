namespace DontMelt
{
    public enum Side { Top, Bottom, Left, Right };
    public struct SideInfo
    {
        public readonly bool top;
        public readonly bool bottom;
        public readonly bool left;
        public readonly bool right;
        public static readonly SideInfo False = new SideInfo(false, false, false, false);
        public static readonly SideInfo True = new SideInfo(true, true, true, true);
        public SideInfo(bool top, bool bottom, bool left, bool right)
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
    }
}
