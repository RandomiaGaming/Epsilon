using System;
using System.IO;
using System.Collections.Generic;
public static class Program
{
    [STAThread]
    public static void Main()
    {
        EpsilonCore.Epsilon epsilon = new EpsilonCore.Epsilon();
        epsilon.Run();
    }
}