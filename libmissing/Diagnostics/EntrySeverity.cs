using System;

namespace Missing.Diagnostics
{
	/// <summary>
	/// Defines how critical a log entry is
	/// </summary>
	public enum EntrySeverity
	{
		/// <summary>
		/// The entry is a fatal error
		/// </summary>
		Fatal = 0,
		
		/// <summary>
        /// The entry is an error
        /// </summary>
        Error = 1,

        /// <summary>
        /// The entry in an warning
        /// </summary>
        Warning = 2,

        /// <summary>
        /// The entry is information
        /// </summary>
        Information = 3,

        /// <summary>
        /// The entry is Debug information
        /// </summary>
        Debug = 4,
	
		/// <summary>
		/// The entry is trace information 
		/// </summary>
		Trace = 5
	}
}