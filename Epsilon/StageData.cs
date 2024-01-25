using System;
using System.Collections.Generic;
using EpsilonEngine;
namespace Epsilon
{
    public struct TileData
    {
        public Point position;
        public string stageItem;
        public TileData(Point position, string stageItem)
        {
            this.position = position;
            if (stageItem is null)
            {
                throw new NullReferenceException();
            }
            this.stageItem = stageItem;
        }
    }
    public sealed class StageData
    {
        public Point playerStartLocation = Point.Zero;
        public List<TileData> tilemapData = new List<TileData>();
    }
}
