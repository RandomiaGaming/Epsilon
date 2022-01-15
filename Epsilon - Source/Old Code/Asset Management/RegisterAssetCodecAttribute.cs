using System;
namespace EpsilonEngine
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RegisterAssetCodecAttribute : Attribute
    {
        public readonly string[] managedExtensions = new string[0];
        public RegisterAssetCodecAttribute(string managedExtension)
        {
            if (managedExtension is null)
            {
                throw new NullReferenceException();
            }
            if (managedExtension == "")
            {
                throw new ArgumentException();
            }
            managedExtensions = new string[] { managedExtension.ToUpper() };
        }
        public RegisterAssetCodecAttribute(string[] managedExtensions)
        {
            if (managedExtensions is null)
            {
                throw new NullReferenceException();
            }
            if (managedExtensions.Length <= 0)
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < managedExtensions.Length; i++)
            {
                if (managedExtensions[i] is null)
                {
                    throw new ArgumentException();
                }
                else if (managedExtensions[i] == "")
                {
                    throw new ArgumentException();
                }
                managedExtensions[i] = managedExtensions[i].ToUpper();
            }
            this.managedExtensions = managedExtensions;
        }
    }
}
