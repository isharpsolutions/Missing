using System;
using System.Collections;

namespace Missing.Validation.Internal.Validators
{
	/// <summary>
	/// Knows how to validate <see cref="System.Collections.IEnumerable"/>
	/// </summary>
	internal class EnumerableValidator : IValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Internal.Validators.EnumerableValidator"/> class.
		/// </summary>
		public EnumerableValidator()
		{
		}
		
		#region IValidator implementation
		public ValidationError ValidateField<T>(FieldSpecification field, T input, Missing.Reflection.PropertyData pd) where T : class
		{
			IEnumerable val = (IEnumerable)pd.Value;
			
			#region Is required
			if (field.IsRequired && val == null)
			{
				return new ValidationError(field.PropertyPath, "Field is required but was 'null'.");
			}
			
			else
			{
				// if the field is not required
				// and it has no value, skip the rest
				// of the validation
				if (val == null)
				{
					return default(ValidationError);
				}
			}
			#endregion Is required
			
			int valCount = 0;
			foreach (var item in val)
			{
				valCount++;
			}
			
			#region Not empty
			if (!field.EmptyIsAllowed)
			{
				if (valCount == 0)
				{
					return new ValidationError(field.PropertyPath, "The list/array may not be empty");
				}
			}
			#endregion Not empty
			
			#region Length
			if (field.MaxLength > 0)
			{
				if (valCount > field.MaxLength)
				{
					return new ValidationError(field.PropertyPath, "The number of items exceeds the max length of '{0}'", field.MaxLength);
				}
			}
			
			if (field.MinLength >= 0)
			{
				if (valCount < field.MinLength)
				{
					return new ValidationError(field.PropertyPath, "The number of items is lower than allowed minimum length of '{0}'", field.MinLength);
				}
			}
			#endregion Length
			
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

		public ValidationError ValidatePrimitive(FieldSpecification field, object input)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}