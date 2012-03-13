using System;
using System.Reflection;
using System.Collections.Generic;

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
				Console.WriteLine("Assembly: '{0}'", ass.FullName);
				
				if (ass.FullName.StartsWith("libmissing-tests"))
				{
					Type[] allTypes = ass.GetTypes();
					foreach (Type t in allTypes)
					{
						Console.WriteLine(t.FullName);
					}
				}
				
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
		
		[ObsoleteAttribute("Do not use this method... It only exists temporarily while implementing Validation")]
		public static Type GetTypeEndsWith(string name, bool doLookInSystem)
		{
			PrepareListOfAssemblies();
			
			Type result = null;
			Type[] allTypes;
			
			foreach (Assembly ass in loadedAssemblies)
			{
				if (!doLookInSystem)
				{
					if (ass.FullName.StartsWith("System") || ass.FullName.StartsWith("mscorlib"))
					{
						continue;
					}
				}
				
				allTypes = ass.GetTypes();
				
				foreach (Type t in allTypes)
				{
					Console.WriteLine(t.FullName);
					if (t.FullName.EndsWith(name))
					{
						result = t;
						break;
					}
				}

				if (result != null)
				{
					break;
				}
			}
			
			if (result == null)
			{
				throw new ArgumentException(String.Format("There is no type '{0}' in any of the loaded assemblies.", name));
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
		
		/// <summary>
		/// Get the PropertyInfo from given type, following the "property path"
		/// defined as property names in a list of strings.
		/// </summary>
		/// <returns>
		/// The property info or default(PropertyInfo)
		/// </returns>
		/// <param name="t">
		/// The initial type
		/// </param>
		/// <param name="path">
		/// The names of each property to follow
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if <paramref name="path"/> has a count of 0
		/// </exception>
		/// <example>
		/// <code>
		/// public class Child
		/// {
		/// 	public string Name { get; set; }
		/// }
		/// 
		/// public class Parent
		/// {
		/// 	public Child Child { get; set; }
		/// }
		/// 
		/// PropertyInfo pi = TypeHelper.GetPropertyInfo(typeof(Parent), new List<string>() { "Child", "Name" });
		/// pi.Name ==> "Name"
		/// </code>
		/// </example>
		public static PropertyInfo GetPropertyInfo(Type t, IList<string> path)
		{
			if (path.Count == 0)
			{
				throw new ArgumentException("The path is empty");
			}
			
			Type curT = t;
			PropertyInfo pi = default(PropertyInfo);
			
			for (int i=0; i<path.Count; i++)
			{
				pi = curT.GetProperty(path[i]);
				curT = pi.PropertyType;
			}
			
			return pi;
		}
	}
}