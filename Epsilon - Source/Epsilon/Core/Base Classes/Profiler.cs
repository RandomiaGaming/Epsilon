using System;

namespace Epsilon
{
    public static class DebugProfiler
    {
        private static long totalElapsedTicks = 0;
        private static int currentWeight = 0;
        static DebugProfiler()
        {
            totalElapsedTicks = 0;
            currentWeight = 0;
        }
        public static void Reset()
        {
            totalElapsedTicks = 0;
            currentWeight = 0;
        }
        public static void AddSample(long elapsedTicks)
        {
            totalElapsedTicks = elapsedTicks;
            currentWeight = 1;
        }
        public static void Print()
        {
            //This should never happen but just incase we don't want to get a devision by 0 error.
            if(currentWeight <= 0)
            {
                return;
            }
            long average = totalElapsedTicks / currentWeight;
            //On the first few frames of gameplay the delta time in ticks can sometimes be 0 due to an error in MonoGame. This would cause a devision by 0 error so we have to accomodate for that.
            if (average == 0)
            {
                Console.WriteLine($"Debug Profiler - Infinity FPS - 0 TPF.");
                return;
            }
            Console.WriteLine($"Debug Profiler - {10000000 / average} FPS - {average} TPF.");
        }
    }
}
