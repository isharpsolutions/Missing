using System;
using log4net.Appender;
using log4net.Core;
using System.Collections.Generic;

namespace Missing.Diagnostics.Log4NetAppenders
{
	public class ManagedColoredConsoleAppender : AppenderSkeleton
	{
		public ManagedColoredConsoleAppender() : base()
		{
		}
		
		private Dictionary<string, ColorMapping> colorMappings = new Dictionary<string, ColorMapping>();
		
		public void AddMapping(ColorMapping mapping)
		{
			this.colorMappings.Add(mapping.level, mapping);
		}
		
		#region implemented abstract members of log4net.Appender.AppenderSkeleton
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.colorMappings.ContainsKey(loggingEvent.Level.Name))
			{
				ColorMapping colors = this.colorMappings[loggingEvent.Level.Name];
				Console.ForegroundColor = colors.ActualForeground;
				
				if (colors.HasBackground())
				{
					Console.BackgroundColor = colors.ActualBackground;
				}
			}
			
			// write the message
			Console.Write(base.RenderLoggingEvent(loggingEvent));
			
			// reset colors BEFORE newline to avoid background color spill
			Console.ResetColor();
			
			Console.WriteLine();
			Console.Out.Flush();
		}
		#endregion
	}
	
	public class ColorMapping : IOptionHandler
	{
		public ColorMapping()
		{
			this.level = String.Empty;
		}
		
		public string level { get; set; }
		public string foreground { get; set; }
		public string background { get; set; }
		
		public ConsoleColor ActualForeground { get; set; }
		public ConsoleColor ActualBackground { get; set; }
		
		public bool HasBackground()
		{
			return !this.background.Equals(String.Empty);
		}
		
		#region IOptionHandler implementation
		public void ActivateOptions()
		{
			ConsoleColor tmp = ConsoleColor.Black;
			
			if (Enum.TryParse<ConsoleColor>(this.foreground, true, out tmp))
			{
				this.ActualForeground = tmp;
			}
			
			if (Enum.TryParse<ConsoleColor>(this.background, true, out tmp))
			{
				this.ActualBackground = tmp;
			}
		}
		#endregion
	}
	
}

