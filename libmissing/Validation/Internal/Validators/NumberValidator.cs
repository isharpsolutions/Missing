using System;

namespace Missing.Validation.Internal.Validators
{
	/// <summary>
	/// Knows how to validate number types (int, long, float, decimal etc).
	/// </summary>
	/// <typeparam name="TNumber">
	/// The number type to validate
	/// </typeparam>
	internal class NumberValidator<TNumber> : IValidator where TNumber : struct, IComparable<TNumber>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Internal.Validators.NumberValidator`1"/> class.
		/// </summary>
		public NumberValidator()
		{
		}
		
		#region IValidator implementation
		public ValidationError ValidateField<T>(FieldSpecification field, T input, Missing.Reflection.PropertyData pd) where T : class
		{
			TNumber val = (TNumber)pd.Value;
			
			//
			// we cant check for IsRequired
			// as number is not able to be null
			//
			
			// length does not make sense to check
			
			#region Range
			if (field.DefinedRange != null)
			{
				Range<TNumber> range = (Range<TNumber>)field.DefinedRange;
				
				// unfortunately I have not been able to find a way
				// to use the "<" operator, so we have to do the
				// comparison "by hand"
				int compare = val.CompareTo(range.Min);
				
				if (compare == -1)
				{
					return new ValidationError(field.PropertyPath, "The value is too low - it must be at least {0}", range.Min);
				}
				
				compare = val.CompareTo(range.Max);
				
				if (compare == 1)
				{
					return new ValidationError(field.PropertyPath, "The value is too high - it must be at {0} the most", range.Max);
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