namespace EpsilonEngine
{
    public sealed class VirtualInput
    {
        private InputManager _inputManager = null;
        private string _name = "Unnamed Hardware Input";
        private bool _pressed = false;
        public InputManager InputManager
        {
            get { return _inputManager; }
        }
        public string Name
        {
            get { return _name; }
        }
        public bool Pressed
        {
            get { return _pressed; }
        }
        private VirtualInput()
        {
            _inputManager = null;
            _name = "Unnamed Virtual Input";
        }
        public VirtualInput(InputManager inputManager, string name)
        {
            if (inputManager is null)
            {
                throw new System.Exception("inputManager cannot be null.");
            }
            _inputManager = inputManager;
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
        public void SetPressed(bool newValue)
        {
            _pressed = newValue;
        }
        public override string ToString()
        {
            return $"Epsilon.VirtualInput({_name})";
        }
    }
}