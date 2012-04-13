using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Missing.Validation
{
	public class PropertyPath
	{
		#region Static: From<T>
		public static PropertyPath From<T>(Expression<Func<T, object>> memberExpression) where T : class
		{
			return ValidationSpecification.GetPropertyPath<T>(memberExpression);
		}
		#endregion Static: From<T>
		
		public PropertyPath()
		{
			this.Parts = new List<String>();
		}
		
		
		
		public IList<String> Parts { get; set; }
		
		public string AsString()
		{
			return String.Join(".", this.Parts);
		}
		
		public string FieldName
		{
			get
			{
				return this.Parts[ this.Parts.Count - 1 ];
			}
		}
	}
}