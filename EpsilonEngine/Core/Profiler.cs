namespace EpsilonEngine
{
    internal static class Profiler
    {
        #region Private Variables
        private static long _initializeStart = 0;
        private static long _updateStart = 0;
        private static long _updateEnd = 0;
        private static long _renderStart = 0;
        private static long _renderEnd = 0;
        private static System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
        #endregion
        #region Constructors
        static Profiler()
        {
            _stopWatch.Restart();
        }
        #endregion
        #region Internal Methods
        internal static void InitializeStart()
        {
            _initializeStart = _stopWatch.ElapsedTicks;
        }
        internal static void InitializeEnd()
        {
            long initializeEnd = _stopWatch.ElapsedTicks;
            long initializeTime = initializeEnd - _initializeStart;
            System.Console.WriteLine($"Debug Profiler - {initializeTime} Tick Initialization which is {initializeTime / 10000000.0} seconds.");
        }
        internal static void UpdateStart()
        {
            _updateStart = _stopWatch.ElapsedTicks;
        }
        internal static void UpdateEnd()
        {
            _updateEnd = _stopWatch.ElapsedTicks;
        }
        internal static void RenderStart()
        {
            _renderStart = _stopWatch.ElapsedTicks;
        }
        internal static void RenderEnd()
        {
            _renderEnd = _stopWatch.ElapsedTicks;
        }
        internal static void Print(int currentFPS)
        {
            long updateTicks = _updateEnd - _updateStart;
            long renderTicks = _renderEnd - _renderStart;
            System.Console.WriteLine($"Debug Profiler - {updateTicks} Tick Update - {renderTicks} Tick Render - {currentFPS} Real FPS - {10000000 / (updateTicks + renderTicks)} Theoretical FPS.");
        }
        #endregion
    }
}