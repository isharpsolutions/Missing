using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Missing.Validation;

namespace Missing.Reflection
{
	/// <summary>
	/// The path to a specific property on a class.
	/// </summary>
	/// <example>
	/// <code>
	/// public class Child
	/// {
	/// 	public string Name { get; set; }
	/// }
	/// 
	/// public class Parent
	/// {
	/// 	public Child FirstBorn { get; set; }
	/// }
	/// </code>
	/// 
	/// The path to the name of the firstborn, from the perspective of "Parent", would be "FirstBorn.Name"
	/// </example>
	public class PropertyPath
	{
		#region Static: From<T>
		/// <summary>
		/// Generate a property path using a lambda-expression on the given class
		/// </summary>
		/// <param name="memberExpression">
		/// Lambda-expression that selects a property on <typeparamref name="T"/> or one of it's children.
		/// </param>
		/// <typeparam name="T">
		/// The class from which to generate the path
		/// </typeparam>
		public static PropertyPath From<T>(Expression<Func<T, object>> memberExpression) where T : class
		{
			return PropertyPathWorker.GetPropertyPath<T>(memberExpression);
		}
		#endregion Static: From<T>

		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Reflection.PropertyPath"/> class.
		/// </summary>
		public PropertyPath()
		{
			this.Parts = new List<String>();
		}
		
		/// <summary>
		/// Gets or sets the parts of the path
		/// </summary>
		public IList<String> Parts { get; set; }
		
		/// <summary>
		/// The property path as you would write it in code.
		/// </summary>
		/// <returns>
		/// A string like "Property.Property.Property"
		/// </returns>
		public string AsString()
		{
			return String.Join(".", this.Parts);
		}
		
		/// <summary>
		/// Gets the name of the field.
		/// </summary>
		public string FieldName
		{
			get
			{
				return this.Parts[ this.Parts.Count - 1 ];
			}
		}
	}
}