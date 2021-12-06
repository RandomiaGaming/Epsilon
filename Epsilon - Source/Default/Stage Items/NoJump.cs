using System;

using System.Collections.Generic;
namespace Epsilon
{
    public class NoJump : StageObject
    {
        public NoJump(StagePlayer stagePlayer) : base(stagePlayer)
        {
            collider = new Collider(this)
            {
                trigger = false,
                shape = new Rectangle(Point.Zero, new Point(16, 16)),
                sideCollision = SideInfo.True,
            };

            position = Point.Zero;
            texture = AssetHelper.LoadAsset<TextureAsset>("NoJump.png").data;
        }
        public override void Update()
        {

        }
    }
}