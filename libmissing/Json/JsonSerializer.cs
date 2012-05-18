using System;
using System.Collections.Generic;
using Missing.Json.TypeSerializers;
using System.Reflection;
using System.Text;
using Missing.Json.ClassAnalysis;
using Missing.Reflection;

namespace Missing.Json
{
	internal class JsonSerializer
	{
		private static Dictionary<Type, ITypeSerializer> Serializers = new Dictionary<Type, ITypeSerializer>() {
			{typeof(String), new StringSerializer()},
			{typeof(Int32), new Int32Serializer()},
			{typeof(Int64), new Int64Serializer()},
			{typeof(float), new SingleSerializer()},
			{typeof(double), new DoubleSerializer()},
			{typeof(decimal), new DecimalSerializer()},
			{typeof(bool), new BoolSerializer()}
		};
		
		private static ClassSpecificationCache Cache = new ClassSpecificationCache();
		
		public JsonSerializer()
		{
		}
		
		public string Serialize(object obj)
		{
			if (obj == null)
			{
				return "null";
			}
			
			Type t = obj.GetType();
			
			if (Serializers.ContainsKey(t))
			{
				return Serializers[t].Serialize(obj);
			}
			
			if (!Cache.ContainsKey(t))
			{
				List<ClassSpecification> specs = ClassSpecification.For(t);
				foreach (ClassSpecification x in specs)
				{
					if (!Cache.ContainsKey(x.Type))
					{
						Cache.Add(x.Type, x);
					}
				}
			}
			
			ClassSpecification spec = Cache[t];
			
			StringBuilder sb = new StringBuilder();
			
			// open object
			sb.Append("{");
			
			foreach (ClassProperty cur in spec.Properties)
			{
				sb.AppendFormat("\"{0}\":{1},", cur.Name, this.Serialize(cur.PropertyInfo.GetValue(obj, null)));
			}
			
			// remove excess comma
			sb.Remove(sb.Length-1, 1);
			
			// close the object
			sb.Append("}");
			
			return sb.ToString();
		}
	}
}