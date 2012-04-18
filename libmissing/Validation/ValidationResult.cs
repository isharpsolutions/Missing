using System;

namespace Missing.Validation
{
	/// <summary>
	/// The overall result of a validation on a given model
	/// </summary>
	public class ValidationResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.ValidationResult"/> class.
		/// </summary>
		public ValidationResult()
		{
			this.Errors = new ValidationErrorCollection();
		}
		
		/// <summary>
		/// Get/set the full set of errors
		/// </summary>
		public ValidationErrorCollection Errors { get; set; }
		
		/// <summary>
		/// Determines whether the validation yielded any errors
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance has errors; otherwise, <c>false</c>.
		/// </returns>
		public bool HasErrors()
		{
			return this.Errors.Count > 0;
		}
		
		/// <summary>
		/// Merge another validation result into this one
		/// </summary>
		/// <param name="other">
		/// Other.
		/// </param>
		public void Merge(ValidationResult other)
		{
			foreach (ValidationError err in other.Errors)
			{
				this.Errors.Add(err);
			}
		}
	}
}