using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon
{
    //This enum is a more programmer friedly way of storing asset types.
    //An Asset's type will also be reflected by the header to its UAN.
    public enum AssetType { Texture, Animation, Assembly, GameObject, Wipe, Map, Input };
    public abstract class Asset
    {
        public Stream assetSource = null;
        //An asset's source file is the file from which this asset was loaded.
        //Note that the source file is always full path, unlike emeta which can contain relative paths.
        public string sourceFile = "C:\\Users\\RandomiaGaming\\Desktop\\File.emeta";
        //UAN stands for unique asset name.
        //UANs must begin with either "texture:", "animation:", "assembly:", "gameobject:", "wipe:", "Input:" or "map:".
        //UANs will then contain their name in the format "asset type:mod name.asset name"
        //For example the players idle texture can be accessed with "texture:epsilon.player.idle.0".
        public string UAN = "texture:epsilon.player.idle.0";
        public Asset(string fileSource)
        {

        }
    }
}
