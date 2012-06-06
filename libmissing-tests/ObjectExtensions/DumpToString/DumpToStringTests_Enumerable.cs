using System;
using NUnit.Framework;
using Missing.ObjectExtensions;
using Missing.DumpToStringHelpers.Enumerable;
using System.Collections.Generic;

namespace Missing
{
	[TestFixture]
	public class DumpToStringTests_Enumerable
	{
		#region Primitive list
		[Test]
		public void ListOfPrimitives()
		{
			List<string> obj = new List<string>() { "One", "Two", "Three" };
			
			string expected = @"[0] = 'One'
[1] = 'Two'
[2] = 'Three'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Primitive list
		
		#region Primitive array
		[Test]
		public void ArrayOfPrimitives()
		{
			string[] obj = new string[] { "One", "Two", "Three" };
			
			string expected = @"[0] = 'One'
[1] = 'Two'
[2] = 'Three'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Primitive list
		
		#region Class with primitive list
		[Test]
		public void ClassWithListOfPrimitives()
		{
			ClassWithPrimitiveEnumerable obj = new ClassWithPrimitiveEnumerable() {
				Strings = new List<string>() { "One", "Two", "Three" }
			};
			
			string expected = @"{
	Strings[0] = 'One'
	Strings[1] = 'Two'
	Strings[2] = 'Three'
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Class with primitive list
		
		#region Class with primitive array
		[Test]
		public void ClassWithArrayOfPrimitives()
		{
			ClassWithArray obj = new ClassWithArray() {
				Strings = new string[] { "One", "Two", "Three" }
			};
			
			string expected = @"{
	Strings[0] = 'One'
	Strings[1] = 'Two'
	Strings[2] = 'Three'
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Class with primitive array
	}
}


namespace Missing.DumpToStringHelpers.Enumerable
{
	public class ClassWithPrimitiveEnumerable
	{
		public List<string> Strings { get; set; }
	}
	
	public class ClassWithArray
	{
		public string[] Strings { get; set; }
	}
}