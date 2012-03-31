using System;
using System.Xml;
using Missing.Diagnostics.Log4NetAppenders;

namespace Missing.Diagnostics
{
	/// <summary>
	/// Holds properties containing hardcoded default configurations
	/// </summary>
	public static class Log4NetConfigurations
	{
		#region Simple console
		/// <summary>
		/// Simple console configuration.
		/// 
		/// The format is:
		/// [level][date][caller] message
		/// </summary>
		/// <returns>
		/// Configuration
		/// </returns>
		public static XmlElement GetSimpleConsole()
		{
			XmlDocument doc = new XmlDocument();
			
			XmlElement wrapper = doc.CreateElement("log4net");
			wrapper.SetAttribute("debug", "False");
			
			XmlElement appender = doc.CreateElement("appender");
			wrapper.AppendChild(appender);
			appender.SetAttribute("name", "ConsoleAppender");
			appender.SetAttribute("type", "log4net.Appender.ConsoleAppender");
			
			XmlElement layout = doc.CreateElement("layout");
			appender.AppendChild(layout);
			layout.SetAttribute("type", "log4net.Layout.PatternLayout");
			
			XmlElement conversionPattern = doc.CreateElement("conversionPattern");
			layout.AppendChild(conversionPattern);
			conversionPattern.SetAttribute("value", "[%-5level][%date][%property{%%CALLER%%}] %message%newline".Replace("%%CALLER%%", Log.CallerContextName));
			
			//
			// root
			//
			XmlElement root = doc.CreateElement("root");
			wrapper.AppendChild(root);
			
			XmlElement priority = doc.CreateElement("priority");
			root.AppendChild(priority);
			priority.SetAttribute("value", "DEBUG");
			
			XmlElement appenderRef = doc.CreateElement("appender-ref");
			root.AppendChild(appenderRef);
			appenderRef.SetAttribute("ref", "ConsoleAppender");
			
			return wrapper;
		}
		#endregion Simple console
		
		#region Colored console appender
		/// <summary>
		/// Colored version of "Simple console configuration".
		/// 
		/// The format is:
		/// [level][date][caller] message
		/// </summary>
		/// <returns>
		/// Configuration
		/// </returns>
		public static XmlElement GetSimpleConsoleColored()
		{
			XmlDocument doc = new XmlDocument();
			
			XmlElement wrapper = doc.CreateElement("log4net");
			wrapper.SetAttribute("debug", "False");
			
			XmlElement appender = doc.CreateElement("appender");
			wrapper.AppendChild(appender);
			appender.SetAttribute("name", "ColoredConsoleAppender");
			appender.SetAttribute("type", typeof(ManagedColoredConsoleAppender).FullName);
			
			XmlElement layout = doc.CreateElement("layout");
			appender.AppendChild(layout);
			layout.SetAttribute("type", "log4net.Layout.PatternLayout");
			
			XmlElement conversionPattern = doc.CreateElement("conversionPattern");
			layout.AppendChild(conversionPattern);
			conversionPattern.SetAttribute("value", "[%-5level][%date][%property{%%CALLER%%}] %message".Replace("%%CALLER%%", Log.CallerContextName));
			
			//
			// color mappings
			//
			appender.AppendChild(GetColorMapping(doc, "DEBUG", ConsoleColor.DarkBlue));
			appender.AppendChild(GetColorMapping(doc, "INFO", ConsoleColor.Blue));
			appender.AppendChild(GetColorMapping(doc, "WARN", ConsoleColor.Yellow));
			appender.AppendChild(GetColorMapping(doc, "ERROR", ConsoleColor.Red));
			appender.AppendChild(GetColorMapping(doc, "FATAL", ConsoleColor.Black, ConsoleColor.Red));
			
			
			//
			// root
			//
			XmlElement root = doc.CreateElement("root");
			wrapper.AppendChild(root);
			
			XmlElement priority = doc.CreateElement("priority");
			root.AppendChild(priority);
			priority.SetAttribute("value", "DEBUG");
			
			XmlElement appenderRef = doc.CreateElement("appender-ref");
			root.AppendChild(appenderRef);
			appenderRef.SetAttribute("ref", "ColoredConsoleAppender");
			
			return wrapper;
		}
		
