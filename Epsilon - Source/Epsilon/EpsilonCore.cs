using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
namespace EpsilonCore
{
    public static class EpsilonCore
    {
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
        public static void Run(string arguments)
        {
            EpsilonGame game = new EpsilonGame();
            game.Run();
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
