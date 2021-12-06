namespace Epsilon
{
    public sealed class Ground : StageObject
    {
        public Ground(StagePlayer stagePlayer) : base(stagePlayer)
        {
            collider = new Collider(this)
            {
                trigger = false,
                shape = new Rectangle(Point.Zero, new Point(8, 8)),
                sideCollision = SideInfo.True,
            };

            tag = StageObjectTag.Ground;
            position = Point.Zero;
            texture = AssetHelper.LoadAsset<TextureAsset>("Ground.png").data;
        }
        public override void Update()
        {
            
        }
    }
}