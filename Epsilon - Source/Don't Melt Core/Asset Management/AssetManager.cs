using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DontMelt
{
    public static class AssetHelper
    {
        private static List<AssetCodecInfo> codecs = null;
        private static void LoadCodecs()
        {
            codecs = new List<AssetCodecInfo>();
            Assembly assembly = Assembly.GetCallingAssembly();
            foreach (TypeInfo type in assembly.DefinedTypes)
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    RegisterAssetCodecAttribute assetCodecAttribute = method.GetCustomAttribute<RegisterAssetCodecAttribute>();
                    if (assetCodecAttribute is not null)
                    {
                        codecs.Add(new AssetCodecInfo(assetCodecAttribute, method));
                    }
                }
            }
        }
        public static AssetBase LoadAsset(string name)
        {
            if (codecs is null)
            {
                LoadCodecs();
            }

            Assembly assembly = Assembly.GetCallingAssembly();
            string bestFitResourceName = null;
            foreach(string resourceName in assembly.GetManifestResourceNames())
            {
                if(resourceName.EndsWith(name))
                {
                    bestFitResourceName = resourceName;
                }
            }
            if(bestFitResourceName is null)
            {
                return null;
            }
            else
            {
                string[] splitResourceName = bestFitResourceName.Split('.');
                string resourceExtension = splitResourceName[splitResourceName.Length - 1];
                foreach(AssetCodecInfo assetCodec in codecs)
                {
                    if (assetCodec.managedExtensions.Contains(resourceExtension.ToUpper()))
                    {
                        return assetCodec.LoadAsset(assembly.GetManifestResourceStream(bestFitResourceName), bestFitResourceName);
                    }
                }
                return null;
            }
        }
       public static T LoadAsset<T>(string name) where T : AssetBase
        {
            AssetBase loadedAsset = LoadAsset(name);
            if(loadedAsset is null)
            {
                return null;
            }
            if (loadedAsset.GetType().IsAssignableFrom(typeof(T)))
            {
                return (T)loadedAsset;
            }
            else
            {
                return null;
            }
        }
    }
}