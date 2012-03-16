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
		/// <param name="propertyPath">
		/// The path of the field that contains an error
		/// </param>
		/// <param name="message">
		/// A descriptive message of what the error is
		/// </param>
		public ValidationError(string propertyPath, string message)
		{
			this.PropertyPath = propertyPath;
			this.Message = message;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.ValidationError"/> class.
		/// </summary>
		/// <param name="propertyPath">
		/// The path of the field that contains an error
		/// </param>
		/// <param name="messageFormat">
		/// Format of descriptive message
		/// </param>
		/// <param name="messageArgs">
		/// Arguments for descriptive message format
		/// </param>
		public ValidationError(string propertyPath, string messageFormat, params object[] messageArgs) : this(propertyPath, String.Format(messageFormat, messageArgs))
		{
		}
		
		/// <summary>
		/// Get/set the path of the property that contains an error
		/// </summary>
		public string PropertyPath { get; set; }
		
		/// <summary>
		/// Get/set a descriptive message of what the error is
		/// </summary>
		public string Message { get; set; }
		
		/// <summary>
		/// Get/set the full name of the enforcer that noticed the error
		/// </summary>
		public string EnforcerName { get; set; }
	}
}

