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
			
			//Log.UseConfig(Log4NetConfigurations.GetSimpleConsole());
			//Log.UseConfig(Log4NetConfigurations.GetSimpleConsoleColored());
			
			/*****************************************************************
			 CREATE SCHEMA `missing` DEFAULT CHARACTER SET utf8;
			  
			 CREATE TABLE `mysqladonetappender` (
			   `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
			   `DateTime` datetime DEFAULT NULL,
			   `Level` text,
			   `Caller` text,
			   `Message` text,
			   PRIMARY KEY (`id`)
			 ) ENGINE=InnoDB DEFAULT CHARSET=utf8
			 
			 create user 'missing'@'localhost' identified by '123456';
			 grant insert on missing.* to 'missing'@'localhost';
			 flush privileges;
			 ******************************************************************/
			Log.UseConfig(Log4NetConfigurations.GetMySqlAdoNetAppender("mysqladonetappender", "127.0.0.1", "missing", "missing", "123456"));
			
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
