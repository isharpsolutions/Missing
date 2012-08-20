using System;

namespace Missing.Validation
{
	/// <summary>
	/// This exception is designed for cases where the throwing code is
	/// unable to find a validation specification.
	/// </summary>
	[Serializable]
	public class UnableToFindValidationSpecificationException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UnableToFindValidationSpecificationException"/> class
		/// </summary>
		public UnableToFindValidationSpecificationException ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UnableToFindValidationSpecificationException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public UnableToFindValidationSpecificationException (string message) : base (message)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UnableToFindValidationSpecificationException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public UnableToFindValidationSpecificationException (string message, Exception inner) : base (message, inner)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UnableToFindValidationSpecificationException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected UnableToFindValidationSpecificationException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context)
		{
		}
	}
}