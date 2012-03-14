using System;
using System.Collections.ObjectModel;

namespace Missing.Validation
{
	/// <summary>
	/// A collection of validation errors
	/// </summary>
	public class ValidationErrorCollection : Collection<ValidationError>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.ValidationErrorCollection"/> class.
		/// </summary>
		public ValidationErrorCollection() : base()
		{
		}
	}
}