using System;

namespace Missing.Diagnostics.Configurations
{
	/// <summary>
	/// Holds appender configurations.
	/// 
	/// This class is tightly bound to <see cref="LogImplementation"/> as
	/// is not meant to be used in any other context.
	/// </summary>
	public partial class AppenderConfigurations
	{
		public AppenderConfigurations(LogImplementation impl)
		{
			this.Implementation = impl;
		}
		
		internal LogImplementation Implementation { get; set; }
	}
}