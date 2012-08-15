using System;
using NUnit.Framework;
using Missing.ObjectExtensions;
using Missing.DumpToStringHelpers.ClassPrimitives;

namespace Missing
{
	[TestFixture]
	public partial class DumpToStringTests
	{
		[Test]
		public void ClassWithPrimitiveProperties()
		{
			ClassWithPrimitiveProperties obj = new ClassWithPrimitiveProperties() {
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
			};
			
			string expected = @"{
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
}";

			Assert.AreEqual(expected, obj.DumpToString());
		}


		[Test]
		public void ClassWithPrimitivePropertiesRepeated()
		{
			ClassWithPrimitivePropertiesRepeated obj = new ClassWithPrimitivePropertiesRepeated() {
				Bool = false,
				BoolRepeated = false,
				Byte = 23,
				ByteRepeated = 23,
				Char = 'A',
				CharRepeated = 'A',
				DateTime = new DateTime(2012, 12, 31, 13, 5, 2),
				DateTimeRepeated = new DateTime(2012, 12, 31, 13, 5, 2),
				Decimal = -34.1m,
				DecimalRepeated = -34.1m,
				Double = 75.3d,
				DoubleRepeated = 75.3d,
				Enum = TestEnum.TheOption,
				EnumRepeated = TestEnum.TheOption,
				Float = 764.3f,
				FloatRepeated = 764.3f,
				Int = 29,
				IntRepeated = 29,
				Long = 9999999999L,
				LongRepeated = 9999999999L,
				String = "String value",
				StringRepeated = "String value",
				Null = null
			};
			
			string expected = @"{
	String = 'String value'
	StringRepeated = 'String value'
	Char = 'A'
	CharRepeated = 'A'
	Int = 29
	IntRepeated = 29
	Long = 9999999999
	LongRepeated = 9999999999
	Float = 764.3
	FloatRepeated = 764.3
	Double = 75.3
	DoubleRepeated = 75.3
	Decimal = -34.1
	DecimalRepeated = -34.1
	DateTime = '12/31/2012 1:05:02 PM'
	DateTimeRepeated = '12/31/2012 1:05:02 PM'
	Bool = False
	BoolRepeated = False
	Byte = 23
	ByteRepeated = 23
	Enum = 'TheOption'
	EnumRepeated = 'TheOption'
	Null = null
}";

			Assert.AreEqual(expected, obj.DumpToString());
		}
	}
}


namespace Missing.DumpToStringHelpers.ClassPrimitives
{
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

	public class ClassWithPrimitivePropertiesRepeated
	{
		public string String { get; set; }
		public string StringRepeated { get; set; }
		public char Char { get; set; }
		public char CharRepeated { get; set; }
		public int Int { get; set; }
		public int IntRepeated { get; set; }
		public long Long { get; set; }
		public long LongRepeated { get; set; }
		public float Float { get; set; }
		public float FloatRepeated { get; set; }
		public double Double { get; set; }
		public double DoubleRepeated { get; set; }
		public decimal Decimal { get; set; }
		public decimal DecimalRepeated { get; set; }
		public DateTime DateTime { get; set; }
		public DateTime DateTimeRepeated { get; set; }
		public bool Bool { get; set; }
		public bool BoolRepeated { get; set; }
		public byte Byte { get; set; }
		public byte ByteRepeated { get; set; }
		public TestEnum Enum { get; set; }
		public TestEnum EnumRepeated { get; set; }
		public string Null { get; set; }
	}
	
	public enum TestEnum
	{
		TheOption
	}
}