using System;
namespace Epsilon
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Epsilon epsilon = new Epsilon();
            epsilon.Run();
        }
    }
}