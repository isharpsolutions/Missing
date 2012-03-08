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
			this.WhosYourDaddy = "You are!";
		}
		
		public string WhosYourDaddy { get; set; }
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
		
		#region Create Instance
		[Test]
		public void CreateInstance_FullName_Exists()
		{
			Type t = TypeHelper.GetType("Missing.TypeHelperTestType");
			
			//
			// full call
			//
			TypeHelperTestType actual = TypeHelper.CreateInstance<TypeHelperTestType>(t);
			
			Assert.AreEqual("Missing.TypeHelperTestType", actual.GetType().FullName, "Full call: Type.FullName is wrong");
			Assert.AreEqual("You are!", actual.WhosYourDaddy, "Full call: Property is wrong");
			
			
			//
			// convenience overload
			//
			TypeHelperTestType convenience = TypeHelper.CreateInstance<TypeHelperTestType>("Missing.TypeHelperTestType");
			
			Assert.AreEqual("Missing.TypeHelperTestType", convenience.GetType().FullName, "Convenience: Type.FullName is wrong");
			Assert.AreEqual("You are!", convenience.WhosYourDaddy, "Convenience: Property is wrong");
		}
		#endregion Create Instance
	}
}