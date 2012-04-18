using System;
using Missing.Reflection;

namespace Missing.Validation.Validators
{
	/// <summary>
	/// Defines a validator
	/// </summary>
	internal interface IValidator
	{
		/// <summary>
		/// Validates a specific field based on a field specification
		/// and an instance of the model
		/// </summary>
		/// <returns>
		/// An instance of <see cref="ValidationError"/> if field's value
		/// in the model does not correspond with the field specification.
		/// If the value is valid, then default(ValidationError) is returned.
		/// </returns>
		/// <param name="field">
		/// The field specification
		/// </param>
		/// <param name="input">
		/// The input model
		/// </param>
		/// <param name="pd">
		/// Property data for the field
		/// </param>
		/// <typeparam name="T">
		/// The model type
		/// </typeparam>
		ValidationError ValidateField<T>(FieldSpecification field, T input, PropertyData pd) where T : class;
	}
}