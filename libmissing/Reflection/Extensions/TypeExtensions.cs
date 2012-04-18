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
		
		/// <summary>
		/// Check whether the type implements a specific interface
		/// </summary>
		/// <returns>
		/// <c>True</c> if the type implements the given interface, <c>False</c> otherwise
		/// </returns>
		/// <param name="type">
		/// The type instance on which to run the check
		/// </param>
		/// <param name="interfaceType">
		/// The type of the interface to check for
		/// </param>
		public static bool ImplementsInterface(this Type type, Type interfaceType)
		{
			Type[] faces = type.GetInterfaces();
			
			foreach (Type cur in faces)
			{
				if (cur == interfaceType)
				{
					return true;
				}
			}
			
			return false;
			
			/* We use a loop on GetInterfaces
			 * instead of GetInterface(string), because
			 * the latter does not catch generic interfaces
			 * like IList<String>
			 */
		}
	}
}