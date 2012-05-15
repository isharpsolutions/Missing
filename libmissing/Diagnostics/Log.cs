using System;
using log4net;
using log4net.Config;
using System.Xml;
using System.Data;
using System.Collections.Specialized;
using System.Text;
using Missing.Diagnostics.Configurations;

namespace Missing.Diagnostics
{
	/// <summary>
	/// Convenience methods regarding logging
	/// 
	/// You can either configure the logger using
	/// classic Log4Net sections in you app/web.config
	/// </summary>
	/// <example>
	/// Using classic app/web.config configuration.
	/// 
	/// <code>
	/// Log.Trace();
	/// </code>
	/// </example>
	/// <example>
	/// Using in-code configuration.
	/// 
	/// <code>
	/// Log.Use().SimpleConsole();
	/// 
	/// Log.Trace();
	/// </code>
	/// </example>
	public static class Log
	{
		/// <summary>
		/// The name of the context property containing
		/// the name of the caller
		/// </summary>
		public static readonly string CallerContextName = "missing.caller";
		
		/// <summary>
		/// The underlying implementation (needed to support a DSL)
		/// </summary>
		private static LogImplementation impl = new LogImplementation();
		
		/// <summary>
		/// Start the configuration DSL
		/// </summary>
		public static AppenderConfigurations Use()
		{
			return impl.Use();
		}
		
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
			impl.Trace();
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
			impl.Trace(format, arg);
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
			impl.Trace(message);
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
			impl.Debug(format, arg);
		}
		
		/// <summary>
		/// Write a debug message to log
		/// </summary>
		/// <param name="message">
		/// The debug message
		/// </param>
		public static void Debug(string message)
		{
			impl.Debug(message);
		}
		
		/// <summary>
		/// Write a debug message with "column = value" from the given NameValueCollection
		/// </summary>
		/// <param name="row">
		/// A <see cref="DataRow"/>
		/// </param>
		public static void Debug(DataRow row)
		{
			impl.Debug(row);
		}
		
		/// <summary>
		/// Write a debug message with all the key-value-pairs in the given NameValueCollection
		/// </summary>
		/// <param name="data">
		/// The <see cref="NameValueCollection"/> to output
		/// </param>
		public static void Debug(NameValueCollection data)
		{
			impl.Debug(data);
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
			impl.Information(format, arg);
		}
		
		/// <summary>
		/// Write an information message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public static void Information(string message)
		{
			impl.Information(message);
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
			impl.Warning(format, arg);
		}
		
		/// <summary>
		/// Write a warning message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public static void Warning(string message)
		{
			impl.Warning(message);
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
			impl.Error(format, arg);
		}
		
		/// <summary>
		/// Write an error message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public static void Error(string message)
		{
			impl.Error(message);
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
			impl.Fatal(format, arg);
		}
		
		/// <summary>
		/// Write a fatal message to log
		/// </summary>
		/// <param name="message">
		/// The message
		/// </param>
		public static void Fatal(string message)
		{
			impl.Fatal(message);
		}
		#endregion Fatal
	}
}