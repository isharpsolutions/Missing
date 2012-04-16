using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using Missing.Reflection;

namespace Missing
{
	[TestFixture]
	public class PropertyPathWorkerTests
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
			
			public GetPropertyPathTestModelSub Sub { get; set; }
		}
		
		public class GetPropertyPathTestModelSub
		{
			public string SubString { get; set; }
			public int SubInt { get; set; }
			
			public GetPropertyPathTestModelSub2 Three { get; set; }
		}
		
		public class GetPropertyPathTestModelSub2
		{
			public int ThirdInt { get; set; }
		}
		
		#region First level
		[Test]
		public void FirstLevel_String()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyString).Parts;
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyString", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Int()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyInt).Parts;
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyInt", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Long()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyLong).Parts;
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyLong", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Bool()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyBool).Parts;
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyBool", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Decimal()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyDecimal).Parts;
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyDecimal", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Float()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyFloat).Parts;
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyFloat", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Double()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyDouble).Parts;
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyDouble", path[0], "Element is wrong");
		}
		
		[Test]
		public void FirstLevel_Enum()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.MyEnum).Parts;
			
			Assert.AreEqual(1, path.Count, "Path should contain 1 element");
			Assert.AreEqual("MyEnum", path[0], "Element is wrong");
		}
		#endregion First level
		
		#region Second level
		[Test]
		public void SecondLevel_String()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.Sub.SubString).Parts;
			
			Assert.AreEqual(2, path.Count, "Path should contain 2 elements");
			Assert.AreEqual("Sub", path[0], "First: Element is wrong");
			Assert.AreEqual("SubString", path[1], "Second: Element is wrong");
		}
		
		[Test]
		public void SecondLevel_Int()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.Sub.SubInt).Parts;
			
			Assert.AreEqual(2, path.Count, "Path should contain 2 elements");
			Assert.AreEqual("Sub", path[0], "First: Element is wrong");
			Assert.AreEqual("SubInt", path[1], "Second: Element is wrong");
		}
		#endregion Second level
		
		#region Third level
		[Test]
		public void ThirdLevel_Int()
		{
			List<string> path = (List<string>)PropertyPathWorker.GetPropertyPath<GetPropertyPathTestModel>(y => y.Sub.Three.ThirdInt).Parts;
			
			Assert.AreEqual(3, path.Count, "Path should contain 3 elements");
			Assert.AreEqual("Sub", path[0], "First: Element is wrong");
			Assert.AreEqual("Three", path[1], "Second: Element is wrong");
			Assert.AreEqual("ThirdInt", path[2], "Third: Element is wrong");
		}
		#endregion Third level
	}
}

