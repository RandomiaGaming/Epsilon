using System;

namespace EpsilonEngine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class RegisterHardwareInputAttribute : Attribute
    {
        private string _name = "Unnamed Hardware Input";
        public string Name
        {
            get
            {
                return _name;
            }
        }
        private RegisterHardwareInputAttribute()
        {
            _name = "Unnamed Hardware Input";
        }
        public RegisterHardwareInputAttribute(string name)
        {
            if (name is null)
            {
                throw new Exception("name cannot be null.");
            }
            if(name == "")
            {
                throw new Exception("name cannot be empty.");
            }
            _name = name;
        }
        public override string ToString()
        {
            return $"Epsilon.RegisterHardwareInputAttribute({_name})";
        }
    }
}
