using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Missing.Validation
{
	/// <summary>
	/// Defines a specification for validation of a specific type
	/// </summary>
	public class ValidationSpecification<T> where T : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.ValidationSpecification`1"/> class.
		/// </summary>
		public ValidationSpecification()
		{
		}
		
		/// <summary>
		/// Collection of validation properties
		/// </summary>
		private FieldSpecificationCollection fields = new FieldSpecificationCollection();
		
		/// <summary>
		/// Get/set the full set of fields
		/// </summary>
		internal FieldSpecificationCollection Fields
		{
			get { return this.fields; }
			set { this.fields = value; }
		}
		
		/// <summary>
		/// Add a field to the specification
		/// </summary>
		/// <param name="memberExpression">
		/// Lambda expression like "y => y.PropertyOne.PropertyTwo"
		/// </param>
		public FieldSpecification Field(Expression<Func<T, object>> memberExpression)
		{
			FieldSpecification prop = new FieldSpecification();
			
			prop.PropertyPath = ValidationSpecification.GetPropertyPath<T>(memberExpression);
			
			this.fields.Add(prop);
			
			return prop;
		}
		
		
	}
	
	internal static class ValidationSpecification
	{
		/// <summary>
		/// Get property path from lambda expression
		/// </summary>
		/// <returns>
		/// The property path.
		/// </returns>
		/// <param name="exp">
		/// The lambda expression
		/// </param>
		/// <typeparam name="T">
		/// The type for which the specification applies
		/// </typeparam>
		/// <exception cref="NotSupportedException">
		/// Thrown if the method is unable to find the appropiate
		/// MemberExpression in the lambda expression.
		/// </exception>
		internal static PropertyPath GetPropertyPath<T>(Expression<Func<T, object>> exp) where T : class
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
				throw new NotSupportedException(String.Format("Unable to find the appropiate MemberExpression for expression of type '{0}'", exp.GetType()));
			}
			
			return GetPropertyPathFromMemberExpression(mExp);
		}
		
		/// <summary>
		/// Get property path from MemberExpression
		/// </summary>
		/// <returns>
		/// The property path from the given member expression.
		/// </returns>
		/// <param name="exp">
		/// The member expression
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if the inner loop exceeds a maximum number of iterations
		/// </exception>
		private static PropertyPath GetPropertyPathFromMemberExpression(MemberExpression exp)
		{
			List<String> path = new List<String>();
			
			
			MemberExpression curExp = exp;
			
			int failsafeMax = 500;
			int failsafe = 0;
			
			while (true)
			{
				path.Add(curExp.Member.Name);
				
				if (curExp.Expression is MemberExpression)
				{
					curExp = (MemberExpression)curExp.Expression;
				}
				
				else
				{
					break;
				}
				
				failsafe++;
				
				if (failsafe == failsafeMax)
				{
					throw new ArgumentException(String.Format("Breaking out of infinite loop using failsafe of {0} iterations", failsafeMax));
				}
			}
			
			path.Reverse();
			
			return new PropertyPath() {
				Parts = path
			};
		}
	}
}

