using System;
using System.Xml.Linq;

namespace Missing.Diagnostics.Log4NetConfigurations
{
	public static class Log4NetConfigurations
	{
		/*
		<log4net debug="True">
			<appender name="ConsoleAppender" type="log4net.Appender.ConsoleAppender">
			    <layout type="log4net.Layout.PatternLayout">
			        <conversionPattern value="%date - %property{mycaller} - %message%newline" />
			    </layout>
			</appender>
			<root>
				<priority value="DEBUG" />
				<appender-ref ref="ConsoleAppender" />
			</root>
		</log4net>
		*/
		
		public static XElement SimpleConsole()
		{
			return new XElement("log4net",
			                    new XAttribute("debug", "True"),
			                    new XElement("appender",
			             			new XAttribute("name", "ConsoleAppender"),
			             			new XAttribute("type", "log4net.Appender.ConsoleAppender"),
			             			new XElement("layout",
			             				new XAttribute("type", "log4net.Layout.PatternLayout"),
			             				new XElement("conversionPattern",
			             					new XAttribute("value", "%date - %property{mycaller} - %message%newline")
			             				)
			             			)
			             		),
			                    new XElement("root",
			             			new XElement("priority",
			             				new XAttribute("value", "DEBUG")
			             			),
			             			new XElement("appender-ref",
			             				new XAttribute("ref", "ConsoleAppender")
			             			)
			             		)
			);
		}
	}
}