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
		/// <summary>
		/// Contains a table of special handlers
		/// </summary>
		private static Dictionary<string, Func<object, string>> SpecialHandlers = new Dictionary<string, Func<object, string>>();
		
		#region Static
		/// <summary>
		/// Add a special case (meaning a custom handler
		/// for a specific type).
		/// </summary>
		/// <example>
		/// When working with MongoDB we always run into the special
		/// ID format called an <c>ObjectId</c>. This consists of multiple
		/// properties that are encoded to give a single string.
		/// 
		/// When we dump an entity we want the ID (i.e. the string) - not the
		/// individual properties. Therefore we want to add a special handler
		/// for that case. We do this like so...
		/// 
		/// <code>
		/// Missing.Diagnostics.ObjectDumper.AddSpecialCase(typeof(ObjectId), y => {
		///		return System.String.Format("'{0}'", y.ToString());
		///	});
		/// </code>
		/// </example>
		/// <param name="forType">
		/// The type that this handler works on
		/// </param>
		/// <param name="action">
		/// The actual handling code
		/// </param>
		public static void AddSpecialCase(Type forType, Func<object, string> action)
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
		
		private HashSet<object> seenBefore = new HashSet<object>();
		
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
			
			//
			// handle cyclic references
			//
			if (!t.IsEnum && this.seenBefore.Contains(obj))
			{
				return String.Format("{0}{1}{2}", indent, prefix, "--cyclic--");
			}
			
			this.seenBefore.Add(obj);
			
			//
			// special cases / custom handlers
			//
			if (SpecialHandlers.ContainsKey(t.FullName))
			{
				return String.Format("{0}{1}{2}", indent, prefix, SpecialHandlers[t.FullName](obj));
			}
			
			//
			// ienumerable
			//
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

			// if the enuerable is empty, we do
			// not need to do anything else than
			// return the count - especially since
			// we never want to return with a newline
			// at the end
			if (index == 0)
			{
				return count;
			}
			
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