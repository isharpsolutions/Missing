using System;

namespace Missing.Validation
{
	public static class Validator
	{
		public static ValidationResult Validate<T>(T input) where T : class
		{
			ValidationResult result = new ValidationResult();
			
			// find validation specification class
			// if unable to find one throw ArgumentException/InvalidOperationException
			// get validation specification
			// run through each element in the specification
			
			return result;
		}
	}
}

