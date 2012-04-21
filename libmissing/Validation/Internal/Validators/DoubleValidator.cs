using System;

namespace Missing.Validation.Internal.Validators
{
	/// <summary>
	/// Knows how to validate <see cref="System.Double"/>
	/// </summary>
	internal class DoubleValidator : IValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Internal.Validators.DoubleValidator"/> class.
		/// </summary>
		public DoubleValidator()
		{
		}
		
		#region IValidator implementation
		public ValidationError ValidateField<T>(FieldSpecification field, T input, Missing.Reflection.PropertyData pd) where T : class
		{
			double val = (double)pd.Value;
			
			//
			// we cant check for IsRequired
			// as a double is not able to be null
			//
			
			// length does not make sense to check
			
			#region Range
			if (field.DoubleRange.Min != null)
			{
				if (val < field.DoubleRange.Min)
				{
					return new ValidationError(field.PropertyPath, "The value is too low - it must be at least {0}", field.DoubleRange.Min);
				}
				
				else if (val > field.DoubleRange.Max)
				{
					return new ValidationError(field.PropertyPath, "The value is too high - it must be at {0} the most", field.DoubleRange.Max);
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