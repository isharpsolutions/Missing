using System;
using Missing.Reflection;
using System.Collections.Generic;

namespace Missing.Validation
{
	public class PrimitiveValidationSpecification<T> : ValidationSpecification<T> where T : class
	{
		public FieldSpecification Value()
		{
			FieldSpecification prop = new FieldSpecification();
			
			prop.PropertyPath = new PropertyPath() {
				Parts = new List<string>() {
					Validator.PrimitiveFieldName
				}
			};
			
			base.Fields.Add(prop);
			
			return prop;
		}
	}
}