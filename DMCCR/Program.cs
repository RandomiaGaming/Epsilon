using EpsilonEngine;
namespace DMCCR
{
	public static class Program
	{
		[System.STAThread()]
		public static int Main()
		{
			/*System.Console.WriteLine("Enter Sender Name:");
			string senderName = Console.ReadLine();
			System.Console.WriteLine("Enter Recipient Name:");
			string recipientName = System.Console.ReadLine();
			LocalMessagingClient LMClient = new LocalMessagingClient(senderName, recipientName);
			LMClient.OnMessageRecievedEvent = (string message) =>
			{
				System.Console.WriteLine(recipientName + " says " + message);
			};
			while (true)
			{
				LMClient.SendMessage(System.Console.ReadLine());
			}*/
			// RunSpeedTest();
			 DMCCR dmccr = new DMCCR();
			 dmccr.Run();
			 return 0;
		}
		public static void RunSpeedTest()
		{
			System.Diagnostics.Stopwatch speedTestStopwatch = System.Diagnostics.Stopwatch.StartNew();
			for (int i = 0; i < 10000; i++)
			{
				
			}
			speedTestStopwatch.Stop();
			System.Console.WriteLine(speedTestStopwatch.ElapsedTicks);
			System.Console.ReadLine();
		}
		/*public static string CreateFastRectArrayConstructor(Rect[] rectArray, string rectArrayName)
		{
		    if (rectArray is null)
		    {
			   throw new System.Exception("rectArray cannot be null.");
		    }
		    if (rectArrayName is null)
		    {
			   throw new System.Exception("rectArrayName cannot be null.");
		    }
		    if (rectArrayName is "")
		    {
			   throw new System.Exception("rectArrayName cannot be empty.");
		    }
		    string output = $"#region {rectArrayName} - Fast Rect[] Constructor";
		    output += $"\nRect[] {rectArrayName} = new Rect[{rectArray.Length}];";
		    for (int i = 0; i < rectArray.Length; i++)
		    {
			   output += $"\n{rectArrayName}[{i}]._minX = {rectArray[i]._minX};";
			   output += $"\n{rectArrayName}[{i}]._minY = {rectArray[i]._minY};";
			   output += $"\n{rectArrayName}[{i}]._maxX = {rectArray[i]._maxX};";
			   output += $"\n{rectArrayName}[{i}]._maxY = {rectArray[i]._maxY};";
		    }
		    output += $"\n#endregion";
		    return output;
		}*/
	}
}