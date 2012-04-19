using System;
using Missing.Reflection;
using System.Collections.Generic;

namespace Missing.Validation
{
	/// <summary>
	/// <see cref="ValidationSpecification"/> for primitive values.
	/// </summary>
	/// <remarks>
	/// This should not be used directly in consumer code.
	/// </remarks>
	public class PrimitiveValidationSpecification<TModel> : ValidationSpecification<TModel> where TModel : class
	{
		/// <summary>
		/// Specify that the field itself holds the value
		/// to validate.
		/// </summary>
		public FieldSpecification Value()
		{
			FieldSpecification prop = new FieldSpecification();
			
			prop.PropertyPath = new PropertyPath() {
				Parts = new List<string>() {
					Missing.Validation.Internal.InternalValidator.PrimitiveFieldName
				}
			};
			
			base.Fields.Add(prop);
			
			return prop;
		}
	}
}