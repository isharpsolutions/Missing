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
			
		public static string DumpToString(this object obj, string prefix)
		{
			if (obj == null)
			{
				return "null";
			}
			
			Type t = obj.GetType();
			
			if (t.IsPrimitive || t == typeof(String) || t == typeof(DateTime) || t == typeof(Decimal) || t.IsEnum)
			{
				return obj.ToString();
			}
			
			throw new NotSupportedException(String.Format("Type '{0}' is not supported yet", t.Name));
			
			
			StringBuilder sb = new StringBuilder();
			
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