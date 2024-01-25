using System;
using System.Collections.Generic;
using System.Reflection;
namespace EpsilonEngine
{
    //public enum VirtualInput { Jump, Right, Left, MenuUp, MenuDown, MenuRight, MenuLeft, MenuEnter, MenuBack };
    public sealed class InputManager
    {
        private Game _epsilon = null;
        private List<HardwareInput> _hardwareInputs = new List<HardwareInput>();
        private List<VirtualInput> _virtualInputs = new List<VirtualInput>();
        private List<InputBinding> _inputBindings = new List<InputBinding>();
        public Game Epsilon
        {
            get
            {
                return _epsilon;
            }
        }
        public InputManager(Game epsilon)
        {
            if (epsilon is null)
            {
                throw new Exception("epsilon cannot be null.");
            }
            _epsilon = epsilon;

            ReloadInputs();
        }
        public override string ToString()
        {
            return $"Epsilon.InputManager()";
        }
        public void Update()
        {
            foreach (HardwareInput hardwareInput in _hardwareInputs)
            {
                hardwareInput.Update();
            }
            foreach (VirtualInput virtualInput in _virtualInputs)
            {
                virtualInput.SetPressed(false);
            }
            foreach (InputBinding inputBinding in _inputBindings)
            {
                if (inputBinding.HardwareInput.Pressed)
                {
                    inputBinding.VirtualInput.SetPressed(true);
                }
            }
        }
        public void ReloadInputs()
        {
            _hardwareInputs = new List<HardwareInput>();
            _virtualInputs = new List<VirtualInput>();
            _inputBindings = new List<InputBinding>();

            Assembly assembly = Assembly.GetCallingAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo methodInfo in type.GetMethods())
                {
                    if (HardwareInput.MethodIsHardwareInput(methodInfo))
                    {
                        AddHardwareInput(new HardwareInput(this, methodInfo));
                    }
                }
            }

            foreach (RegisterVirtualInputAttribute registerVirtualInputAttribute in assembly.GetCustomAttributes<RegisterVirtualInputAttribute>())
            {
                AddVirtualInput(new VirtualInput(this, registerVirtualInputAttribute.Name));
            }

            foreach (DefaultInputBindingAttribute defaultInputBindingAttribute in assembly.GetCustomAttributes<DefaultInputBindingAttribute>())
            {
                AddInputBinding(CreateBindingFromNames(defaultInputBindingAttribute.HardwareInputName, defaultInputBindingAttribute.VirtualInputName));
            }
        }
        public InputBinding CreateBindingFromNames(string hardwareInputName, string virtualInputName)
        {
            if (hardwareInputName is null)
            {
                throw new Exception("hardwareInputName cannot be null.");
            }

            if (virtualInputName is null)
            {
                throw new Exception("virtualInputName cannot be null.");
            }

            HardwareInput matchHardwareInput = GetHardwareInputFromName(hardwareInputName);
            if (matchHardwareInput is null)
            {
                throw new Exception("HardwareInput with requested name could not be found.");
            }

            VirtualInput matchVirtualInput = GetVirtualInputFromName(virtualInputName);
            if (matchVirtualInput is null)
            {
                throw new Exception("VirtualInput with requested name could not be found.");
            }

            return new InputBinding(this, matchHardwareInput, matchVirtualInput);
        }
        public HardwareInput GetHardwareInputFromName(string hardwareInputName)
        {
            HardwareInput matchHardwareInput = null;

            foreach (HardwareInput hardwareInput in _hardwareInputs)
            {
                if (hardwareInput.Name == hardwareInputName)
                {
                    matchHardwareInput = hardwareInput;
                    break;
                }
            }

            return matchHardwareInput;
        }
        public VirtualInput GetVirtualInputFromName(string virtualInputName)
        {
            VirtualInput matchVirtualInput = null;

            foreach (VirtualInput virtualInput in _virtualInputs)
            {
                if (virtualInput.Name == virtualInputName)
                {
                    matchVirtualInput = virtualInput;
                    break;
                }
            }

            return matchVirtualInput;
        }
        public void AddHardwareInput(HardwareInput hardwareInput)
        {
            if (hardwareInput is null)
            {
                throw new Exception("hardwareInput cannot be null.");
            }

            if (hardwareInput.InputManager != this)
            {
                throw new Exception("hardwareInput belongs to a different InputManager.");
            }

            foreach (HardwareInput potentialDuplicate in _hardwareInputs)
            {
                if (potentialDuplicate.Name == hardwareInput.Name)
                {
                    throw new Exception("hardwareInput must have a unique name.");
                }
            }

            _hardwareInputs.Add(hardwareInput);
        }
        public void AddVirtualInput(VirtualInput virtualInput)
        {
            if (virtualInput is null)
            {
                throw new Exception("virtualInput cannot be null.");
            }

            if (virtualInput.InputManager != this)
            {
                throw new Exception("virtualInput belongs to a different InputManager.");
            }

            foreach (VirtualInput potentialDuplicate in _virtualInputs)
            {
                if (potentialDuplicate.Name == virtualInput.Name)
                {
                    throw new Exception("virtualInput must have a unique name.");
                }
            }

            _virtualInputs.Add(virtualInput);
        }
        public void AddInputBinding(InputBinding inputBinding)
        {
            if (inputBinding is null)
            {
                throw new Exception("inputBinding cannot be null.");
            }

            if (inputBinding.InputManager != this)
            {
                throw new Exception("inputBinding belongs to a different InputManager.");
            }

            foreach (InputBinding potentialDuplicate in _inputBindings)
            {
                if (potentialDuplicate.HardwareInput == inputBinding.HardwareInput && potentialDuplicate.VirtualInput == inputBinding.VirtualInput)
                {
                    throw new Exception("inputBinding must have a unique hardwareInput or a unique VirtualInput.");
                }
            }

            _inputBindings.Add(inputBinding);
        }
    }
}