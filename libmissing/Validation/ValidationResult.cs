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
		
		/// <summary>
		/// Prepend a string to all property paths in the result.
		/// 
		/// This is used when validating IEnumerable fields, to
		/// output the property name with index.
		/// </summary>
		/// <param name="prepend">
		/// The text to prepend. It should NOT have a dot at the end.
		/// </param>
		public void PrependAllPropertyPathsWith(string prepend)
		{
			foreach (ValidationError err in this.Errors)
			{
				err.PropertyPath = String.Format("{0}.{1}", prepend, err.PropertyPath)
										.Replace(
											String.Format(".{0}", Validator.PrimitiveFieldName),
											String.Empty
										);
			}
		}
	}
}