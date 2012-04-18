using System;

namespace Missing.Validation.Validators
{
	/// <summary>
	/// Knows how to validate <see cref="System.Decimal"/>
	/// </summary>
	public class DecimalValidator : IValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Validators.DecimalValidator"/> class.
		/// </summary>
		public DecimalValidator()
		{
		}
		
		#region IValidator implementation
		public ValidationError ValidateField<T>(FieldSpecification field, T input, Missing.Reflection.PropertyData pd) where T : class
		{
			decimal val = (decimal)pd.Value;
			
			//
			// we cant check for IsRequired
			// as a decimal is not able to be null
			//
			
			// length does not make sense to check
			
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

