using System.Collections.Generic;
using System;
namespace Epsilon
{
    public sealed class TickReturnPacket
    {
        public readonly List<Exception> exceptions = new List<Exception>();
        public readonly Texture frameBuffer = null;
        public readonly AudioClip audioBuffer = null;
        public readonly bool requestingToQuit = false;
        public TickReturnPacket(List<Exception> exceptions, Texture frameBuffer, AudioClip audioBuffer, bool requestingToQuit)
        {
            if (exceptions is null)
            {
                this.exceptions = new List<Exception>();
            }
            else
            {
                this.exceptions = exceptions;
            }
            this.frameBuffer = frameBuffer;
            this.audioBuffer = audioBuffer;
            this.requestingToQuit = requestingToQuit;
        }
    }
}