using System;
using System.Collections.Generic;
namespace DontMelt
{
    public struct TileData
    {
        public Point position;
        public string stageItem;
        public string NSID;
        public TileData(Point position, string stageItem)
        {
            this.position = position;
            if (stageItem is null)
            {
                throw new NullReferenceException();
            }
            this.stageItem = stageItem;
            NSID = null;
        }
        public TileData(Point position, string stageItem, string NSID)
        {
            this.position = position;
            if (stageItem is null)
            {
                throw new NullReferenceException();
            }
            this.stageItem = stageItem;
            this.NSID = NSID;
        }
    }
    public sealed class StageData
    {
        public Point playerStartLocation = Point.Zero;
        public string playerNSID = null;
        public List<TileData> tilemapData = new List<TileData>();
    }
}
