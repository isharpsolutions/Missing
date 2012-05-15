using System;
using System.Collections.Generic;
using log4net.Appender;
using log4net.Core;

namespace Missing.Diagnostics.Internal.Log4NetAppenders
{
	/// <summary>
	/// Appends logging events to the console with color
	/// </summary>
	/// <remarks>
	/// <para>
	/// When configuring the colored console appender, mapping should be
	/// specified to map a logging level to a color. For example:
	/// </para>
	/// <code lang="XML" escaped="true">
	/// <mapping>
	///     <level value="ERROR" />
	///     <foreground value="White" />
	///     <background value="Red" />
	/// </mapping>
	/// <mapping>
	///     <level value="DEBUG" />
	///     <background value="Green" />
	/// </mapping>
	/// </code>
	/// <para>
	/// The Level is the standard log4net logging level and ForeColor and BackColor can be any
	/// color from <see cref="ConsoleColor"/>
	/// </para>
	/// </remarks>
	public class ManagedColoredConsoleAppender : AppenderSkeleton
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Diagnostics.Log4NetAppenders.ManagedColoredConsoleAppender"/> class.
		/// </summary>
		public ManagedColoredConsoleAppender() : base()
		{
		}
		
		#region Color mappings
		/// <summary>
		/// The color mappings
		/// </summary>
		private Dictionary<string, ColorMapping> colorMappings = new Dictionary<string, ColorMapping>();
		
		/// <summary>
		/// Add a color mapping
		/// </summary>
		/// <param name="mapping">
		/// The mapping
		/// </param>
		public void AddMapping(ColorMapping mapping)
		{
			this.colorMappings.Add(mapping.level, mapping);
		}
		#endregion Color mappings
		
		#region implemented abstract members of log4net.Appender.AppenderSkeleton
		/// <summary>
		/// Append the specified loggingEvent.
		/// </summary>
		/// <param name="loggingEvent">
		/// Logging event.
		/// </param>
		protected override void Append(LoggingEvent loggingEvent)
		{
			//
			// set colors
			//
			if (this.colorMappings.ContainsKey(loggingEvent.Level.Name))
			{
				ColorMapping colors = this.colorMappings[loggingEvent.Level.Name];
				Console.ForegroundColor = colors.ActualForeground;
				
				if (colors.HasForeground())
				{
					Console.ForegroundColor = colors.ActualForeground;
				}
				
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
}