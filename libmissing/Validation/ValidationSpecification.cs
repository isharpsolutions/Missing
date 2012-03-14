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
			
			prop.PropertyPath = ValidationSpecification.GetPropertyPath<T>(memberExpression);
			
			/*
			string[] path = memberExpression.Body.ToString().Remove(0, lambdaPrefix.Length + 1).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			
			foreach (string p in path)
			{
				prop.PropertyPath.Add(p);
			}
			*/
			
			this.properties.Add(prop);
			
			return prop;
		}
		
		
	}
	
	public static class ValidationSpecification
	{
		internal static IList<string> GetPropertyPath<T>(Expression<Func<T, object>> exp) where T : class
		{
			List<string> path = new List<string>();
			
			if (exp.Body is UnaryExpression)
			{
				UnaryExpression uexp = (UnaryExpression)exp.Body;
				MemberExpression mexp = (MemberExpression)uexp.Operand;
				
				path.Add(mexp.Member.Name);
			}
			
			else
			{
				path.Add( ((MemberExpression)exp.Body).Member.Name);
			}
			
			return path;
		}
	}
}

