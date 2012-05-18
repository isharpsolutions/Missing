using System;
using System.Reflection;
using Missing.Reflection;
using System.Collections.Generic;

namespace Missing.Json.ClassAnalysis
{
	internal class ClassSpecification
	{
		#region Static: For
		public static List<ClassSpecification> For(Type t)
		{
			List<ClassSpecification> res = new List<ClassSpecification>();
			
			ClassSpecification spec = new ClassSpecification();
			
			spec.Type = t;
			
			res.Add(spec);
			
			PropertyInfo[] properties = t.GetProperties();
			
			List<ClassSpecification> sub;
			
			foreach (PropertyInfo pi in properties)
			{
				spec.Properties.Add(ClassProperty.For(pi));
				
				if (!Primitives.IsPrimitive(pi.PropertyType))
				{
					sub = ClassSpecification.For(pi.PropertyType);
					foreach (ClassSpecification x in sub)
					{
						res.Add(x);
					}
				}
			}
			
			return res;
		}
		#endregion Static: For
		
		public ClassSpecification()
		{
			this.Properties = new ClassPropertyCollection();
		}
		
		public Type Type { get; set; }
		public ClassPropertyCollection Properties { get; set; }
	}
}