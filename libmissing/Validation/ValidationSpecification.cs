using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;
using Missing.Reflection;
using System.Reflection;

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
			
			prop.PropertyPath = PropertyPath.From<T>(memberExpression);
			
			this.fields.Add(prop);
			
			return prop;
		}
	}
}