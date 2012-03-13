using System;
using System.Reflection;

namespace Missing.Reflection
{
	/// <summary>
	/// Carries data regarding a class property
	/// </summary>
	public class PropertyData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Reflection.PropertyData"/> class.
		/// </summary>
		public PropertyData()
		{
		}
		
		/// <summary>
		/// Get/set the actual PropertyInfo
		/// </summary>
		public PropertyInfo PropertyInfo { get; set; }
		
		/// <summary>
		/// Get/set the value
		/// </summary>
		/// <remarks>
		/// This is used by <see cref="TypeHelper.GetPropertyData"/> to
		/// carry the value of the property from a given instance
		/// </remarks>
		public object Value { get; set; }
	}
}