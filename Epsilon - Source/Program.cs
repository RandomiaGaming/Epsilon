using System;
public static class Program
{
    [STAThread]
    public static void Main()
    {
        Epsilon.Epsilon epsilon = new Epsilon.Epsilon();
        epsilon.Run();
    }
}