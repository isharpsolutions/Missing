using System;
using NUnit.Framework;
using Missing.ObjectExtensions;
using Missing.DumpToStringHelpers.PropertyWithClass;

namespace Missing
{
	[TestFixture]
	public partial class DumpToStringTests
	{
		[Test]
		public void ClassAsProperty()
		{
			WrapperClass obj = new WrapperClass() {
				String = "Defined in wrapper",
				Sub = new ClassWithPrimitiveProperties() {
					Bool = false,
					Byte = 23,
					Char = 'A',
					DateTime = new DateTime(2012, 12, 31, 13, 5, 2),
					Decimal = -34.1m,
					Double = 75.3d,
					Enum = TestEnum.TheOption,
					Float = 764.3f,
					Int = 29,
					Long = 9999999999L,
					String = "String value",
					Null = null
				}
			};
			
			string expected = @"{
	String = 'Defined in wrapper'
	Sub = {
		String = 'String value'
		Char = 'A'
		Int = 29
		Long = 9999999999
		Float = 764.3
		Double = 75.3
		Decimal = -34.1
		DateTime = '12/31/2012 1:05:02 PM'
		Bool = False
		Byte = 23
		Enum = 'TheOption'
		Null = null
	}
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
	}
}


namespace Missing.DumpToStringHelpers.PropertyWithClass
{
	public class WrapperClass
	{
		public string String { get; set; }
		public ClassWithPrimitiveProperties Sub { get; set; }
	}
	
	public class ClassWithPrimitiveProperties
	{
		public string String { get; set; }
		public char Char { get; set; }
		public int Int { get; set; }
		public long Long { get; set; }
		public float Float { get; set; }
		public double Double { get; set; }
		public decimal Decimal { get; set; }
		public DateTime DateTime { get; set; }
		public bool Bool { get; set; }
		public byte Byte { get; set; }
		public TestEnum Enum { get; set; }
		public string Null { get; set; }
	}
	
	public enum TestEnum
	{
		TheOption
	}
}