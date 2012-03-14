using System;

namespace Missing.Validation
{
	/// <summary>
	/// Information about a validation error
	/// </summary>
	public class ValidationError
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.ValidationError"/> class.
		/// </summary>
		public ValidationError() : this(String.Empty, String.Empty)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.ValidationError"/> class.
		/// </summary>
		/// <param name="propertyName">
		/// The name of the property that contains an error
		/// </param>
		/// <param name="message">
		/// A descriptive message of what the error is
		/// </param>
		public ValidationError(string propertyName, string message)
		{
			this.PropertyName = propertyName;
			this.Message = message;
		}
		
		/// <summary>
		/// Get/set the name of the property that contains an error
		/// </summary>
		public string PropertyName { get; set; }
		
		/// <summary>
		/// Get/set a descriptive message of what the error is
		/// </summary>
		public string Message { get; set; }
	}
}

