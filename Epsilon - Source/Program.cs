using System;
using System.Diagnostics;
using System.Collections.Generic;
public static class Program
{
    [STAThread]
    public static void Main()
    {
        MonoGameInterface monoGameInterface = new MonoGameInterface();
        monoGameInterface.Run();
        /*
        DontMelt.Game dontMeltGame = new DontMelt.Game();

        dontMeltGame.Initialize(new DontMelt.InitializePacket(false, "Windows"));

        Stopwatch gameStopwatch = new Stopwatch();
        long ticksLastFrame = 0;
        gameStopwatch.Start();

        while (true)
        {
            dontMeltGame.Tick(new DontMelt.InputPacket(new DontMelt.KeyboardState(false, false, false, false, new List<DontMelt.KeyboardButton>()), new DontMelt.MouseState(new DontMelt.Point(0, 0), 0, false, false, false, new List<DontMelt.MouseButton>())));
            Console.WriteLine(gameStopwatch.ElapsedTicks - ticksLastFrame);
            ticksLastFrame = gameStopwatch.ElapsedTicks;
        }*/
    }
}