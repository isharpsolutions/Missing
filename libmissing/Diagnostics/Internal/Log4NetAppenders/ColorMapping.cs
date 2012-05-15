using System;
using log4net.Core;

namespace Missing.Diagnostics.Internal.Log4NetAppenders
{
	/// <summary>
	/// A mapping between log level and color
	/// </summary>
	public class ColorMapping : IOptionHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Diagnostics.Log4NetAppenders.ColorMapping"/> class.
		/// </summary>
		public ColorMapping()
		{
			this.level = String.Empty;
		}
		
		/// <summary>
		/// Get/set the log level for which this mapping
		/// applies
		/// </summary>
		public string level { get; set; }
		
		/// <summary>
		/// DO NOT USE THIS... it is only here to
		/// satisfy log4net config parser
		/// 
		/// Use <see cref="ColorMapping.ActualForeground"/> instead
		/// </summary>
		public string foreground { get; set; }
		
		/// <summary>
		/// DO NOT USE THIS... it is only here to
		/// satisfy log4net config parser
		/// 
		/// Use <see cref="ColorMapping.ActualBackground"/> instead
		/// </summary>
		public string background { get; set; }
		
		/// <summary>
		/// Get/set the foreground color to use
		/// </summary>
		public ConsoleColor ActualForeground { get; set; }
		
		/// <summary>
		/// Get/set the background color to use
		/// </summary>
		public ConsoleColor ActualBackground { get; set; }
		
		/// <summary>
		/// Determines whether this instance has set a foreground color
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance has set a foreground color; otherwise, <c>false</c>.
		/// </returns>
		public bool HasForeground()
		{
			return !this.foreground.Equals(String.Empty);
		}
		
		/// <summary>
		/// Determines whether this instance has set a background color
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance has set a background color; otherwise, <c>false</c>.
		/// </returns>
		public bool HasBackground()
		{
			return !this.background.Equals(String.Empty);
		}
		
		#region IOptionHandler implementation
		/// <summary>
		/// Activates the options.
		/// </summary>
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

