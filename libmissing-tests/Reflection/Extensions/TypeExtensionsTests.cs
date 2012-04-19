using System;
using NUnit.Framework;
using Missing.Reflection;
using Missing.Reflection.Extensions;
using System.Collections.Generic;
using System.Collections;

namespace Missing
{
	[TestFixture]
	public class TypeExtensionsTests
	{
		#region Get non-generic name
		[Test]
		public void GetNonGenericName_NormalType()
		{
			Assert.AreEqual("String", typeof(String).GetNonGenericName(), "The name is wrong");
		}
		
		[Test]
		public void GetNonGenericName_OneTypeParams()
		{
			Assert.AreEqual("List", typeof(List<String>).GetNonGenericName(), "The name is wrong");
		}
		
		[Test]
		public void GetNonGenericName_TwoTypeParams()
		{
			Assert.AreEqual("Dictionary", typeof(Dictionary<string, string>).GetNonGenericName(), "The name is wrong");
		}
		#endregion
		
		#region Implements interface
		[Test]
		public void ImplementsInterface_Yes()
		{
			Assert.IsTrue(typeof(List<String>).ImplementsInterface(typeof(IEnumerable)));
		}
		
		[Test]
		public void ImplementsInterface_Generic_Yes()
		{
			Assert.IsTrue(typeof(List<String>).ImplementsInterface(typeof(IList<String>)));
		}
		
		[Test]
		public void ImplementsInterface_No()
		{
			Assert.IsFalse(typeof(List<String>).ImplementsInterface(typeof(IServiceProvider)));
		}
		#endregion
	}
}