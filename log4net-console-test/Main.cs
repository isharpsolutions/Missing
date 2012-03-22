using System;
using Missing.Diagnostics;

namespace log4netconsoletest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Missing Log4Net Console Test");
			
			Log.UseConfig(Log4NetConfigurations.SimpleConsole);
			Log.Trace("muhahaha");
		}
	}
}
