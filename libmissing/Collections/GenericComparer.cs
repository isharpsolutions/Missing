using System;
using System.Collections.Generic;
using Missing.Validation;
using System.Linq.Expressions;
using Missing.Reflection;

namespace Missing.Collections
{
	/// <summary>
	/// Generic comparer.
	/// </summary>
	/// <typeparam name="T">
	/// The type of item to compare
	/// </typeparam>
	/// <remarks>
	/// Adapted from code in StackOverflow response http://stackoverflow.com/a/2692199
	/// </remarks>
	/// <example>
	/// <code>
	/// public class MyScore
	/// {
	/// 	public int Score { get; set; }
	/// 	public string Name { get; set; }
	/// }
	/// 
	/// List<MyScore> scores = new List<MyScore>();
	/// ...add scores...
	/// scores.Sort(new GenericComparer<MyScore>(y => y.Match));
	/// </code>
	/// </example>
	public class GenericComparer<T> : IComparer<T> where T : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.GenericComparer`1"/> class.
		/// </summary>
		public GenericComparer()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.GenericComparer`1"/> class.
		/// </summary>
		/// <param name="sortExpression">
		/// Expression stating which property to
		/// use for comparison
		/// </param>
		/// <param name="sortDirection">
		/// Sort direction. Defaults to ascending
		/// </param>
		public GenericComparer(Expression<Func<T, object>> sortExpression, SortDirection sortDirection = SortDirection.Ascending)
		{
			this.SortExpression = PropertyPath.From<T>(sortExpression);
			this.SortDirection = sortDirection;
		}
		
		/// <summary>
		/// Get/set the path of the property to use
		/// for comparison
		/// </summary>
		/// <example>
		/// <code>
		/// myComparer.SortExpression = PropertyPath.From<T>(y => y.Property);
		/// </code>
		/// </example>
		public PropertyPath SortExpression { get; set; }
		
		/// <summary>
		/// Get/set the sort direction
		/// </summary>
		public SortDirection SortDirection { get; set; }

		#region IComparer<T> Members
		/// <summary>
		/// Compare the specified x and y.
		/// </summary>
		/// <param name='x'>
		/// X.
		/// </param>
		/// <param name='y'>
		/// Y.
		/// </param>
		public int Compare(T x, T y)
		{
			PropertyData p = TypeHelper.GetPropertyData(x, this.SortExpression.Parts);

			IComparable obj1 = (IComparable)p.Value;
			IComparable obj2 = (IComparable)p.PropertyInfo.GetValue(y, null);
			
			if (this.SortDirection == SortDirection.Ascending)
			{
				return obj1.CompareTo(obj2);
			}
			
			else
			{
				return obj2.CompareTo(obj1);
			}
		}
		#endregion
	}
}