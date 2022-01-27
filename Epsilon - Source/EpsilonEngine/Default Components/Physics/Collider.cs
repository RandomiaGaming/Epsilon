using Microsoft.Xna.Framework;
namespace Epsilon
{
    public sealed class Collider : Component
    {
        public Rectangle shape = new Rectangle(Point.Zero, new Point(16, 16));
        public SideInfo sideCollision = SideInfo.True;
        public bool trigger = false;
        public Collider(StageObject stageObject) : base(stageObject)
        {
            stageObject.Stage.loadedColliders.Add(this);
        }
        public Rectangle GetWorldShape()
        {
            return new Rectangle(shape.X + StageObject.Position.X, shape.Y + StageObject.Position.Y, shape.Width, shape.Height);
        }
    }
}