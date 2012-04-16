using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Missing.Reflection
{
	/// <summary>
	/// Does the hard work for <see cref="PropertyPath"/>
	/// </summary>
	/// <remarks>
	/// You should not call these methods directly. Call them through <see cref="PropertyPath"/>
	/// instead - e.g. <see cref="PropertyPath.From`T"/>
	/// </remarks>
	internal static class PropertyPathWorker
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