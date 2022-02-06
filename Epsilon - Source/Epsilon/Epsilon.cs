using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpsilonEngine;

namespace Epsilon
{
    public sealed class Epsilon : Game
    {
        public Epsilon()
        {
            this.BackgroundColor = new Color(255, 150, 255, 255);
            new Stage(this);
        }
    }
}
