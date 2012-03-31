using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Missing.Reflection
{
	/// <summary>
	/// Methods that helps the work with <see cref="System.Type"/>
	/// </summary>
	public static class TypeHelper
	{
		/// <summary>
		/// Get the type matching the given predicate.
		/// 
		/// Only the supplied assemblies are searched.
		/// 
		/// Should multiple matches exist, only the first match is returned.
		/// </summary>
		/// <returns>
		/// The type.
		/// </returns>
		/// <param name="typePredicate">
		/// The predicate that the wanted type must match
		/// </param>
		/// <param name="assemblies">
		/// The set of assemblies to search through
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if a type matching the predicate could not be found
		/// </exception>
		/// <example>
		/// TypeHelper.GetType(assemblies, y => y.FullName.Equals("MyNamespace.MyType"));
		/// TypeHelper.GetType(assemblies, y => y.FullName.Endswith("MyType"));
		/// TypeHelper.GetType(assemblies, y => y.Name.Equals("MyType"));
		/// </example>
		public static Type GetType(Predicate<Type> typePredicate, Assembly[] assemblies)
		{
			Type[] allTypes;
			
			foreach (Assembly ass in assemblies)
			{
				allTypes = ass.GetTypes();
				
				foreach (Type t in allTypes)
				{
					
					if (typePredicate(t))
					{
						return t;
					}
				}
			}
			
			throw new ArgumentException("There was no mathing types in any of the assemblies");
		}
		
		/// <summary>
		/// Get the type matching the given predicate.
		/// 
		/// All loaded assemblies are searched.
		/// 
		/// Should multiple matches exist, only the first match is returned.
		/// </summary>
		/// <returns>
		/// The type.
		/// </returns>
		/// <param name="typePredicate">
		/// The predicate that the wanted type must match
		/// </param>
		public static Type GetType(Predicate<Type> typePredicate)
		{
			return GetType(typePredicate, AssemblyHelper.GetAssemblies());
		}
		
		
		/// <summary>
		/// Get the type with the given name.
		/// 
		/// All loaded assemblies are searched.
		/// </summary>
		/// <param name="name">
		/// A <see cref="String"/> with the full name of the wanted type.
		/// The name must match <see cref="System.Type.FullName"/>
		/// </param>
		/// <returns>
		/// The <see cref="Type"/>
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown if the type specified by <paramref name="name"/> could
		/// not be found in any of the loaded assemblies
		/// </exception>
		public static Type GetType(string name)
		{
			try
			{
				return GetType(y => y.FullName == name, AssemblyHelper.GetAssemblies());
			}
			
			catch (ArgumentException)
			{
				throw new ArgumentException(String.Format("There is no type '{0}' in any of the loaded assemblies. Remember that the name must also contain the namespace.", name));
			}
		}
		
		/// <summary>
		/// Get the type matching the given predicate.
		/// 
		/// Only the loaded assemblies matching the assembly filter are searched.
		/// 
		/// Should multiple matches exist, only the first match is returned.
		/// </summary>
		/// <returns>
		/// The type.
		/// </returns>
		/// <param name="typePredicate">
		/// The predicate that the wanted type must match
		/// </param>
		/// <param name="assemblyPredicate">
		/// The predicate that assemblies must match in order to be searched
		/// </param>
		public static Type GetType(Predicate<Type> typePredicate, Predicate<Assembly> assemblyPredicate)
		{
			return GetType(typePredicate, AssemblyHelper.GetAssemblies(assemblyPredicate));
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
		/// Get PropertyInfo and value from a given class instance, following the "property path"
		/// defined as property names in a list of strings.
		/// </summary>
		/// <returns>
		/// The property data.
		/// </returns>
		/// <param name="input">
		/// The class instance to traverse
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
		/// Parent input = new Parent() {
		/// 	Child = new Child() {
		/// 		Name = "Kirk"
		/// 	}
		/// };
		/// 
		/// PropertyData pd = TypeHelper.GetPropertyData(input, new List<string>() { "Child", "Name" });
		/// pd.PropertyInfo.Name ==> "Name"
		/// pd.Value ==> "Kirk"
		/// </code>
		/// </example>
		public static PropertyData GetPropertyData(object input, IList<string> path)
		{
			if (path.Count == 0)
			{
				throw new ArgumentException("The path is empty");
			}
			
			Type curT = input.GetType();
			
			PropertyData result = new PropertyData();
			result.Value = input;
			
			result.PropertyInfo = default(PropertyInfo);
			
			for (int i=0; i<path.Count; i++)
			{
				result.PropertyInfo = curT.GetProperty(path[i]);
				result.Value = result.PropertyInfo.GetValue(result.Value, null);
				
				curT = result.PropertyInfo.PropertyType;
			}
			
			return result;
		}
	}
}