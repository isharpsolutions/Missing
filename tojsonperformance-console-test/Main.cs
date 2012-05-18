using System;
using System.Diagnostics;

namespace tojsonperformanceconsoletest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("ToJson performance test: Missing vs ServiceStack.Text");
			Console.WriteLine();
			
			Disclaimer();
			
			// make sure both methods are "warm"
			MissingTest.Run(new OneLevel());
			ServiceStackTest.Run(new OneLevel());
			
			Run(new OneLevel());
			Run(new TwoLevels());
			Run(new ThreeLevels());
			Run(new FourLevels());
		}
		
		public static void Disclaimer()
		{
			Console.WriteLine("*** Disclaimer ***");
			
			Console.WriteLine("This should not be seen as the absolute thruth, as this test does not do millions of runs with proper reset between runs.");
			
			Console.WriteLine();
			Console.WriteLine();
		}
		
		public static void Run(object obj)
		{
			Stopwatch missing = MissingTest.Run(obj);
			Stopwatch sstext = ServiceStackTest.Run(obj);
			
			Console.WriteLine(obj.GetType().Name);
			Console.WriteLine("==============================================");
			Console.WriteLine("Unit is 'ticks'");
			Console.WriteLine("Missing:      {0,7}", missing.ElapsedTicks);
			Console.WriteLine("ServiceStack: {0,7}", sstext.ElapsedTicks);
			
			Console.WriteLine();
		}
	}
}