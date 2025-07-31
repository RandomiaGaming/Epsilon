using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.UI
{
    public sealed class ColorBox : Element
    {
        public Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.Red;
        public ColorBox(Canvas canvas) : base(canvas)
        {

        }
        public override void Render()
        {
            //canvas.RenderSprite()
        }
    }
}
