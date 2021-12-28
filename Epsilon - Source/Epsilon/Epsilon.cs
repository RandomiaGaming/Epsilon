using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
namespace EpsilonCore
{
    public sealed class Epsilon
    {
        private string arguments = null;
        private EpsilonGame _game = null;
        public EpsilonGame game
        {
            get
            {
                return _game;
            }
            private set
            {
                _game = value;
            }
        }
        public Epsilon()
        {
            arguments = "";
            game = new EpsilonGame();
        }
        public Epsilon(string arguments)
        {
            if(arguments is null)
            {
                arguments = "";
            }
            this.arguments = arguments;
            game = new EpsilonGame();
        }
        public Epsilon(string[] arguments)
        {
            if (arguments is null || arguments.Length <= 0)
            {
                this.arguments = "";
            }
            else
            {
                foreach (string arg in arguments)
                {
                    this.arguments = this.arguments + "\"" + arg + "\"\n";
                }
            }
            game = new EpsilonGame();
        }
        public Profiler profiler = new Profiler();
        /*private static Assembly _epsilonCoreAssembly;
        private static Assembly epsilonCoreAssembly
        {
            get
            {
                if (_epsilonCoreAssembly is null)
                {
                    _epsilonCoreAssembly = Assembly.GetCallingAssembly();
                }
                return _epsilonCoreAssembly;
            }
        }
        private static string epsilonCoreLocation
        {
            get
            {
                return epsilonCoreAssembly.Location;
            }
        }
        private static string rootDirectory
        {
            get
            {
                return Path.GetDirectoryName(epsilonCoreLocation);
            }
        }
        private static readonly List<Assembly> loadedMods = new List<Assembly>();*/
        public void Run(string arguments)
        {
            if(arguments is null)
            {
                arguments = "";
            }
            this.arguments = arguments;
            profiler.Reset();
            Run();
        }
        public void Run()
        {
            game.Run();
        }
        public void UpdateCallback()
        {

        }
        public void DrawingCallback()
        {
            
        }
        /*
public static void LoadMods()
{
   string modsListPath = rootDirectory + "\\ModsList.txt";
   if (File.Exists(modsListPath))
   {
       string modsList = File.ReadAllText(modsListPath);
       List<string> modsListSplit = new List<string>(modsList.Split('\n'));
       foreach (string modLocation in modsListSplit)
       {
           LoadMod(modLocation);
       }
   }
   else
   {
       //Warn that mod modLocation could not be found.
   }
}
public static void LoadMod(string modLocation)
{
   if (File.Exists(modLocation))
   {
       try
       {
           Assembly modAssembly = Assembly.LoadFrom(modLocation);
           loadedMods.Add(modAssembly);
       }
       catch (Exception ex)
       {
           throw new Exception("Could not load mod from file " + modLocation + " because of exception " + ex.Message);
       }
   }
   else
   {
       throw new FileNotFoundException("Could not load mod because no file exists at " + modLocation);
   }
}*/
    }
}
