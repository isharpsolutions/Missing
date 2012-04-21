using System;

namespace Missing.Validation.Internal.Validators
{
	/// <summary>
	/// Knows how to validate <see cref="System.Int64"/> aka "long"
	/// </summary>
	internal class LongValidator : IValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Internal.Validators.LongValidator"/> class.
		/// </summary>
		public LongValidator()
		{
		}

		#region IValidator implementation
		public ValidationError ValidateField<T>(FieldSpecification field, T input, Missing.Reflection.PropertyData pd) where T : class
		{
			long val = (long)pd.Value;
			
			//
			// we cant check for IsRequired
			// as a long is not able to be null
			//
			
			// length does not make sense to check
			
			#region Range
			if (field.LongRange.Min != null)
			{
				if (val < field.LongRange.Min)
				{
					return new ValidationError(field.PropertyPath, "The value is too low - it must be at least {0}", field.LongRange.Min);
				}
				
				else if (val > field.LongRange.Max)
				{
					return new ValidationError(field.PropertyPath, "The value is too high - it must be at {0} the most", field.LongRange.Max);
				}
			}
			#endregion Range
			
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

		public ValidationError ValidatePrimitive(FieldSpecification field, object input)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}