using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Missing.Reflection.Extensions;
using Missing.Text.Extensions;
using System.Linq;
using Missing.Diagnostics;

namespace Missing.ObjectExtensions
{
	/// <summary>
	/// "Dump to string" extension for <see cref="Object"/>
	/// </summary>
	public static class DumpToStringObjectExtension
	{
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
			ObjectDumper dumper = new ObjectDumper();
			
			return dumper.Dump(obj);
		}
	}
}