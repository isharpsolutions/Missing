using System;
using System.Reflection;
using System.Collections.Generic;

namespace Missing.Validation
{
	/// <summary>
	/// Validation property
	/// </summary>
	public class ValidationProperty
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.ValidationProperty"/> class.
		/// </summary>
		public ValidationProperty()
		{
			this.PropertyPath = new List<string>();
		}
		
		/// <summary>
		/// Get/set the property path
		/// </summary>
		public IList<string> PropertyPath { get; set; }
		
		/// <summary>
		/// Get the name of the underlying property
		/// </summary>
		public string Name
		{
			get
			{
				return this.PropertyPath[ this.PropertyPath.Count - 1 ];
			}
		}
		
		#region Required
		private bool isRequired = false;
		
		/// <summary>
		/// Get/set whether this property is required
		/// </summary>
		internal bool IsRequired
		{
			get { return this.isRequired; }
			set { this.isRequired = value; }
		}
		
		/// <summary>
		/// Marks this property as being required
		/// </summary>
		/// <remarks>
		/// Currently we do not support "Required"
		/// for properties of types where default(..type..) = null
		/// </remarks>
		public ValidationProperty Required()
		{
			this.IsRequired = true;
			
			return this;
		}
		#endregion Required
	}
}