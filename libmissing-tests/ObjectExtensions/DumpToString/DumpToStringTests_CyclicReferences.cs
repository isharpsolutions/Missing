using System;
using NUnit.Framework;
using Missing.ObjectExtensions;
using Missing.DumpToStringHelpers.CyclicStuff;

namespace Missing
{
	[TestFixture]
	public partial class DumpToStringTests
	{
		[Test]
		public void CyclicReferences()
		{
			Parent obj = new Parent() {
				Name = "Old Fart",
				Child = new Child() {
					Name = "Young Fart"
				}
			};
			
			obj.Child.Parent = obj;
			
			string expected = @"{
	Name = 'Old Fart'
	Child = {
		Name = 'Young Fart'
		Parent = --cyclic--
	}
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
	}
}


namespace Missing.DumpToStringHelpers.CyclicStuff
{
	public class Parent
	{
		public string Name { get; set; }
		public Child Child { get; set; }
	}
	
	public class Child
	{
		public string Name { get; set; }
		public Parent Parent { get; set; }
	}
}