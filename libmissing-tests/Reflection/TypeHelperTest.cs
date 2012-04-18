using System;
using System.Reflection;
using Missing.Reflection;
using NUnit.Framework;
using System.Collections.Generic;

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
	
	public class TypeHelperTestTypeWrapper
	{
		public TypeHelperTestTypeWrapper()
		{
			this.Child = new TypeHelperTestType() {
				WhosYourDaddy = "My mommy"
			};
		}
		
		public TypeHelperTestType Child { get; set; }
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
		
		[Test]
		public void GetType_PartialNamePredicate_Exists()
		{
			Type t = TypeHelper.GetType(y => y.FullName.EndsWith("TypeHelperTestType"));
			
			Assert.AreEqual("Missing.TypeHelperTestType", t.FullName, "FullName is wrong");
		}
		
		[Test]
		public void GetType_PartialNamePredicate_DoesNotExist()
		{
			try
			{
				TypeHelper.GetType(y => y.FullName.EndsWith("DoesNotExist"));
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
		
		#region GetPropertyData
		[Test]
		public void GetPropertyData_EmptyPath()
		{
			try
			{
				TypeHelper.GetPropertyData(new TypeHelperTestType(), new List<string>());
				Assert.Fail("An ArgumentException should have been thrown");
			}
			
			catch (ArgumentException)
			{
				// good :)
			}
		}
		
		[Test]
		public void GetPropertyData_OneLevel()
		{
			TypeHelperTestType input = new TypeHelperTestType() {
				WhosYourDaddy = "Spock"
			};
			
			PropertyData data = TypeHelper.GetPropertyData(input, new List<string>() { "WhosYourDaddy" });
			
			Assert.AreEqual("WhosYourDaddy", data.PropertyInfo.Name, "Name is wrong");
			Assert.AreEqual("Spock", data.Value, "Value is wrong");
		}
		
		[Test]
		public void GetPropertyData_TwoLevels()
		{
			TypeHelperTestTypeWrapper input = new TypeHelperTestTypeWrapper() {
				Child = new TypeHelperTestType() {
					WhosYourDaddy = "Spock"
				}
			};
			
			PropertyData data = TypeHelper.GetPropertyData(input, new List<string>() { "Child", "WhosYourDaddy" });
			
			Assert.AreEqual("WhosYourDaddy", data.PropertyInfo.Name, "Name is wrong");
			Assert.AreEqual("Spock", data.Value, "Value is wrong");
		}
		#endregion GetPropertyData
		
		#region Get non-generic name
		[Test]
		public void GetNonGenericName_NormalType()
		{
			Assert.AreEqual("String", TypeHelper.GetNonGenericName(typeof(String)), "The name is wrong");
		}
		
		[Test]
		public void GetNonGenericName_OneTypeParams()
		{
			Assert.AreEqual("List", TypeHelper.GetNonGenericName(typeof(List<string>)), "The name is wrong");
		}
		
		[Test]
		public void GetNonGenericName_TwoTypeParams()
		{
			Assert.AreEqual("Dictionary", TypeHelper.GetNonGenericName(typeof(Dictionary<string, string>)), "The name is wrong");
		}
		#endregion
	}
}