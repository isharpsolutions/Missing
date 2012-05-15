using System;
using log4net;
using log4net.Config;
using System.Xml;
using System.Data;
using System.Collections.Specialized;
using System.Text;

namespace Missing.Diagnostics
{
	/// <summary>
	/// Convenience methods regarding logging
	/// </summary>
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
		
		/// <summary>
		/// Add a <logger/> section to the current config
		/// </summary>
		/// <param name="loggerName">
		/// The name of the logger
		/// </param>
		/// <param name="level">
		/// The log level of the logger
		/// </param>
		/// <example>
		/// This example shows how to use the method to disable logging from NHibernate
		/// 
		/// <code>
		/// Log.UseConfig(Log4NetConfigurations.GetMySqlAdoNetAppender("admin", "127.0.0.1", "myschema", "myuser", "mypassword"));
		///	Log.AddLogger("NHibernate", "ERROR");
		/// </code>
		/// </example>
		public static void AddLogger(string loggerName, string level)
		{
			if (config == null)
			{
				throw new InvalidOperationException("You must call Log.UseConfig(..) before calling AddLogger(..)");
			}
			
			XmlElement newLogger = config.OwnerDocument.CreateElement("logger");
			newLogger.SetAttribute("name", loggerName);
			
			XmlElement newLoggerLevel = config.OwnerDocument.CreateElement("level");
			newLoggerLevel.SetAttribute("value", level);
			
			newLogger.AppendChild(newLoggerLevel);
			config.AppendChild(newLogger);
		}
		#endregion Config
		
		#region Set caller in context
		private static void SetCallerInContext()
		{
			string caller = String.Empty;
			string callerClass = String.Empty;
			string callerName = String.Empty;
			string fullName = String.Empty;;
			string callerNamespace = String.Empty;			

			LogTools.FindFrame(out caller, out callerClass, out callerName, out fullName, out callerNamespace);
			
			log4net.ThreadContext.Properties[Log.CallerContextName] = String.Format("{0}.{1}.{2}", callerName, callerClass, caller);
		}
		#endregion Set caller in context
		
		#region Trace
		/// <summary>
		/// Write an empty trace message to log
		/// </summary>
		/// <remarks>
		/// Log4Net does not have a "trace" level, so
		/// all trace messages are output as "debug"
		/// </remarks>
		public static void Trace()
		{
			Log.Trace(String.Empty);
		}
		
		/// <summary>
		/// Write a trace message to log
		/// </summary>
		/// <param name="format">
		/// Message format
		/// </param>
		/// <param name="arg">
		/// Message arguments
		/// </param>
		/// <remarks>
		/// Log4Net does not have a "trace" level, so
		/// all trace messages are output as "debug"
		/// </remarks>
		public static void Trace(string format, params object[] arg)
		{
			Log.Trace(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write a trace message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		/// <remarks>
		/// Log4Net does not have a "trace" level, so
		/// all trace messages are output as "debug"
		/// </remarks>
		public static void Trace(string message)
		{
			Log.ToLog(EntrySeverity.Trace, message);
		}
		#endregion Trace
		
		#region Debug
		/// <summary>
		/// Write a debug message to log
		/// </summary>
		/// <param name="format">
		/// Message format
		/// </param>
		/// <param name="arg">
		/// Message arguments
		/// </param>
		public static void Debug(string format, params object[] arg)
		{
			Log.Debug(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write a debug message to log
		/// </summary>
		/// <param name="message">
		/// The debug message
		/// </param>
		public static void Debug(string message)
		{
			Log.ToLog(EntrySeverity.Debug, message);
		}
		
		/// <summary>
		/// Write a debug message with "column = value" from the given NameValueCollection
		/// </summary>
		/// <param name="row">
		/// A <see cref="DataRow"/>
		/// </param>
		public static void Debug(DataRow row)
		{
			StringBuilder sb = new StringBuilder();
			
			DataColumnCollection cols = row.Table.Columns;
			sb.AppendLine();
			for (int i=0; i<cols.Count; i++)
			{
				sb.AppendFormat("{0} = '{1}'{2}", cols[i].ColumnName, row[cols[i].ColumnName], Environment.NewLine);
			}
			
			Log.Debug(sb.ToString());
			
			sb = null;
		}
		
		/// <summary>
		/// Write a debug message with all the key-value-pairs in the given NameValueCollection
		/// </summary>
		/// <param name="data">
		/// The <see cref="NameValueCollection"/> to output
		/// </param>
		public static void Debug(NameValueCollection data)
		{
			StringBuilder sb = new StringBuilder(data.Count);
			
			string[] keys = data.AllKeys;
			foreach (string s in keys)
			{
				sb.AppendFormat("data[{0}] = {1}{2}", s, data[s], Environment.NewLine);
			}
			
			Log.Debug(sb.ToString());
			sb = null;
			keys = null;
		}
		#endregion Debug
		
		#region Information
		/// <summary>
		/// Write an information message to log
		/// </summary>
		/// <param name="format">
		/// Message format
		/// </param>
		/// <param name="arg">
		/// Message arguments
		/// </param>
		public static void Information(string format, params object[] arg)
		{
			Log.Information(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write an information message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public static void Information(string message)
		{
			Log.ToLog(EntrySeverity.Information, message);
		}
		#endregion Information
		
		#region Warning
		/// <summary>
		/// Write a warning message to log
		/// </summary>
		/// <param name="format">
		/// Message format
		/// </param>
		/// <param name="arg">
		/// Message arguments
		/// </param>
		public static void Warning(string format, params object[] arg)
		{
			Log.Warning(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write a warning message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public static void Warning(string message)
		{
			Log.ToLog(EntrySeverity.Warning, message);
		}
		#endregion Warning
		
		#region Error
		/// <summary>
		/// Write an error message to log
		/// </summary>
		/// <param name="format">
		/// Message format
		/// </param>
		/// <param name="arg">
		/// Message arguments
		/// </param>
		public static void Error(string format, params object[] arg)
		{
			Log.Error(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write an error message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public static void Error(string message)
		{
			Log.ToLog(EntrySeverity.Error, message);
		}
		#endregion Error
		
		#region Fatal
		/// <summary>
		/// Write a fatal message to log
		/// </summary>
		/// <param name="format">
		/// Message format
		/// </param>
		/// <param name="arg">
		/// Message arguments
		/// </param>
		public static void Fatal(string format, params object[] arg)
		{
			Log.Fatal(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write a fatal message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public static void Fatal(string message)
		{
			Log.ToLog(EntrySeverity.Fatal, message);
		}
		#endregion Fatal
		
		#region Message
		public static void ToLog(EntrySeverity severity, string message)
		{
			Log.SetCallerInContext();
			
			switch (severity)
			{
				case EntrySeverity.Trace:
				case EntrySeverity.Debug:
				{
					Log.GetLogger().Debug(message);
					break;
				}
					
				case EntrySeverity.Information:
				{
					Log.GetLogger().Info(message);
					break;
				}
					
				case EntrySeverity.Warning:
				{
					Log.GetLogger().Warn(message);
					break;
				}
					
				case EntrySeverity.Error:
				{
					Log.GetLogger().Error(message);
					break;
				}
					
				case EntrySeverity.Fatal:
				{
					Log.GetLogger().Fatal(message);
					break;
				}
			}
		}
		#endregion Message
	}
}