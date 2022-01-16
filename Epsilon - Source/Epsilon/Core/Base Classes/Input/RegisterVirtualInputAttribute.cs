using System;

namespace Epsilon
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class RegisterVirtualInputAttribute : Attribute
    {
        public string Name = "Unnamed Virtual Input";
        private RegisterVirtualInputAttribute()
        {
            Name = "Unnamed Virtual Input";
        }
        public RegisterVirtualInputAttribute(string Name)
        {
            this.Name = Name;
        }
    }
}
