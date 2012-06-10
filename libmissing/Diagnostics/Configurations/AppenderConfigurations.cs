using System;
using Missing.Diagnostics.Internal;

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
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Diagnostics.Configurations.AppenderConfigurations"/> class.
		/// </summary>
		/// <param name="impl">
		/// The implementation to use
		/// </param>
		public AppenderConfigurations(LogImplementation impl)
		{
			this.Implementation = impl;
		}
		
		internal LogImplementation Implementation { get; set; }
	}
}