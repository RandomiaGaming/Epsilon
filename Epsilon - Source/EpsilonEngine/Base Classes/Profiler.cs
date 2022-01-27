using System;

namespace EpsilonEngine
{
    public static class DebugProfiler
    {
        private const int bufferSize = 1000;
        private static uint[] buffer = new uint[bufferSize];
        static DebugProfiler()
        {
            buffer = new uint[bufferSize];
            for (int i = 0; i < bufferSize; i++)
            {
                buffer[i] = 0;
            }
        }
        public static void Reset()
        {
            buffer = new uint[bufferSize];
            for (int i = 0; i < bufferSize; i++)
            {
                buffer[i] = 0;
            }
        }
        public static void AddSample(uint sample)
        {
            for (int i = bufferSize - 1; i > 0; i--)
            {
                buffer[i] = buffer[i - 1];
            }
            buffer[0] = sample;
        }
        public static void PrintAverage()
        {
            ulong total = 0;
            for (int i = 0; i < bufferSize; i++)
            {
                total += buffer[i];
            }
            ulong average = total / bufferSize;
            if (average <= 0)
            {
                Console.WriteLine($"Debug Profiler - Infinity FPS - 0 TPF.");
            }
            else
            {
                Console.WriteLine($"Debug Profiler - {10000000 / average} FPS - {average} TPF.");
            }
        }
    }
}
