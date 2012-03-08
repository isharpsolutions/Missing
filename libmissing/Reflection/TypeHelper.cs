using System;
using System.Reflection;

namespace Missing.Reflection
{
	/// <summary>
	/// Methods that helps the work with <see cref="System.Type"/>
	/// </summary>
	public static class TypeHelper
	{
		/// <summary>
		/// The loaded assemblies
		/// </summary>
		private static Assembly[] loadedAssemblies = null;
		
		/// <summary>
		/// Prepares the list of loaded assemblies
		/// </summary>
		private static void PrepareListOfAssemblies()
		{
			if (loadedAssemblies == null)
			{
				loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			}
		}
		
		/// <summary>
		/// Get a <see cref="Type"/> instance for a given type name
		/// </summary>
		/// <param name="name">
		/// A <see cref="String"/> with the full name of the wanted type
		/// </param>
		/// <returns>
		/// The <see cref="Type"/>
		/// </returns>
		/// <exception cref='ArgumentException'>
		/// Thrown if the type specified by <paramref name="name"/> could
		/// not be found in any of the loaded assemblies
		/// </exception>
		public static Type GetType(string name)
		{
			PrepareListOfAssemblies();
			
			Type result = null;
			
			foreach (Assembly ass in loadedAssemblies)
			{
				result = ass.GetType(name, false, true);
				if (result != null)
				{
					break;
				}
			}
			
			if (result == null)
			{
				throw new ArgumentException(String.Format("There is no type '{0}' in any of the loaded assemblies. Remember that the name must also contain the namespace.", name));
			}
			
			return result;
		}
		
		/// <summary>
		/// Create an instance of a specific type
		/// </summary>
		/// <returns>
		/// The instance.
		/// </returns>
		/// <param name="type">
		/// The fully qualified name of the type to instantiate
		/// </param>
		/// <typeparam name="T">
		/// The type of instance to return
		/// </typeparam>
		/// <remarks>
		/// This is just a convenience overload for
		/// res = GetType(type)
		/// CreateInstance(res)
		/// </remarks>
		public static T CreateInstance<T>(string type)
		{
			return CreateInstance<T>(GetType(type));
		}
		
		/// <summary>
		/// Create an instance of a specific type
		/// </summary>
		/// <returns>
		/// The instance.
		/// </returns>
		/// <param name="type">
		/// The type to instantiate
		/// </param>
		/// <typeparam name="T">
		/// The type of instance to return
		/// </typeparam>
		public static T CreateInstance<T>(Type type)
		{
			return (T)Activator.CreateInstance(type);
		}
	}
}