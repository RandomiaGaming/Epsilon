namespace EpsilonEngine.Physics
{
    public sealed class Collider : Component
    {
        public RectangleInt shape = new RectangleInt(Vector2Int.Zero, new Vector2Int(16, 16));
        public SideInfo sideCollision = SideInfo.True;
        public bool trigger = false;
        public Collider(GameObject gameObject) : base(gameObject)
        {

        }
        public RectangleInt GetShapeInWorldShape()
        {
            return new RectangleInt(shape.min + gameObject.position, shape.max + gameObject.position);
        }
    }
}