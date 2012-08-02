using System;
using NUnit.Framework;
using Missing.ObjectExtensions;
using Missing.DumpToStringHelpers.Enumerable;
using System.Collections.Generic;

namespace Missing
{
	[TestFixture]
	public partial class DumpToStringTests
	{
		#region Primitive list
		[Test]
		public void ListOfPrimitives()
		{
			List<string> obj = new List<string>() { "One", "Two", "Three" };
			
			string expected = @"Count = 3
[0] = 'One'
[1] = 'Two'
[2] = 'Three'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Primitive list

		#region Primitive list - empty
		[Test]
		public void ListOfPrimitives_Empty()
		{
			List<string> obj = new List<string>();
			
			string expected = @"Count = 0";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Primitive list - empty
		
		#region Primitive array
		[Test]
		public void ArrayOfPrimitives()
		{
			string[] obj = new string[] { "One", "Two", "Three" };
			
			string expected = @"Count = 3
[0] = 'One'
[1] = 'Two'
[2] = 'Three'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Primitive array

		#region Primitive array - empty
		[Test]
		public void ArrayOfPrimitives_Empty()
		{
			string[] obj = new string[3];
			
			string expected = @"Count = 3
[0] = null
[1] = null
[2] = null";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Primitive array - empty
		
		#region Class with primitive list
		[Test]
		public void ClassWithListOfPrimitives()
		{
			ClassWithPrimitiveEnumerable obj = new ClassWithPrimitiveEnumerable() {
				Strings = new List<string>() { "One", "Two", "Three" }
			};
			
			string expected = @"{
	Strings.Count = 3
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
	Strings.Count = 3
	Strings[0] = 'One'
	Strings[1] = 'Two'
	Strings[2] = 'Three'
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Class with primitive array
		
		#region Non-primitive array
		[Test]
		public void ArrayOfNonPrimitive()
		{
			Simple[] obj = new Simple[] {
				new Simple() { String = "One" },
				new Simple() { String = "Two" },
				new Simple() { String = "Three" }
			};
			
			string expected = @"Count = 3
[0] = {
	String = 'One'
}
[1] = {
	String = 'Two'
}
[2] = {
	String = 'Three'
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Non-primitive array
		
		#region Non-primitive list
		[Test]
		public void ListOfNonPrimitive()
		{
			List<Simple> obj = new List<Simple>() {
				new Simple() { String = "One" },
				new Simple() { String = "Two" },
				new Simple() { String = "Three" }
			};
			
			string expected = @"Count = 3
[0] = {
	String = 'One'
}
[1] = {
	String = 'Two'
}
[2] = {
	String = 'Three'
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		#endregion Non-primitive list
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
	
	public class Simple
	{
		public string String { get; set; }
	}
}