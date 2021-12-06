using System;
using System.Collections.Generic;
public static class Program
{
    [STAThread]
    public static void Main()
    {
        List<string> arguments = new List<string>(Environment.GetCommandLineArgs());
        arguments.RemoveAt(0);
        string concatenatedArguments = "";
        foreach (string argument in arguments)
        {
            concatenatedArguments += $"\"{argument}\"";
        }
        EpsilonCore.EpsilonCore.Run(concatenatedArguments);
    }
}