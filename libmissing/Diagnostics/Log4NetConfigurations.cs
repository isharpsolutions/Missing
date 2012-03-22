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
		public static XmlElement SimpleConsole
		{
			get
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
		}
		#endregion Simple console
		
		private static XmlElement GetColorMapping(XmlDocument doc, string level, ConsoleColor foregroundColor)
		{
			return GetColorMapping(doc, level, foregroundColor.ToString(), String.Empty);
		}
		
		private static XmlElement GetColorMapping(XmlDocument doc, string level, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
		{
			return GetColorMapping(doc, level, foregroundColor.ToString(), backgroundColor.ToString());
		}
		
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
		
		#region Colored console appender
		public static XmlElement ColoredConsoleAppender
		{
			get
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
		}
		#endregion Colored console appender
	}
}