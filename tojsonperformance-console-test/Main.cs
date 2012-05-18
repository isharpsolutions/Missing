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
			
			OneLevel();
			Console.WriteLine();
			TwoLevels();
			Console.WriteLine();
			ThreeLevels();
		}
		
		public static void Disclaimer()
		{
			Console.WriteLine("*** Disclaimer ***");
			
			Console.WriteLine("This should not be seen as the absolute thruth, as this test does not do millions of runs with proper reset between runs.");
			
			Console.WriteLine();
			Console.WriteLine();
		}
		
		#region OneLevel
		public static void OneLevel()
		{
			OneLevel obj = new OneLevel();
			
			Stopwatch missing = MissingTest.Run(obj);
			Stopwatch sstext = ServiceStackTest.Run(obj);
			
			Console.WriteLine("OneLevel");
			Console.WriteLine("==============================================");
			Console.WriteLine("Unit is 'ticks'");
			Console.WriteLine("Missing:      {0,7}", missing.ElapsedTicks);
			Console.WriteLine("ServiceStack: {0,7}", sstext.ElapsedTicks);
		}
		#endregion OneLevel
		
		#region TwoLevels
		public static void TwoLevels()
		{
			TwoLevels obj = new TwoLevels();
			
			Stopwatch missing = MissingTest.Run(obj);
			Stopwatch sstext = ServiceStackTest.Run(obj);
			
			Console.WriteLine("TwoLevels");
			Console.WriteLine("==============================================");
			Console.WriteLine("Unit is 'ticks'");
			Console.WriteLine("Missing:      {0,7}", missing.ElapsedTicks);
			Console.WriteLine("ServiceStack: {0,7}", sstext.ElapsedTicks);
		}
		#endregion TwoLevels
		
		#region ThreeLevels
		public static void ThreeLevels()
		{
			ThreeLevels obj = new ThreeLevels();
			
			Stopwatch missing = MissingTest.Run(obj);
			Stopwatch sstext = ServiceStackTest.Run(obj);
			
			Console.WriteLine("ThreeLevels");
			Console.WriteLine("==============================================");
			Console.WriteLine("Unit is 'ticks'");
			Console.WriteLine("Missing:      {0,7}", missing.ElapsedTicks);
			Console.WriteLine("ServiceStack: {0,7}", sstext.ElapsedTicks);
		}
		#endregion ThreeLevels
	}
}