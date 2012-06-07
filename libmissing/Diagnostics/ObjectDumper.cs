using System;
using System.Collections.Generic;
using Missing.Reflection.Extensions;
using System.Collections;
using System.Text;
using System.Reflection;
using Missing.Text.Extensions;

namespace Missing.Diagnostics
{
	/// <summary>
	/// This class knows how to "dump" an entire class instance
	/// to a string
	/// </summary>
	public class ObjectDumper
	{
		private static Dictionary<string, Action<Type, string>> SpecialHandlers = new Dictionary<string, Action<Type, string>>();
		
		#region Static
		public static void AddSpecialCase(Type forType, Action<Type, string> action)
		{
			SpecialHandlers.Add(forType.FullName, action);
		}
		#endregion Static
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Diagnostics.ObjectDumper"/> class.
		/// </summary>
		public ObjectDumper()
		{
		}
		#endregion Constructors
		
		#region Dump
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
		private string Dump(object obj, int indendation, string prefix)
		{
			string indent = this.MakeIndentation(indendation);
			
			if (obj == null)
			{
				return String.Format("{0}{1}{2}", indent, prefix, "null");
			}
			
			Type t = obj.GetType();
			
			if (t.ImplementsInterface(typeof(IEnumerable)) && t != typeof(String))
			{
				return this.DumpEnumerable((IEnumerable)obj, indendation, prefix);
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
			return String.Format("{0}{1}{2}", indent, prefix, this.DumpNonPrimitiveType(t, obj, indendation));
		}
		#endregion Dump
		
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
		private string DumpNonPrimitiveType(Type t, object obj, int indendation)
		{
			StringBuilder sb = new StringBuilder();
			
			string indent = this.MakeIndentation(indendation);
			
			sb.AppendLine("{");

			PropertyInfo[] properties = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			
			object val;
			
			foreach (PropertyInfo pi in properties)
			{
				val = pi.GetValue(obj, null);
				
				sb.Append(this.Dump(val, indendation+1, "{0} = ", pi.Name));
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
		private string DumpEnumerable(IEnumerable list, int indendation, string prefix)
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
				sb.Append(this.Dump(cur, indendation, "{0}[{1}] = ", prefix, index));
				sb.AppendLine();
				index++;
			}
			
			sb.RemoveLastNewLine();
			
			string count = String.Format("{0}{1}{2} = {3}", this.MakeIndentation(indendation), prefix, countField, index);
			
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
		private string MakeIndentation(int indendation)
		{
			return String.Empty.PadLeft(indendation, '\t');
		}
		#endregion Helpers
		
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
		public string Dump(object obj)
		{
			return this.Dump(obj, 0);
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
		private string Dump(object obj, int indendation)
		{
			return this.Dump(obj, indendation, String.Empty);
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
		private string Dump(object obj, int indendation, string prefixFormat, params object[] prefixArgs)
		{
			return this.Dump(obj, indendation, String.Format(prefixFormat, prefixArgs));
		}
		#endregion Overloads
	}
}