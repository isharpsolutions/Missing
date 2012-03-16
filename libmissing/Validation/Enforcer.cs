using System;

namespace Missing.Validation
{
	/// <summary>
	/// Base class for all enforcers.
	/// 
	/// An Enforcer is a class that can
	/// check an input against the enforcer's
	/// internal standards.
	/// You could call it a sort of validator.
	/// </summary>
	public abstract class Enforcer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Enforcer"/> class.
		/// </summary>
		public Enforcer()
		{
		}
		
		/// <summary>
		/// Check the specified input
		/// </summary>
		/// <param name="input">
		/// The input to check
		/// </param>
		/// <returns>
		/// A string with details about what is wrong with the input
		/// -or-
		/// String.Empty in case nothing is wrong with the input
		/// </returns>
		public abstract string Check(object input);
	}
}

