using System;
using System.Collections.Generic;
using Missing.Validation;
using NUnit.Framework;
using System.Linq;

namespace Missing
{
	[TestFixture]
	public class ValidationSpecification_GetPropertyPathTests
	{
		public enum GetPropertyPathTestModelEnum
		{
			KungFu,
			
			Ninja
		}
		
		public class GetPropertyPathTestModel
		{
			public string MyString { get; set; }
			public int MyInt { get; set; }
			public long MyLong { get; set; }
			public bool MyBool { get; set; }
			public decimal MyDecimal { get; set; }
			public float MyFloat { get; set; }
			public double MyDouble { get; set; }
			public GetPropertyPathTestModelEnum MyEnum { get; set; }
		}
		
		[Test]
		public void FirstLevel_String()
		{
			List<string> path = (List<string>)ValidationSpecification.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyString);
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyString", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Int()
		{
			List<string> path = (List<string>)ValidationSpecification.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyInt);
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyInt", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Long()
		{
			List<string> path = (List<string>)ValidationSpecification.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyLong);
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyLong", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Bool()
		{
			List<string> path = (List<string>)ValidationSpecification.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyBool);
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyBool", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Decimal()
		{
			List<string> path = (List<string>)ValidationSpecification.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyDecimal);
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyDecimal", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Float()
		{
			List<string> path = (List<string>)ValidationSpecification.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyFloat);
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyFloat", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Double()
		{
			List<string> path = (List<string>)ValidationSpecification.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyDouble);
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyDouble", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Enum()
		{
			List<string> path = (List<string>)ValidationSpecification.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyEnum);
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyEnum", path[0], "Element is wrong");
		}
	}
}

