using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Missing.Reflection
{
	/// <summary>
	/// Methods that helps the work with <see cref="System.Reflection.Assembly"/>
	/// </summary>
	public static class AssemblyHelper
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
		/// Get an array of loaded assemblies
		/// </summary>
		/// <returns>
		/// The assemblies.
		/// </returns>
		public static Assembly[] GetAssemblies()
		{
			PrepareListOfAssemblies();
			
			return loadedAssemblies;
		}
		
		/// <summary>
		/// Get an array of loaded assemblies filtered
		/// by a given predicate.
		/// </summary>
		/// <returns>
		/// The assemblies matching the predicate
		/// </returns>
		/// <param name="predicate">
		/// The predicate that an assembly must match
		/// to be included
		/// </param>
		public static Assembly[] GetAssemblies(Predicate<Assembly> predicate)
		{
			IEnumerable<Assembly> assemblies =	from yy in loadedAssemblies
												where predicate(yy)
				                  				select yy;
			
			return assemblies.Cast<Assembly>().ToArray();
		}
	}
}