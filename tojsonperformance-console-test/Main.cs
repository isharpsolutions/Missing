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
			
			OneLevel();
			Console.WriteLine();
			TwoLevels();
		}
		
		#region OneLevel
		public static void OneLevel()
		{
			OneLevel obj = new OneLevel() {
				MyBool = true,
				MyDecimal = 35.3m,
				MyDouble = 235.2d,
				MyFloat = 242.7f,
				MyInt = 254,
				MyLong = 35385L,
				MyString = @"Something kewl with
a newline in it"
			};
			
			Stopwatch missing = MissingTest.OneLevel(obj);
			Stopwatch sstext = ServiceStackTest.OneLevel(obj);
			
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
			TwoLevels obj = new TwoLevels() {
				OneBool = false,
				OneDecimal = 3632.2m,
				OneDouble = 2525.224d,
				OneFloat = 1243.24f,
				OneInt = 5432,
				OneLong = 987765L,
				OneString = "Level One",
				
				Sub = new OneLevel() {
					MyBool = true,
					MyDecimal = 35.3m,
					MyDouble = 235.2d,
					MyFloat = 242.7f,
					MyInt = 254,
					MyLong = 35385L,
					MyString = @"Something kewl with
	a newline in it"
				}
			};
			
			Stopwatch missing = MissingTest.TwoLevels(obj);
			Stopwatch sstext = ServiceStackTest.TwoLevels(obj);
			
			Console.WriteLine("TwoLevels");
			Console.WriteLine("==============================================");
			Console.WriteLine("Unit is 'ticks'");
			Console.WriteLine("Missing:      {0,7}", missing.ElapsedTicks);
			Console.WriteLine("ServiceStack: {0,7}", sstext.ElapsedTicks);
		}
		#endregion TwoLevels
	}
}