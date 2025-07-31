using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.UI
{
    public sealed class Canvas
    {
        public Epsilon epsilon;
        public List<Element> children = new List<Element>();
        public Microsoft.Xna.Framework.Graphics.Texture2D defaultTexture;
        public Canvas(Epsilon epsilon)
        {
            this.epsilon = epsilon;
            defaultTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(epsilon.game.GraphicsDevice, 1, 1);
            defaultTexture.SetData(new Microsoft.Xna.Framework.Color[] { Microsoft.Xna.Framework.Color.White });
        }
        public void RenderSprite(Microsoft.Xna.Framework.Graphics.Texture2D texture, double minX, double maxX, double minY, double maxY)
        {
            //epsilon.game.
        }
    }
}