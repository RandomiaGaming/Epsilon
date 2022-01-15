using System.Collections.Generic;
using System;

namespace Epsilon
{
    public class Lava : StageObject
    {
        public Lava(StagePlayer stagePlayer) : base(stagePlayer)
        {
            collider = new Collider(this)
            {
                trigger = false,
                shape = new Rectangle(Point.One, new Point(15, 15)),
                sideCollision = SideInfo.True,
            };

            tag = StageObjectTag.Hazzard;
            position = Point.Zero;
            texture = ((TextureAsset)AssetHelper.LoadAsset("Lava.png")).data;
        }
        public override void Update()
        {

        }
    }
}