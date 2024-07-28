using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EpsilonEngine
{
    public static class AssetHelper
    {
        private static List<AssetCodec> codecs = null;
        private static void LoadCodecs()
        {
            codecs = new List<AssetCodec>();
            Assembly assembly = Assembly.GetCallingAssembly();
            foreach (TypeInfo type in assembly.DefinedTypes)
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    RegisterAssetCodecAttribute assetCodecAttribute = method.GetCustomAttribute<RegisterAssetCodecAttribute>();
                    if (assetCodecAttribute is not null)
                    {
                        codecs.Add(new AssetCodec(assetCodecAttribute, method));
                    }
                }
            }
        }
        public static AssetBase LoadAsset(string assetName)
        {
            if (codecs is null)
            {
                LoadCodecs();
            }

            Assembly assembly = Assembly.GetCallingAssembly();
            string bestFitResourceName = null;
            foreach(string resourceName in assembly.GetManifestResourceNames())
            {
                if(resourceName.EndsWith(assetName))
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
                foreach(AssetCodec assetCodec in codecs)
                {
                    if (assetCodec.managedExtensions.Contains(resourceExtension.ToUpper()))
                    {
                        return assetCodec.LoadAsset(assembly.GetManifestResourceStream(bestFitResourceName), bestFitResourceName);
                    }
                }
                return null;
            }
        }
    }
}