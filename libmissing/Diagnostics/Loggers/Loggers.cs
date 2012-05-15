using System;

namespace Missing.Diagnostics.Loggers
{
	/// <summary>
	/// Holds appender configurations.
	/// 
	/// This class is tightly bound to <see cref="LogImplementation"/> as
	/// is not meant to be used in any other context.
	/// </summary>
	public partial class Loggers
	{
		public Loggers(LogImplementation impl)
		{
			this.Implementation = impl;
		}
		
		internal LogImplementation Implementation { get; set; }
	}
}