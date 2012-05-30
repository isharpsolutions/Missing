using System;
using log4net;
using log4net.Config;
using System.Xml;
using System.Data;
using System.Collections.Specialized;
using System.Text;
using Missing.Diagnostics.Configurations;
using System.Collections.Generic;

namespace Missing.Diagnostics.Internal
{
	/// <summary>
	/// The implementation beneath <see cref="Log"/>
	/// </summary>
	public class LogImplementation
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Diagnostics.LogImplementation"/> class.
		/// </summary>
		public LogImplementation()
		{
		}
		
		#region Logger specific
		/// <summary>
		/// The log instance (may be null)
		/// </summary>
		private ILog log;
		
		/// <summary>
		/// Get logger instance
		/// </summary>
		/// <returns>
		/// A logger
		/// </returns>
		private ILog GetLogger()
		{
			// create a new logger if we do not have one already
			// or if the name of the existing logger is wrong
			if (this.log == null || !this.log.Logger.Name.Equals(this.loggerName))
			{
				// check if we have previosly created the wanted logger
				if (LoggerCache.ContainsKey(this.loggerName))
				{
					this.log = LoggerCache[this.loggerName];
				}
				
				else
				{
					// create the logger
					this.log = LogManager.GetLogger(this.loggerName);
					
					if (this.Config == null)
					{
						XmlConfigurator.Configure();
					}
					
					else
					{
						XmlConfigurator.Configure( this.Config );
					}
					
					LoggerCache.Add(this.loggerName, this.log);
				}
			}
			
			return this.log;
		}
		
		/// <summary>
		/// The name of the logger.
		/// </summary>
		private string loggerName = String.Empty;
		
		/// <summary>
		/// Cache of previosly used loggers
		/// </summary>
		private static Dictionary<string, ILog> LoggerCache = new Dictionary<string, ILog>();
		#endregion Logger specific
		
		/// <summary>
		/// Define which Logger/Appender to use
		/// </summary>
		public AppenderConfigurations Use()
		{
			return new AppenderConfigurations(this);
		}
		
		/// <summary>
		/// Add a <logger/> section to the current config.
		/// 
		/// You can filter your own loggers - code using Log.[level](...) - by adding
		/// a logger with a name that begins with the namespace of the class/classes that you
		/// wish to configure.
		/// 
		/// Like Log4Net, if you add a logger filter for "TopNamespace", you automatically filter
		/// "TopNamespace.SubNamespace".
		/// </summary>
		/// <returns>
		/// The log implementation
		/// </returns>
		/// <param name="loggerName">
		/// The name of the logger
		/// </param>
		/// <param name="level">
		/// The log level of the logger
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if this method is called before a Config exists.
		/// </exception>
		public LogImplementation AddLogger(string loggerName, string level)
		{
			if (this.Config == null)
			{
				throw new InvalidOperationException("You must call Log.Use().<appender>(..) before calling AddLogger(..)");
			}
			
			XmlElement newLogger = this.Config.OwnerDocument.CreateElement("logger");
			newLogger.SetAttribute("name", loggerName);
			
			XmlElement newLoggerLevel = this.Config.OwnerDocument.CreateElement("level");
			newLoggerLevel.SetAttribute("value", level);
			
			newLogger.AppendChild(newLoggerLevel);
			this.Config.AppendChild(newLogger);
			
			return this;
		}
		
		/// <summary>
		/// Gets or sets the config.
		/// </summary>
		internal XmlElement Config { get; set; }
		
		#region Set caller in context
		/// <summary>
		/// Set the caller variable in the log-context
		/// </summary>
		private void SetCallerInContext()
		{
			string caller = String.Empty;
			string callerClass = String.Empty;
			string callerName = String.Empty;
			string fullName = String.Empty;;
			string callerNamespace = String.Empty;			

			LogTools.FindFrame(out caller, out callerClass, out callerName, out fullName, out callerNamespace);
			
			// make sure we create the correct logger
			this.loggerName = callerNamespace;
			
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
		public void Trace()
		{
			this.Trace(String.Empty);
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
		public void Trace(string format, params object[] arg)
		{
			this.Trace(String.Format(format, arg));
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
		public void Trace(string message)
		{
			this.ToLog(EntrySeverity.Trace, message);
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
		public void Debug(string format, params object[] arg)
		{
			this.Debug(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write a debug message to log
		/// </summary>
		/// <param name="message">
		/// The debug message
		/// </param>
		public void Debug(string message)
		{
			this.ToLog(EntrySeverity.Debug, message);
		}
		
		/// <summary>
		/// Write a debug message with "column = value" from the given NameValueCollection
		/// </summary>
		/// <param name="row">
		/// A <see cref="DataRow"/>
		/// </param>
		public void Debug(DataRow row)
		{
			StringBuilder sb = new StringBuilder();
			
			DataColumnCollection cols = row.Table.Columns;
			sb.AppendLine();
			for (int i=0; i<cols.Count; i++)
			{
				sb.AppendFormat("{0} = '{1}'{2}", cols[i].ColumnName, row[cols[i].ColumnName], Environment.NewLine);
			}
			
			this.Debug(sb.ToString());
			
			sb = null;
		}
		
		/// <summary>
		/// Write a debug message with all the key-value-pairs in the given NameValueCollection
		/// </summary>
		/// <param name="data">
		/// The <see cref="NameValueCollection"/> to output
		/// </param>
		public void Debug(NameValueCollection data)
		{
			StringBuilder sb = new StringBuilder(data.Count);
			
			string[] keys = data.AllKeys;
			foreach (string s in keys)
			{
				sb.AppendFormat("data[{0}] = {1}{2}", s, data[s], Environment.NewLine);
			}
			
			this.Debug(sb.ToString());
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
		public void Information(string format, params object[] arg)
		{
			this.Information(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write an information message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public void Information(string message)
		{
			this.ToLog(EntrySeverity.Information, message);
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
		public void Warning(string format, params object[] arg)
		{
			this.Warning(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write a warning message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public void Warning(string message)
		{
			this.ToLog(EntrySeverity.Warning, message);
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
		public void Error(string format, params object[] arg)
		{
			this.Error(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write an error message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public void Error(string message)
		{
			this.ToLog(EntrySeverity.Error, message);
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
		public void Fatal(string format, params object[] arg)
		{
			this.Fatal(String.Format(format, arg));
		}
		
		/// <summary>
		/// Write a fatal message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public void Fatal(string message)
		{
			this.ToLog(EntrySeverity.Fatal, message);
		}
		#endregion Fatal
		
		#region To log
		/// <summary>
		/// Send a log message to the log
		/// </summary>
		/// <param name="severity">
		/// The log entry severity
		/// </param>
		/// <param name="message">
		/// The message
		/// </param>
		public void ToLog(EntrySeverity severity, string message)
		{
			this.SetCallerInContext();
			
			switch (severity)
			{
				case EntrySeverity.Trace:
				case EntrySeverity.Debug:
				{
					this.GetLogger().Debug(message);
					break;
				}
					
				case EntrySeverity.Information:
				{
					this.GetLogger().Info(message);
					break;
				}
					
				case EntrySeverity.Warning:
				{
					this.GetLogger().Warn(message);
					break;
				}
					
				case EntrySeverity.Error:
				{
					this.GetLogger().Error(message);
					break;
				}
					
				case EntrySeverity.Fatal:
				{
					this.GetLogger().Fatal(message);
					break;
				}
			}
		}
		#endregion To log
	}
}