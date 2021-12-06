using System;
namespace Epsilon
{
    public sealed class Collider
    {
        public Rectangle shape = new Rectangle(Point.Zero, new Point(16, 16));
        public Point offset = Point.Zero;
        public SideInfo sideCollision = SideInfo.True;
        public bool trigger = false;

        public readonly StageObject stageItem = null;
        public Collider(StageObject stageItem)
        {
            if (stageItem is null)
            {
                throw new NullReferenceException();
            }
            this.stageItem = stageItem;
        }
        public Rectangle GetWorldShape()
        {
            Rectangle output = new Rectangle(shape.min + stageItem.position + offset, shape.max + stageItem.position + offset);
            return output;
        }
    }
}