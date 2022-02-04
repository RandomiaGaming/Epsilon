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
            ChangeScene(new Stage(this));
        }
    }
}
