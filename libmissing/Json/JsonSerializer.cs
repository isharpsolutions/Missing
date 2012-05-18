using System;
using System.Collections.Generic;
using Missing.Json.TypeSerializers;
using System.Reflection;
using System.Text;

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
			{typeof(decimal), new DecimalSerializer()}
		};
		
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
			
			else
			{
				PropertyInfo[] properties = t.GetProperties();
				
				StringBuilder sb = new StringBuilder();
				
				// open object
				sb.Append("{");
				
				foreach (PropertyInfo pi in properties)
				{
					sb.AppendFormat("\"{0}\":{1},", pi.Name, this.Serialize(pi.GetValue(obj, null)));
				}
				
				// remove excess comma
				sb.Remove(sb.Length-1, 1);
				
				// close the object
				sb.Append("}");
				
				return sb.ToString();
			}
		}
	}
}