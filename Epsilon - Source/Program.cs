using System;
namespace RandomiaGaming.Epsilon
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            MonoGameInterface monoGameInterface = new MonoGameInterface();
            monoGameInterface.Run();
        }
    }
}