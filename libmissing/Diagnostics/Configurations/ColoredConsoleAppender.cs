using System;
using System.Xml;
using Missing.Diagnostics.Log4NetAppenders;

namespace Missing.Diagnostics.Configurations
{
	public partial class AppenderConfigurations
	{
		/// <summary>
		/// Colored version of "Simple console configuration".
		/// 
		/// The format is:
		/// [level][date][caller] message
		/// </summary>
		/// <returns>
		/// The log implementation
		/// </returns>
		public LogImplementation SimpleConsoleColored()
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
			appender.AppendChild(this.GetColorMapping(doc, "DEBUG", ConsoleColor.DarkBlue));
			appender.AppendChild(this.GetColorMapping(doc, "INFO", ConsoleColor.Blue));
			appender.AppendChild(this.GetColorMapping(doc, "WARN", ConsoleColor.Yellow));
			appender.AppendChild(this.GetColorMapping(doc, "ERROR", ConsoleColor.Red));
			appender.AppendChild(this.GetColorMapping(doc, "FATAL", ConsoleColor.Black, ConsoleColor.Red));
			
			
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
			
			this.Implementation.Config = wrapper;
			
			return this.Implementation;
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
		private XmlElement GetColorMapping(XmlDocument doc, string level, ConsoleColor foregroundColor)
		{
			return this.GetColorMapping(doc, level, foregroundColor.ToString(), String.Empty);
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
		private XmlElement GetColorMapping(XmlDocument doc, string level, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
		{
			return this.GetColorMapping(doc, level, foregroundColor.ToString(), backgroundColor.ToString());
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
		private XmlElement GetColorMapping(XmlDocument doc, string level, string foregroundColor, string backgroundColor)
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
	}
}