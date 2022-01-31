using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonEngine
{
    public sealed class PhysicsLayer
    {
        public int Index = 0;
        public List<Collider> Colliders = new List<Collider>();
    }
}
