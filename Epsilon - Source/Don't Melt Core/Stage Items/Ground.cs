namespace DontMelt
{
    public sealed class Ground : StageItem
    {
        public Ground(StagePlayer stagePlayer) : base(stagePlayer)
        {
            collider = new Collider(this)
            {
                trigger = false,
                shape = new Rectangle(Point.Zero, new Point(16, 16)),
                sideCollision = SideInfo.True,
            };

            tag = StageItemTag.Ground;
            position = Point.Zero;
            texture = AssetHelper.LoadAsset<TextureAsset>("Ground.png").data;
        }
        public override void Update()
        {
            
        }
    }
}