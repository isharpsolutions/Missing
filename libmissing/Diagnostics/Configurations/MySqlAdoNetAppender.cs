using System;
using System.Xml;

namespace Missing.Diagnostics.Configurations
{
	public partial class AppenderConfigurations
	{
		/// <summary>
		/// MySql AdoNetAppender with "caller"
		/// </summary>
		/// <returns>
		/// The log implementation
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
		public LogImplementation GetMySqlAdoNetAppender(string tableName, string host, string schema, string user, string password)
		{
			XmlDocument doc = new XmlDocument();
			
			XmlElement wrapper = doc.CreateElement("log4net");
			wrapper.SetAttribute("debug", "False");
			
			XmlElement appender = doc.CreateElement("appender");
			appender.SetAttribute("name", "AdoNetAppender");
			appender.SetAttribute("type", "log4net.Appender.AdoNetAppender");
			
			XmlElement connectionType = doc.CreateElement("connectionType");
			connectionType.SetAttribute("value", "MySql.Data.MySqlClient.MySqlConnection, MySql.Data, Culture=neutral, PublicKeyToken=c5687fc88969c44d");
			
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
			
			appender.AppendChild(this.GetParameterForMySqlAdoNetAppender(doc, "?message", "String", "log4net.Layout.PatternLayout", "%message"));
			appender.AppendChild(this.GetParameterForMySqlAdoNetAppender(doc, "?level", "String", "log4net.Layout.PatternLayout", "%level"));
			appender.AppendChild(this.GetParameterForMySqlAdoNetAppender(doc, "?log_date", "DateTime", "log4net.Layout.RawTimeStampLayout", String.Empty));
			appender.AppendChild(this.GetParameterForMySqlAdoNetAppender(doc, "?caller", "String", "log4net.Layout.PatternLayout", "%property{%%CALLER%%}".Replace("%%CALLER%%", Log.CallerContextName)));
			
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
			
			this.Implementation.Config = wrapper;
			return this.Implementation;
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
		private XmlElement GetParameterForMySqlAdoNetAppender(XmlDocument doc, string name, string dbType, string layout, string conversionPattern)
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
	}
}