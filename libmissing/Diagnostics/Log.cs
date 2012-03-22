using System;
using log4net;
using log4net.Config;
using System.Xml;

namespace Missing.Diagnostics
{
	public static class Log
	{
		/// <summary>
		/// The name of the context property containing
		/// the name of the caller
		/// </summary>
		public static readonly string CallerContextName = "missing.caller";
		
		#region Logger specific
		/// <summary>
		/// The log instance (may be null)
		/// </summary>
		private static ILog log;
		
		/// <summary>
		/// Get logger instance
		/// </summary>
		/// <returns>
		/// A logger
		/// </returns>
		private static ILog GetLogger()
		{
			if (log == null)
			{
				log = LogManager.GetLogger("Missing.Diagnostics");
				
				if (config == null)
				{
					XmlConfigurator.Configure();
				}
				
				else
				{
					XmlConfigurator.Configure( config );
				}
			}
			
			return log;
		}
		#endregion Logger specific
		
		#region Config
		/// <summary>
		/// XML configuration for logger
		/// </summary>
		private static XmlElement config = null;
		
		/// <summary>
		/// Set the configuration to use. If not set
		/// whatever config is found in app.config/web.config is used
		/// </summary>
		/// <param name="configXml">
		/// Config xml
		/// </param>
		/// <remarks>
		/// We have some default configurations in <see cref="Log4NetConfigurations"/>
		/// </remarks>
		public static void UseConfig(XmlElement configXml)
		{
			config = configXml;
		}
		#endregion Config
		
		public static void Trace(string message)
		{
			string caller = String.Empty;
			string callerClass = String.Empty;
			string callerName = String.Empty;
			string fullName = String.Empty;;
			string callerNamespace = String.Empty;			
			
			LogTools.FindFrame("Trace", out caller, out callerClass, out callerName, out fullName, out callerNamespace);
			
			log4net.ThreadContext.Properties[CallerContextName] = String.Format("{0}.{1}.{2}", callerName, callerClass, caller);
			
			GetLogger().Debug(message);
		}
	}
}