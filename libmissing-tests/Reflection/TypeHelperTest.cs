using System;
using System.Reflection;
using Missing.Reflection;
using NUnit.Framework;

namespace Missing
{
	public class TypeHelperTestType
	{
		public TypeHelperTestType()
		{
		}
	}
	
	[TestFixture]
	public class TypeHelperTest
	{
		#region Get Type
		[Test]
		public void GetType_FullName_Exists()
		{
			Type t = TypeHelper.GetType("Missing.TypeHelperTestType");
			
			Assert.AreEqual("Missing.TypeHelperTestType", t.FullName, "FullName is wrong");
		}
		
		[Test]
		public void GetType_PartialName_Exists()
		{
			try
			{
				TypeHelper.GetType("TypeHelperTestType");
				Assert.Fail("An ArgumentException should have been thrown");
			}
			
			catch (ArgumentException)
			{
				// good
			}
		}
		
		[Test]
		public void GetType_FullName_DoesNotExist()
		{
			try
			{
				TypeHelper.GetType("Something.That.Does.Not.Exist");
				Assert.Fail("An ArgumentException should have been thrown");
			}
			
			catch (ArgumentException)
			{
				// good
			}
		}
		#endregion Get Type
	}
}