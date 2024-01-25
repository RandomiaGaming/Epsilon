using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonCore
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PhysicalInputAttribute : Attribute
    {
        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
        }
        public PhysicalInputAttribute(string inputName)
        {
            if()
        }
    }
}
