using System;

namespace Missing.Validation.Internal
{
	/// <summary>
	/// The score for a given type
	/// </summary>
	internal class TypeMatchScore
	{
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		public Type Type { get; set; }
		
		/// <summary>
		/// Gets or sets the score.
		/// </summary>
		public int Score { get; set; }
	}
}