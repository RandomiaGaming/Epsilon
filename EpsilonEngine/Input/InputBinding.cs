using System;
namespace EpsilonEngine
{
    public sealed class InputBinding
    {
        private InputManager _inputManager = null;
        private HardwareInput _hardwareInput = null;
        private VirtualInput _virtualInput = null;
        public InputManager InputManager
        {
            get
            {
                return _inputManager;
            }
        }
        public HardwareInput HardwareInput
        {
            get
            {
                return _hardwareInput;
            }
        }
        public VirtualInput VirtualInput
        {
            get
            {
                return _virtualInput;
            }
        }
        private InputBinding()
        {
            _inputManager = null;
            _virtualInput = null;
            _hardwareInput = null;
        }
        public InputBinding(InputManager inputManager, HardwareInput hardwareInput, VirtualInput virtualInput)
        {
            if (inputManager is null)
            {
                throw new Exception("inputManager cannot be null.");
            }
            _inputManager = inputManager;

            if (virtualInput is null)
            {
                throw new Exception("virtualInput cannot be null.");
            }
            _virtualInput = virtualInput;

            if (hardwareInput is null)
            {
                throw new Exception("hardwareInput cannot be null.");
            }
            _hardwareInput = hardwareInput;
        }
    }
}
