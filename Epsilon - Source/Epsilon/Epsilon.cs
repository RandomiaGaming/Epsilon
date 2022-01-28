using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpsilonEngine;

namespace Epsilon
{
    public sealed class Epsilon : Engine
    {
        public Epsilon()
        {
            ChangeStage(new Stage(this));
        }
    }
}
