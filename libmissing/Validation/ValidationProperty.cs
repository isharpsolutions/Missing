using System;
using System.Reflection;
using System.Collections.Generic;

namespace Missing.Validation
{
	public class ValidationProperty
	{
		public ValidationProperty()
		{
			this.PropertyPath = new List<string>();
		}
		
		public IList<string> PropertyPath { get; set; }
		
		#region Required
		private bool isRequired = false;
		
		public bool IsRequired
		{
			get { return this.isRequired; }
			set { this.isRequired = value; }
		}
		
		public ValidationProperty Required()
		{
			this.IsRequired = true;
			
			return this;
		}
		#endregion Required
	}
}