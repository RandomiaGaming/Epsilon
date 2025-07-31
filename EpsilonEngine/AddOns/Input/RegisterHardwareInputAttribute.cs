namespace EpsilonEngine
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class RegisterHardwareInputAttribute : System.Attribute
    {
        private string _name = "Unnamed Hardware Input";
        public string Name
        {
            get { return _name; }
        }
        private RegisterHardwareInputAttribute()
        {
            _name = "Unnamed Hardware Input";
        }
        public RegisterHardwareInputAttribute(string name)
        {
            if (name is null)
            {
                throw new System.Exception("name cannot be null.");
            }
            if (name == "")
            {
                throw new System.Exception("name cannot be empty.");
            }
            _name = name;
        }
        public override string ToString()
        {
            return $"Epsilon.RegisterHardwareInputAttribute({_name})";
        }
    }
}