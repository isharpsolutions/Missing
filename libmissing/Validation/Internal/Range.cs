using System;

namespace Missing.Validation.Internal
{
	/// <summary>
	/// A range.
	/// 
	/// This is used internally for validating number types (int, long, float etc)
	/// </summary>
	internal class Range<T> : IRange where T : struct
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Internal.Range`1"/> class.
		/// </summary>
		public Range()
		{
		}
		
		/// <summary>
		/// Get/set minimum accepted value
		/// </summary>
		public T Min { get; set; }
		
		/// <summary>
		/// Get/set maximum accepted value
		/// </summary>
		public T Max { get; set; }
	}
}