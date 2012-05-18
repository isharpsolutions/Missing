using System;
using System.Reflection;

namespace Missing.Json.ClassAnalysis
{
	internal class ClassProperty
	{
		#region Static: For
		public static ClassProperty For(PropertyInfo topPi)
		{
			ClassProperty res = new ClassProperty();
			res.Name = topPi.Name;
			res.Type = topPi.PropertyType;
			res.PropertyInfo = topPi;
			
			return res;
		}
		#endregion Static: For
		
		public ClassProperty()
		{
		}
		
		public string Name { get; set; }
		public Type Type { get; set; }
		public PropertyInfo PropertyInfo { get; set; }
	}
}