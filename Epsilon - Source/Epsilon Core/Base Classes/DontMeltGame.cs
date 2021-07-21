using System.Collections.Generic;
namespace DontMelt
{
    public sealed class DontMeltGame
    {
        public StagePlayer stagePlayer { get; private set; } = null;
        private bool requestingToQuit = false;
        public bool paused = false;
        public DontMeltGame()
        {
            StageData stageData = new StageData();
            stageData.tilemapData = new List<TileData>() { new TileData(Point.Zero, "Player"), new TileData(new Point(0, -1), "Ground"), new TileData(new Point(0, 3), "BounceBox") };
            stagePlayer = new StagePlayer(stageData);
        }
        public TickReturnPacket Tick(TickInputPacket packet)
        {
            if (stagePlayer is null)
            {
                return new TickReturnPacket(new List<System.Exception>(), null, null, requestingToQuit);
            }
            else
            {
                stagePlayer.Tick(packet.inputPacket);
                Texture frame = stagePlayer.Render();;
                return new TickReturnPacket(null, frame, new AudioClip(48000, new byte[0]), requestingToQuit);
            }
        }
        public void Quit()
        {
            requestingToQuit = true;
        }
    }
}