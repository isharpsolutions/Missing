using System;

namespace Missing.Reflection.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="System.Type"/>
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Get the name of the type without the generic part.
		/// 
		/// <example>
		/// 	<code language="csharp">
		/// 	Type genericType = typeof(List<String>);
		/// 	genericType.Name; // => List`1
		/// 	genericType.GetNonGenericName(); // => List
		/// 	</code>
		/// </example>
		/// </summary>
		/// <returns>
		/// The non generic name.
		/// </returns>
		public static string GetNonGenericName(this Type type)
		{
			if (type.IsGenericType)
			{
				int offset = type.Name.IndexOf("`");
				return type.Name.Substring(0, offset);
			}
			
			return type.Name;
		}
	}
}