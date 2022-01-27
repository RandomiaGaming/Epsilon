using System;

namespace EpsilonEngine
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class DefaultInputBindingAttribute : Attribute
    {
        private string _hardwareInputName = null;
        private string _virtualInputName = null;
        public string HardwareInputName
        {
            get
            {
                return _hardwareInputName;
            }
        }
        public string VirtualInputName
        {
            get
            {
                return _virtualInputName;
            }
        }
        private DefaultInputBindingAttribute()
        {
            _virtualInputName = null;
            _hardwareInputName = null;
        }
        public DefaultInputBindingAttribute(string hardwareInputName, string virtualInputName)
        {
            if (virtualInputName is null)
            {
                throw new Exception("virtualInputName cannot be null.");
            }
            if (virtualInputName == "")
            {
                throw new Exception("virtualInputName cannot be empty.");
            }
            _virtualInputName = virtualInputName;

            if (hardwareInputName is null)
            {
                throw new Exception("hardwareInputName cannot be null.");
            }
            if (hardwareInputName == "")
            {
                throw new Exception("hardwareInputName cannot be empty.");
            }
            _hardwareInputName = hardwareInputName;
        }
        public override string ToString()
        {
            return $"Epsilon.DefaultInputBindingAttribute({_hardwareInputName}, {_virtualInputName})";
        }
    }
}
