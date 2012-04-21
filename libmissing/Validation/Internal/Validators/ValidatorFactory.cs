using System;
using System.Collections;
using Missing.Reflection.Extensions;

namespace Missing.Validation.Internal.Validators
{
	/// <summary>
	/// Factory class for <see cref="IValidator"/>
	/// </summary>
	internal static class ValidatorFactory
	{
		/// <summary>
		/// Get an instance of the validator built for the
		/// specific value type
		/// </summary>
		/// <returns>
		/// The validator
		/// </returns>
		/// <param name="fieldValueType">
		/// The type of the value to validate
		/// </param>
		/// <exception cref="NotSupportedException">
		/// Thrown if there is no known validator for the value type
		/// </exception>
		public static IValidator GetValidatorFor(Type fieldValueType)
		{
			switch (fieldValueType.FullName)
			{
				case "System.String":
				{
					return new StringValidator();
				}
					
				case "System.Int32":
				{
					return new Int32Validator();
				}
					
				case "System.Int64":
				{
					return new LongValidator();
				}
					
				case "System.Decimal":
				{
					return new DecimalValidator();
				}
					
				case "System.Single":
				{
					return new FloatValidator();
				}
					
				case "System.Double":
				{
					return new DoubleValidator();
				}
				
				default:
				{
					if (fieldValueType.ImplementsInterface(typeof(IEnumerable)))
					{
						return new EnumerableValidator();
					}
					
					throw new NotSupportedException(String.Format("There is no known validator for '{0}'", fieldValueType.FullName));
				}
			}
		}
	}
}