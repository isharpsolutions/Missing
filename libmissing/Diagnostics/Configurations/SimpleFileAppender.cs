using System;
using Missing.Diagnostics.Internal;
using System.Xml;

namespace Missing.Diagnostics.Configurations
{
	public partial class AppenderConfigurations
	{
		/// <summary>
		/// A simple FileAppender configuration using unicode encoding
		/// and with default file locking behavior.
		/// 
		/// The format is:
		/// [level][date][caller] message
		/// </summary>
		/// <returns>
		/// The log implementation
		/// </returns>
		public LogImplementation SimpleFileAppender(string filename)
		{
			XmlDocument doc = new XmlDocument();
			
			XmlElement wrapper = doc.CreateElement("log4net");
			wrapper.SetAttribute("debug", "False");
			
			XmlElement appender = doc.CreateElement("appender");
			wrapper.AppendChild(appender);
			appender.SetAttribute("name", "SimpleFileAppender");
			appender.SetAttribute("type", "log4net.Appender.FileAppender");

			XmlElement file = doc.CreateElement("file");
			file.SetAttribute("value", filename);
			appender.AppendChild(file);

			XmlElement appendToFile = doc.CreateElement("appendToFile");
			appendToFile.SetAttribute("value", "true");
			appender.AppendChild(appendToFile);

			XmlElement encoding = doc.CreateElement("encoding");
			encoding.SetAttribute("value", "unicodeFFFE");
			appender.AppendChild(encoding);

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
			appenderRef.SetAttribute("ref", "SimpleFileAppender");
			
			this.Implementation.Config = wrapper;

			return this.Implementation;
		}
	}
}