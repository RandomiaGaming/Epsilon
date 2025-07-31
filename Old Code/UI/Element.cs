using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.UI
{
    public abstract class Element
    {
        public Canvas canvas = null;
        public double minX = 0;
        public double maxX = 1;
        public double minY = 0;
        public double maxY = 1;
        private Element()
        {
            canvas = null;
            minX = 0;
            maxX = 1;
            minY = 0;
            maxY = 1;
        }
        public Element(Canvas canvas)
        {
            if(canvas == null)
            {
                throw new Exception("Canvas cannot be null.");
            }
            this.canvas = canvas;
            minX = 0;
            maxX = 1;
            minY = 0;
            maxY = 1;
        }
        public abstract void Render();
    }
}
