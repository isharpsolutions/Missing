using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Missing.Reflection.Extensions;

namespace Missing.ObjectExtensions
{
	public static class DumpToStringObjectExtension
	{
		public static string DumpToString(this object obj)
		{
			return obj.DumpToString(String.Empty);
		}
		
		public static string DumpToString(this object obj, string prefixFormat, params object[] args)
		{
			return obj.DumpToString(String.Format(prefixFormat, args));
		}
			
		public static string DumpToString(this object obj, string prefix)
		{
			if (obj == null)
			{
				return String.Format("{0}{1}", prefix, "null");
			}
			
			Type t = obj.GetType();
			
			//
			// types that need to be wrapped in single quotes
			//
			if (t == typeof(String) || t == typeof(Char) || t == typeof(DateTime) || t.IsEnum)
			{
				return String.Format("{0}'{1}'", prefix, obj.ToString());
			}
			
			//
			// non-quoted types
			//
			if (t.IsPrimitive || t == typeof(Decimal))
			{
				return String.Format("{0}{1}", prefix, obj.ToString());
			}
			
			//
			// complex types
			//
			return String.Format("{0}{1}", prefix, DumpNonPrimitiveType(t, obj));
		}
		
		private static string DumpNonPrimitiveType(Type t, object obj)
		{
			StringBuilder sb = new StringBuilder();
			
			sb.AppendLine("{");
			
			PropertyInfo[] properties = t.GetProperties();
			
			object val;
			
			foreach (PropertyInfo pi in properties)
			{
				val = pi.GetValue(obj, null);
				
				sb.Append(val.DumpToString("{0} = ", pi.Name));
				sb.AppendLine();
			}
			
			sb.Append("}");
			
			return sb.ToString();
		}
		
		
		
		private static string HandleNonPrimitiveType(PropertyInfo pi, object obj)
		{
			if (pi.PropertyType.ImplementsInterface(typeof(IEnumerable)))
			{
				return HandleEnumerable(pi, obj);
			}
			
			object val = pi.GetValue(obj, null);
			
			if (val == null)
			{
				return "null";
			}
			
			switch (pi.PropertyType.ToString())
			{
				case "System.Decimal":
				case "MongoDB.Bson.ObjectId":
				{
					return String.Format("'{0}'", val);
				}
					
				default:
				{
					StringBuilder sb = new StringBuilder();
					
					sb.Append("{");
					sb.AppendLine();
					
					sb.Append(val.DumpToString());
					
					//sb.AppendLine();
					sb.Append("}");
					
					return sb.ToString();
				}
			}
		}
		
		private static string HandleEnumerable(PropertyInfo pi, object obj)
		{
			IEnumerable list = (IEnumerable)pi.GetValue(obj, null);
			
			StringBuilder sb = new StringBuilder();
			
			sb.Append("[");
			sb.AppendLine();
			
			foreach (Object cur in list)
			{
				sb.Append("{");
				sb.AppendLine();
				
				sb.Append(cur.DumpToString());
				
				sb.Append("},");
				sb.AppendLine();
			}
			
			sb.Append("]");
			
			return sb.ToString();
		}
	}
}