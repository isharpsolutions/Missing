using System;
using NUnit.Framework;
using JsonTest.MultipleLevels;
using Missing.Json;

namespace Missing
{
	[TestFixture]
	public class ToJsonTests_MultipleLevels
	{
		[Test]
		public void TwoLevels()
		{
			var obj = new Foo();
			
			string expected = "{\"Decimal\":2904.87,\"Bla\":{\"String\":\"Missing is awesome\",\"Int\":29}}";
			
			Assert.AreEqual(expected, obj.ToJson());
		}
	}
}

namespace JsonTest.MultipleLevels
{
	public class Bla
	{
		public Bla()
		{
			this.String = "Missing is awesome";
			this.Int = 29;
		}
		
		public string String { get; set; }
		public int Int { get; set; }
	}
	
	public class Foo
	{
		public Foo()
		{
			this.Decimal = 2904.87m;
			this.Bla = new Bla();
		}
		
		public decimal Decimal { get; set; }
		public Bla Bla { get; set; }
	}
}