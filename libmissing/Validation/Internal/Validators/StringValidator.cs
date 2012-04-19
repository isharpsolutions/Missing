using System;
using Missing.Reflection;

namespace Missing.Validation.Internal.Validators
{
	/// <summary>
	/// Knows how to validate <see cref="System.String"/>
	/// </summary>
	internal class StringValidator : IValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Internal.Validators.StringValidator"/> class.
		/// </summary>
		public StringValidator()
		{
		}

		#region IValidator implementation
		public ValidationError ValidateField<T>(FieldSpecification field, T input, PropertyData pd) where T : class
		{
			return this.ValidatePrimitive(field, pd.Value);
		}

		public ValidationError ValidatePrimitive(FieldSpecification field, object input)
		{
			string val = (string)input;
			
			#region Is required
			if (field.IsRequired && String.IsNullOrWhiteSpace(val))
			{
				return new ValidationError(field.PropertyPath, "Field is required but was 'null', 'String.Empty' or consisted of only whitespace.");
			}
			
			else
			{
				// if the field is not required
				// and it has no value, skip the rest
				// of the validation
				if (String.IsNullOrWhiteSpace(val))
				{
					return default(ValidationError);
				}
			}
			#endregion Is required
			
			#region Length
			if (field.MaxLength > 0)
			{
				if (val.Length > field.MaxLength)
				{
					return new ValidationError(field.PropertyPath, "Value exceeds max length of '{0}'", field.MaxLength);
				}
			}
			
			if (field.MinLength >= 0)
			{
				if (val.Length < field.MinLength)
				{
					return new ValidationError(field.PropertyPath, "Value is shorter than allowed minimum length of '{0}'", field.MinLength);
				}
			}
			#endregion Length
			
			#region Invalid values
			if (field.InvalidValues.Count != 0)
			{
				if (field.InvalidValues.Contains(val))
				{
					return new ValidationError(field.PropertyPath, "Value is not allowed");
				}
			}
			#endregion Invalid values
			
			#region Enforcer
			if (field.Enforcer != default(Enforcer))
			{
				string enforcerResult = String.Empty;
				
				try
				{
					enforcerResult = field.Enforcer.Check(val);
				}
				catch (Exception ex)
				{
					enforcerResult = ex.Message;
				}
				
				if (!enforcerResult.Equals(String.Empty))
				{
					return new ValidationError(field.PropertyPath, enforcerResult) {
						EnforcerName = field.Enforcer.GetType().FullName
					};
				}
			}
			#endregion Enforcer
			
			return default(ValidationError);
		}
		#endregion
	}
}