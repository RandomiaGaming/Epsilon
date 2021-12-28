using System;

namespace EpsilonCore
{
    public class Profiler
    {
        private long total = 0;
        private int currentWeight = 0;
        public Profiler()
        {
            total = 0;
            currentWeight = 0;
        }
        public void Reset()
        {
            total = 0;
            currentWeight = 0;
        }
        public void AddSample(long ticks)
        {
            total += ticks;
            currentWeight++;
        }
        public void PrintValue()
        {
            long average = total / currentWeight;
            if (average == 0)
            {
                Console.WriteLine($"Profiler - Infinity FPS - 0 TPF.");
            }
            else
            {
                Console.WriteLine($"Profiler - {10000000 / average} FPS - {average} TPF.");
            }
        }
    }
}
