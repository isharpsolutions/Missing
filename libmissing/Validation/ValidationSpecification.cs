using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Missing.Validation
{
	public class ValidationSpecification<T> where T : class
	{
		public ValidationSpecification()
		{
		}

		private ValidationPropertyCollection properties = new ValidationPropertyCollection();
		
		public ValidationPropertyCollection Properties
		{
			get { return this.properties; }
			set { this.properties = value; }
		}
		
		public ValidationProperty Field(Expression<Func<T, object>> memberExpression)
		{
			ValidationProperty prop = new ValidationProperty();
			
			string lambdaPrefix = memberExpression.Parameters[0].Name;
			
			string[] path = memberExpression.Body.ToString().Remove(0, lambdaPrefix.Length + 1).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			
			foreach (string p in path)
			{
				prop.PropertyPath.Add(p);
			}
			
			this.properties.Add(prop);
			
			return prop;
		}
	}
}

