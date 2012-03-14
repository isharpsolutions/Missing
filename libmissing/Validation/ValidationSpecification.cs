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
			
			this.properties.Add(prop);
			
			return prop;
		}
		
		
	}
	
	public static class ValidationSpecification
	{
		internal static IList<string> GetPropertyPath<T>(Expression<Func<T, object>> exp) where T : class
		{
			MemberExpression mExp;
			
			if (exp.Body is UnaryExpression)
			{
				UnaryExpression uexp = (UnaryExpression)exp.Body;
				
				mExp = (MemberExpression)uexp.Operand;
			}
			
			else
			{
				mExp = (MemberExpression)exp.Body;
			}
			
			if (mExp == default(MemberExpression))
			{
				throw new ArgumentException("Unable to determine a MemberExpression");
			}
			
			return GetPropertyPathFromMemberExpression(mExp);
		}
		
		private static IList<string> GetPropertyPathFromMemberExpression(MemberExpression exp)
		{
			List<string> path = new List<string>();
			
			path.Add( exp.Member.Name );
			
			return path;
		}
	}
}

