using System;
using System.Collections.Generic;

namespace Missing.Validation
{
	public class PropertyPath
	{
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

