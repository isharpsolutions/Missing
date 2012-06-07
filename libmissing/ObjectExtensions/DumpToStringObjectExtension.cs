using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Missing.Reflection.Extensions;
using Missing.Text.Extensions;
using System.Linq;

namespace Missing.ObjectExtensions
{
	/// <summary>
	/// "Dump to string" extensions for <see cref="Object"/>
	/// </summary>
	public static class DumpToStringObjectExtension
	{
		#region Overloads
		/// <summary>
		/// Dumps the entire object structure as a string
		/// </summary>
		/// <returns>
		/// A string with the entire structure of the instance
		/// </returns>
		/// <param name="obj">
		/// The instance to dump (<c>null</c> is allowed)
		/// </param>
		public static string DumpToString(this object obj)
		{
			return obj.DumpToString(0);
		}
		
		/// <summary>
		/// Dumps the entire object structure as a string
		/// </summary>
		/// <returns>
		/// A string with the entire structure of the instance
		/// </returns>
		/// <param name="obj">
		/// The instance to dump (<c>null</c> is allowed)
		/// </param>
		/// <param name="indendation">
		/// The indendation level
		/// </param>
		private static string DumpToString(this object obj, int indendation)
		{
			return obj.DumpToString(indendation, String.Empty);
		}
		
		/// <summary>
		/// Dumps the entire object structure as a string
		/// </summary>
		/// <returns>
		/// A string with the entire structure of the instance
		/// </returns>
		/// <param name="obj">
		/// The instance to dump (<c>null</c> is allowed)
		/// </param>
		/// <param name="indendation">
		/// The indendation level
		/// </param>
		/// <param name="prefixFormat">
		/// Format of a text to output before the value
		/// </param>
		/// <param name="prefixArgs">
		/// Arguments for the prefix
		/// </param>
		private static string DumpToString(this object obj, int indendation, string prefixFormat, params object[] prefixArgs)
		{
			return obj.DumpToString(indendation, String.Format(prefixFormat, prefixArgs));
		}
		#endregion Overloads
			
		#region Dump to string
		/// <summary>
		/// Dumps the entire object structure as a string
		/// </summary>
		/// <returns>
		/// A string with the entire structure of the instance
		/// </returns>
		/// <param name="obj">
		/// The instance to dump (<c>null</c> is allowed)
		/// </param>
		/// <param name="indendation">
		/// The indendation level
		/// </param>
		/// <param name="prefix">
		/// Text to output before the value
		/// </param>
		private static string DumpToString(this object obj, int indendation, string prefix)
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
		#endregion Dump to string
		
		#region Non primitive
		/// <summary>
		/// Dumps the entire structure of a non-primitive object
		/// </summary>
		/// <returns>
		/// A string with the entire structure of the instance
		/// </returns>
		/// <param name="t">
		/// The type of the object
		/// </param>
		/// <param name="obj">
		/// The instance to dump
		/// </param>
		/// <param name="indendation">
		/// The indendation level
		/// </param>
		private static string DumpNonPrimitiveType(Type t, object obj, int indendation)
		{
			StringBuilder sb = new StringBuilder();
			
			string indent = MakeIndentation(indendation);
			
			sb.AppendLine("{");
			
			PropertyInfo[] properties = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			
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
		#endregion Non primitive
		
		#region Enumerable
		/// <summary>
		/// Dumps an enumerable type
		/// </summary>
		/// <returns>
		/// The enumerable values as a string
		/// </returns>
		/// <param name="list">
		/// The enumerable instance
		/// </param>
		/// <param name="indendation">
		/// The indendation level
		/// </param>
		/// <param name="prefix">
		/// Text to output before the value
		/// </param>
		private static string DumpEnumerable(IEnumerable list, int indendation, string prefix)
		{
			StringBuilder sb = new StringBuilder();
			
			string countField = "Count";
			
			// for enumerable-types that are properties,
			// the prefix will be "wrong"
			if (prefix.EndsWith(" = "))
			{
				prefix = prefix.Remove(prefix.Length-3);
				countField = ".Count";
			}
			
			int index = 0;
			
			foreach (object cur in list)
			{
				sb.Append(cur.DumpToString(indendation, "{0}[{1}] = ", prefix, index));
				sb.AppendLine();
				index++;
			}
			
			sb.RemoveLastNewLine();
			
			string count = String.Format("{0}{1}{2} = {3}", MakeIndentation(indendation), prefix, countField, index);
			
			return String.Format("{0}{1}{2}", count, System.Environment.NewLine, sb.ToString());
		}
		#endregion Enumerable
		
		#region Helpers
		/// <summary>
		/// Makes a string of tabs
		/// </summary>
		/// <returns>
		/// String of tabs
		/// </returns>
		/// <param name="indendation">
		/// The number of tabs to include
		/// </param>
		private static string MakeIndentation(int indendation)
		{
			return "".PadLeft(indendation, '\t');
		}
		#endregion Helpers
	}
}