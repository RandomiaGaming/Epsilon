using System;
using System.Diagnostics;
public static class Program
{
    [STAThread]
    public static void Main()
    {
        Epsilon.Epsilon epsilon = new Epsilon.Epsilon();
        epsilon.Run();
    }
}