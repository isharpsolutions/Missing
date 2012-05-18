using System;
using System.Collections.Generic;

namespace Missing.Reflection
{
	/// <summary>
	/// Helpers for reflection work that requires knowledge
	/// about "primitives" (int, string etc)
	/// </summary>
	public static class Primitives
	{
		/// <summary>
		/// List of types recognized as primitives
		/// </summary>
		public static List<Type> List = new List<Type>() {
			typeof(Int16),
			typeof(Int32),
			typeof(Int64),
			typeof(bool),
			typeof(string),
			typeof(decimal),
			typeof(float),
			typeof(double),
			typeof(byte),
			typeof(sbyte),
			typeof(UInt16),
			typeof(UInt32),
			typeof(UInt64),
			typeof(char)
		};
		
		/// <summary>
		/// Check whether a given type is primitive
		/// </summary>
		/// <returns>
		/// <c>true</c> if the given type is recognized as primitive; otherwise, <c>false</c>.
		/// </returns>
		/// <param name="type">
		/// The type to check
		/// </param>
		public static bool IsPrimitive(Type t)
		{
			return List.Contains(t);
		}
	}
}