using System;
namespace Epsilon
{
    public sealed class TickInputPacket
    {
        public readonly InputPacket inputPacket = null;
        public readonly Point viewPortPixelRect = new Point(0, 0);
        public TickInputPacket(InputPacket inputPacket, Point viewPortPixelRect)
        {
            if (inputPacket is null)
            {
                throw new NullReferenceException();
            }
            else
            {
                this.inputPacket = inputPacket;
            }
            this.viewPortPixelRect = viewPortPixelRect;
        }
    }
}