		/// <summary>
		/// Get color mapping xml element without background color
		/// </summary>
		/// <returns>
		/// The color mapping.
		/// </returns>
		/// <param name="doc">
		/// Document.
		/// </param>
		/// <param name="level">
		/// Level.
		/// </param>
		/// <param name="foregroundColor">
		/// Foreground color.
		/// </param>
		private static XmlElement GetColorMapping(XmlDocument doc, string level, ConsoleColor foregroundColor)
		{
			return GetColorMapping(doc, level, foregroundColor.ToString(), String.Empty);
		}
		
		/// <summary>
		/// Get color mapping xml element
		/// </summary>
		/// <returns>
		/// The color mapping.
		/// </returns>
		/// <param name="doc">
		/// Document.
		/// </param>
		/// <param name="level">
		/// Level.
		/// </param>
		/// <param name="foregroundColor">
		/// Foreground color.
		/// </param>
		/// <param name="backgroundColor">
		/// Background color.
		/// </param>
		private static XmlElement GetColorMapping(XmlDocument doc, string level, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
		{
			return GetColorMapping(doc, level, foregroundColor.ToString(), backgroundColor.ToString());
		}
		
		/// <summary>
		/// Get color mapping xml element
		/// </summary>
		/// <returns>
		/// The color mapping.
		/// </returns>
		/// <param name="doc">
		/// Document.
		/// </param>
		/// <param name="level">
		/// Level.
		/// </param>
		/// <param name="foregroundColor">
		/// Foreground color.
		/// </param>
		/// <param name="backgroundColor">
		/// Background color.
		/// </param>
		private static XmlElement GetColorMapping(XmlDocument doc, string level, string foregroundColor, string backgroundColor)
		{
			XmlElement mapping = doc.CreateElement("mapping");
			
			XmlElement mappingLevel = doc.CreateElement("level");
			mappingLevel.SetAttribute("value", level);
			
			XmlElement foreground = doc.CreateElement("foreground");
			foreground.SetAttribute("value", foregroundColor);
			
			XmlElement background = doc.CreateElement("background");
			background.SetAttribute("value", backgroundColor);
			
			mapping.AppendChild(foreground);
			mapping.AppendChild(background);
			mapping.AppendChild(mappingLevel);
			
			return mapping;
		}
		#endregion Colored console appender
		
		#region AdoNet appender
		/// <summary>
		/// MySql AdoNetAppender with "caller"
		/// </summary>
		/// <returns>
		/// Configuration
		/// </returns>
		/// <param name="tableName">
		/// Table name.
		/// </param>
		/// <param name="host">
		/// MySql host
		/// </param>
		/// <param name="schema">
		/// Schema.
		/// </param>
		/// <param name="user">
		/// MySql user
		/// </param>
		/// <param name="password">
		/// Password
		/// </param>
		/// <remarks>
		/// 	<para>You should use the following create statement for the table</para>
		/// 	<code>
		/// 		CREATE TABLE `mysqladonetappender` (
		/// 		  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
		///			  `DateTime` datetime DEFAULT NULL,
		///			  `Level` text,
		/// 		  `Caller` text,
		/// 		  `Message` text,
		/// 		  PRIMARY KEY (`id`)
		/// 		) ENGINE=InnoDB DEFAULT CHARSET=utf8
		/// 	</code>
		/// </remarks>
		public static XmlElement GetMySqlAdoNetAppender(string tableName, string host, string schema, string user, string password)
		{
			XmlDocument doc = new XmlDocument();
			
			XmlElement wrapper = doc.CreateElement("log4net");
			wrapper.SetAttribute("debug", "False");
			
			XmlElement appender = doc.CreateElement("appender");
			appender.SetAttribute("name", "AdoNetAppender");
			appender.SetAttribute("type", "log4net.Appender.AdoNetAppender");
			
			XmlElement connectionType = doc.CreateElement("connectionType");
			connectionType.SetAttribute("value", "MySql.Data.MySqlClient.MySqlConnection, MySql.Data, Version=6.2.3.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d");
			
			XmlElement connectionString = doc.CreateElement("connectionString");
			connectionString.SetAttribute("value", String.Format("data source={0};initial catalog={1};integrated security=false;persist security info=True;User ID={2};Password={3}", host, schema, user, password));
			
			XmlElement commandText = doc.CreateElement("commandText");
			commandText.SetAttribute("value", String.Format("insert into `{0}`.`{1}` (`caller`, `message`, `level`, `datetime`) values(?caller, ?message, ?level, ?log_date)", schema, tableName));
			
			XmlElement bufferSize = doc.CreateElement("bufferSize");
			bufferSize.SetAttribute("value", "1");
			
			appender.AppendChild(connectionType);
			appender.AppendChild(connectionString);
			appender.AppendChild(commandText);
			appender.AppendChild(bufferSize);
			
			appender.AppendChild(GetParameterForMySqlAdoNetAppender(doc, "?message", "String", "log4net.Layout.PatternLayout", "%message"));
			appender.AppendChild(GetParameterForMySqlAdoNetAppender(doc, "?level", "String", "log4net.Layout.PatternLayout", "%level"));
			appender.AppendChild(GetParameterForMySqlAdoNetAppender(doc, "?log_date", "DateTime", "log4net.Layout.RawTimeStampLayout", String.Empty));
			appender.AppendChild(GetParameterForMySqlAdoNetAppender(doc, "?caller", "String", "log4net.Layout.PatternLayout", "%property{%%CALLER%%}".Replace("%%CALLER%%", Log.CallerContextName)));
			
			//
			// root
			//
			XmlElement root = doc.CreateElement("root");
			
			XmlElement priority = doc.CreateElement("priority");
			priority.SetAttribute("value", "DEBUG");
			
			XmlElement appenderRef = doc.CreateElement("appender-ref");
			appenderRef.SetAttribute("ref", "AdoNetAppender");
			
			root.AppendChild(priority);
			root.AppendChild(appenderRef);
			
			wrapper.AppendChild(appender);
			wrapper.AppendChild(root);
			
			return wrapper;
		}
		
		/// <summary>
		/// Get a parameter element for the MySqlAdoNetAppender
		/// </summary>
		/// <returns>
		/// A parameter element
		/// </returns>
		/// <param name="doc">
		/// Document.
		/// </param>
		/// <param name="name">
		/// Parameter name
		/// </param>
		/// <param name="dbType">
		/// Database type
		/// </param>
		/// <param name="layout">
		/// Layout type
		/// </param>
		/// <param name="conversionPattern">
		/// Conversion pattern (supply "String.Empty" if you do not need a conversion pattern)
		/// </param>
		private static XmlElement GetParameterForMySqlAdoNetAppender(XmlDocument doc, string name, string dbType, string layout, string conversionPattern)
		{
			XmlElement param = doc.CreateElement("parameter");
			
			XmlElement parameterName = doc.CreateElement("parameterName");
			parameterName.SetAttribute("value", name);
			
			XmlElement dbTypeElm = doc.CreateElement("dbType");
			dbTypeElm.SetAttribute("value", dbType);
			
			XmlElement layoutElm = doc.CreateElement("layout");
			layoutElm.SetAttribute("type", layout);
			
			if (!conversionPattern.Equals(String.Empty))
			{
				XmlElement conversionPatternElm = doc.CreateElement("conversionPattern");
				conversionPatternElm.SetAttribute("value", conversionPattern);
				
				layoutElm.AppendChild(conversionPatternElm);
			}
			
			param.AppendChild(parameterName);
			param.AppendChild(dbTypeElm);
			param.AppendChild(layoutElm);
			
			return param;
		}
		#endregion AdoNet appender
	}
}