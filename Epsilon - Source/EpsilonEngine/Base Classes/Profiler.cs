using System;

namespace EpsilonEngine
{
    public static class DebugProfiler
    {
        private static long lastFrameTime = 0;
        private static long updateTime = 0;
        private static long renderTime = 0;

        private static long lastFrameEndTime = 0;
        private static long updateStartTime = 0;
        private static long renderStartTime = 0;


        private static System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
        static DebugProfiler()
        {
            _stopWatch.Restart();
        }
        public static void UpdateStart()
        {
            updateStartTime = _stopWatch.ElapsedTicks;
        }
        public static void UpdateEnd()
        {
            updateTime = _stopWatch.ElapsedTicks - updateStartTime;
        }
        public static void RenderStart()
        {
            renderStartTime = _stopWatch.ElapsedTicks;
        }
        public static void RenderEnd()
        {
            renderTime = _stopWatch.ElapsedTicks - renderStartTime;
        }
        public static void FrameElapsed()
        {
            long currentTime = _stopWatch.ElapsedTicks;
            lastFrameTime = currentTime - lastFrameEndTime;
            lastFrameEndTime = currentTime;
        }
        public static void Print()
        {
            if (lastFrameTime == 0)
            {
                Console.WriteLine($"Debug Profiler - Infinity FPS - {lastFrameTime} Tick Frame - {updateTime} Tick Update - {renderTime} Tick Render.");
            }
            else
            {
                Console.WriteLine($"Debug Profiler - {10000000 / lastFrameTime} FPS - {lastFrameTime} Tick Frame - {lastFrameTime - updateTime - renderTime} Tick MonoGame Update - {updateTime} Tick Update - {renderTime} Tick Render.");
            }
        }
    }
}
