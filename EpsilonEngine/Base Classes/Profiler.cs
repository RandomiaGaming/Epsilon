using System;
namespace EpsilonEngine
{
    public static class DebugProfiler
    {
        private static long frameEnd = 0;
        private static long updateEnd = 0;
        private static long renderEnd = 0;

        private static long frameStart = 0;
        private static long updateStart = 0;
        private static long renderStart = 0;

        private static System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
        static DebugProfiler()
        {
            _stopWatch.Restart();
        }
        public static void UpdateStart()
        {
            updateStart = _stopWatch.ElapsedTicks;
        }
        public static void UpdateEnd()
        {
            updateEnd = _stopWatch.ElapsedTicks;
        }
        public static void RenderStart()
        {
            renderStart = _stopWatch.ElapsedTicks;
        }
        public static void RenderEnd()
        {
            renderEnd = _stopWatch.ElapsedTicks;
        }
        public static void FrameStart()
        {
            frameStart = _stopWatch.ElapsedTicks;
        }
        public static void FrameEnd()
        {
            frameEnd = _stopWatch.ElapsedTicks;
        }
        public static void Print()
        {
            long updateTime = updateEnd - updateStart;
            long renderTime = renderEnd - renderStart;
            long frameTime = frameEnd - frameStart;

            if (frameTime <= 0)
            {
                Console.WriteLine($"Debug Profiler - Infinity FPS - {frameTime} Tick Frame - {frameTime - updateTime - renderTime} Tick MonoGame Update - {updateTime} Tick Update - {renderTime} Tick Render.");
                return;
            }

            Console.WriteLine($"Debug Profiler - {10000000 / frameTime} FPS - {frameTime} Tick Frame - {frameTime - updateTime - renderTime} Tick MonoGame Update - {updateTime} Tick Update - {renderTime} Tick Render.");
        }
    }
}
