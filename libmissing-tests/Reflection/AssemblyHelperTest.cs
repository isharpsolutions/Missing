using System;
using System.Reflection;
using Missing.Reflection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Missing
{
	[TestFixture]
	public class AssemblyHelperTest
	{
		[Test]
		public void GetAssemblies_NoFilter()
		{
			Assembly[] assemblies = AssemblyHelper.GetAssemblies();
			
			Assert.IsNotEmpty(assemblies, "There should be loaded assemblies");
			
			AssemblyCollection coll = new AssemblyCollection(assemblies);
			
			Assert.IsTrue(coll.Contains("mscorlib"));
			Assert.IsTrue(coll.Contains("Missing"));
		}
		
		[Test]
		public void GetAssemblies_WithFilter()
		{
			Assembly[] assemblies = AssemblyHelper.GetAssemblies(y => y.FullName.StartsWith("Missing"));
			
			Assert.IsNotEmpty(assemblies, "There should be loaded assemblies");
			
			AssemblyCollection coll = new AssemblyCollection(assemblies);
			
			Assert.IsFalse(coll.Contains("mscorlib"));
			Assert.IsTrue(coll.Contains("Missing"));
		}
		
		
		#region Helper class
		private class AssemblyCollection : System.Collections.ObjectModel.Collection<Assembly>
		{
			public AssemblyCollection() : base()
			{
			}
			
			public AssemblyCollection(Assembly[] assemblies) : this()
			{
				foreach (Assembly cur in assemblies)
				{
					base.Add(cur);
				}
			}
			
			public bool Contains(string assemblyName)
			{
				return	(from yy in base.Items
				        where yy.FullName.StartsWith(assemblyName)
				        select yy).FirstOrDefault() != default(Assembly);
			}
		}
		#endregion
	}
}