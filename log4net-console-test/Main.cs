using System;
using Missing.Diagnostics;
using System.Collections.Specialized;
using System.Data;

namespace log4netconsoletest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Missing Log4Net Console Test");
			
			//Log.UseConfig(Log4NetConfigurations.SimpleConsole);
			Log.UseConfig(Log4NetConfigurations.SimpleConsoleColored);
			
			//
			// trace
			//
			Log.Trace();
			Log.Trace("trace with static message");
			Log.Trace("trace with {0}", "format");
			
			//
			// debug
			//
			Log.Debug("debug with static message");
			Log.Debug("debug with {0}", "format");
			
			NameValueCollection debugNvc = new NameValueCollection();
			debugNvc.Add("spock", "first officer");
			debugNvc.Add("kirk", "captain");
			debugNvc.Add("mccoy", "senior medical officer");
			Log.Debug(debugNvc);
			
			DataTable debugDt = new DataTable();
			debugDt.Columns.Add(new DataColumn("firstColumn"));
			debugDt.Columns.Add(new DataColumn("secondColumn"));
			DataRow tmpRow = debugDt.NewRow();
			tmpRow[0] = "1st. row value for first";
			tmpRow[1] = "1st. row value for second";
			Log.Debug(tmpRow);
			
			//
			// information
			//
			Log.Information("information with static message");
			Log.Information("information with {0}", "format");
			
			//
			// warning
			//
			Log.Warning("warning with static message");
			Log.Warning("warning with {0}", "format");
			
			//
			// error
			//
			Log.Error("error with static message");
			Log.Error("error with {0}", "format");
			
			//
			// fatal
			//
			Log.Fatal("fatal with static message");
			Log.Fatal("fatal with {0}", "format");
		}
	}
}
