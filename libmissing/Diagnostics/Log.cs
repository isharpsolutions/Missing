using System;
using log4net;
using log4net.Config;

namespace Missing.Diagnostics
{
	public static class Log
	{
		private static ILog log;
		
		private static ILog GetLogger()
		{
			if (log == null)
			{
				log = LogManager.GetLogger("Missing.Diagnostics");
				
				XmlConfigurator.Configure();
				
				#warning We can use this overload to have default easy to use no-xml needed configs directly in the code
				//XmlConfigurator.Configure( XmlElement )
			}
			
			return log;
		}
		
		public static void Trace(string message)
		{
			string caller = String.Empty;
			string callerClass = String.Empty;
			string callerName = String.Empty;
			string fullName = String.Empty;;
			string callerNamespace = String.Empty;			
			
			LogTools.FindFrame("Trace", out caller, out callerClass, out callerName, out fullName, out callerNamespace);
			
			// Trace with the following format
			// [LIBNAME.CALLING_CLASS.CALLING_METHOD] message
			//GetLogger().Debug( String.Format("[{0}.{1}.{2}] {3}", callerName, callerClass, caller, message) );
			
			log4net.ThreadContext.Properties["mycaller"] = String.Format("{0}.{1}.{2}", callerName, callerClass, caller);
			
			GetLogger().Debug(message);
		}
	}
}