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
		/// <param name="fieldName">
		/// The name of the field that contains an error
		/// </param>
		/// <param name="message">
		/// A descriptive message of what the error is
		/// </param>
		public ValidationError(string fieldName, string message)
		{
			this.FieldName = fieldName;
			this.Message = message;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.ValidationError"/> class.
		/// </summary>
		/// <param name="fieldName">
		/// The name of the field that contains an error
		/// </param>
		/// <param name="messageFormat">
		/// Format of descriptive message
		/// </param>
		/// <param name="messageArgs">
		/// Arguments for descriptive message format
		/// </param>
		public ValidationError(string fieldName, string messageFormat, params object[] messageArgs) : this(fieldName, String.Format(messageFormat, messageArgs))
		{
		}
		
		/// <summary>
		/// Get/set the name of the property that contains an error
		/// </summary>
		public string FieldName { get; set; }
		
		/// <summary>
		/// Get/set a descriptive message of what the error is
		/// </summary>
		public string Message { get; set; }
	}
}

