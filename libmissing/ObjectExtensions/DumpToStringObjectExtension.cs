using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Missing.Reflection.Extensions;

namespace Missing.ObjectExtensions
{
	public static class DumpToStringObjectExtension
	{
		private static string MakeIndentation(int indendation)
		{
			return "".PadLeft(indendation, '\t');
		}
		
		public static string DumpToString(this object obj)
		{
			return obj.DumpToString(0);
		}
		
		public static string DumpToString(this object obj, int indendation)
		{
			return obj.DumpToString(indendation, String.Empty);
		}
		
		public static string DumpToString(this object obj, int indendation, string prefixFormat, params object[] args)
		{
			return obj.DumpToString(indendation, String.Format(prefixFormat, args));
		}
			
		public static string DumpToString(this object obj, int indendation, string prefix)
		{
			string indent = MakeIndentation(indendation);
			
			if (obj == null)
			{
				return String.Format("{0}{1}{2}", indent, prefix, "null");
			}
			
			Type t = obj.GetType();
			
			if (t.ImplementsInterface(typeof(IEnumerable)) && t != typeof(String))
			{
				return DumpEnumerable((IEnumerable)obj, indendation, prefix);
			}
			
			//
			// types that need to be wrapped in single quotes
			//
			if (t == typeof(String) || t == typeof(Char) || t == typeof(DateTime) || t.IsEnum)
			{
				return String.Format("{0}{1}'{2}'", indent, prefix, obj.ToString());
			}
			
			//
			// non-quoted types
			//
			if (t.IsPrimitive || t == typeof(Decimal))
			{
				return String.Format("{0}{1}{2}", indent, prefix, obj.ToString());
			}
			
			//
			// complex types
			//
			return String.Format("{0}{1}{2}", indent, prefix, DumpNonPrimitiveType(t, obj, indendation));
		}
		
		
		
		private static string DumpNonPrimitiveType(Type t, object obj, int indendation)
		{
			StringBuilder sb = new StringBuilder();
			
			string indent = MakeIndentation(indendation);
			
			sb.AppendLine("{");
			
			PropertyInfo[] properties = t.GetProperties();
			
			object val;
			
			foreach (PropertyInfo pi in properties)
			{
				val = pi.GetValue(obj, null);
				
				sb.Append(val.DumpToString(indendation+1, "{0} = ", pi.Name));
				sb.AppendLine();
			}
			
			sb.Append(indent);
			sb.Append("}");
			
			return sb.ToString();
		}
		
		
		
		private static string DumpEnumerable(IEnumerable list, int indendation, string prefix)
		{
			StringBuilder sb = new StringBuilder();
			
			if (prefix.EndsWith(" = "))
			{
				prefix = prefix.Remove(prefix.Length-3);
			}
			
			int index = 0;
			
			foreach (object cur in list)
			{
				sb.Append(cur.DumpToString(indendation, "{0}[{1}] = ", prefix, index));
				sb.AppendLine();
				index++;
			}
			
			RemoveLastNewline(sb);
			
			return sb.ToString();
		}
		
		
		private static void RemoveLastNewline(StringBuilder sb)
		{
			string newline = System.Environment.NewLine;
			
			sb = sb.Remove(sb.Length-newline.Length, newline.Length);
		}
	}
}