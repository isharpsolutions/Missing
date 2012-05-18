using System;
using System.Diagnostics;

namespace tojsonperformanceconsoletest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
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
	}
}