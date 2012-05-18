using System;
using NUnit.Framework;
using Missing.Json;
using JsonTest;

namespace Missing
{
	[TestFixture]
	public class ToJsonTests
	{
		#region String
		[Test]
		public void String_Empty()
		{
			var obj = new StringContainer() { MyString = String.Empty };
			Assert.AreEqual("{\"MyString\":\"\"}", obj.ToJson());
		}
		
		[Test]
		public void String_Null()
		{
			var obj = new StringContainer() { MyString = null };
			Assert.AreEqual("{\"MyString\":null}", obj.ToJson());
		}
		
		[Test]
		public void String_OnlySimpleChars()
		{
			var obj = new StringContainer() { MyString = "Missing is awesome" };
			Assert.AreEqual("{\"MyString\":\"Missing is awesome\"}", obj.ToJson());
		}
		
		[Test]
		public void String_WithNewline()
		{
			var obj = new StringContainer() { MyString = @"Missing is\nawesome" };
			Assert.AreEqual("{\"MyString\":\"Missing is\\nawesome\"}", obj.ToJson(), "Unix newline fails");
			
			obj = new StringContainer() { MyString = @"Missing is\rawesome" };
			Assert.AreEqual("{\"MyString\":\"Missing is\\nawesome\"}", obj.ToJson(), "Mac newline fails");
			
			obj = new StringContainer() { MyString = @"Missing is\r\nawesome" };
			Assert.AreEqual("{\"MyString\":\"Missing is\\nawesome\"}", obj.ToJson(), "Windows newline fails");
			
			obj = new StringContainer() { MyString = @"Missing\nis\r\nawesome\ryeah!" };
			Assert.AreEqual("{\"MyString\":\"Missing\\nis\\nawesome\\nyeah!\"}", obj.ToJson(), "All combined newline fails");
		}
		
		[Test]
		public void String_Quotes()
		{
			var obj = new StringContainer() { MyString = "Double \"quotes\"" };
			Assert.AreEqual("{\"MyString\":\"Double \\\"quotes\\\"\"}", obj.ToJson(), "Double quotes fail");
			
			obj = new StringContainer() { MyString = "Single 'quotes'" };
			Assert.AreEqual("{\"MyString\":\"Single 'quotes'\"}", obj.ToJson(), "Single quotes fail");
		}
		
		[Test]
		public void String_Tab()
		{
			var obj = new StringContainer() { MyString = "With \ttab" };
			Assert.AreEqual("{\"MyString\":\"With \\ttab\"}", obj.ToJson());
		}
		
		[Test]
		public void String_Backspace()
		{
			var obj = new StringContainer() { MyString = "With \bbackspace" };
			Assert.AreEqual("{\"MyString\":\"With \\bbackspace\"}", obj.ToJson());
		}
		
		[Test]
		public void String_Formfeed()
		{
			var obj = new StringContainer() { MyString = "With \fformfeed" };
			Assert.AreEqual("{\"MyString\":\"With \\fformfeed\"}", obj.ToJson());
		}
		
		[Test]
		public void String_Backslash()
		{
			var obj = new StringContainer() { MyString = "With \\ backslash" };
			Assert.AreEqual("{\"MyString\":\"With \\\\ backslash\"}", obj.ToJson());
		}
		#endregion
		
		#region Integer types
		[Test]
		public void Integers_Zero()
		{
			var obj = new IntegerTypesContainer() {
				MyInt = 0,
				MyLong = 0L,
				MyFloat = 0f,
				MyDouble = 0d,
				MyDecimal = 0m
			};
			
			string expected = "{\"MyInt\":0,\"MyLong\":0,\"MyFloat\":0,\"MyDouble\":0,\"MyDecimal\":0}";
			
			Assert.AreEqual(expected, obj.ToJson());
		}
		
		[Test]
		public void Integers_PositiveValue()
		{
			var obj = new IntegerTypesContainer() {
				MyInt = 10,
				MyLong = 10L,
				MyFloat = 10.3f,
				MyDouble = 10.3d,
				MyDecimal = 10.3m
			};
			
			string expected = "{\"MyInt\":10,\"MyLong\":10,\"MyFloat\":10.3,\"MyDouble\":10.3,\"MyDecimal\":10.3}";
			
			Assert.AreEqual(expected, obj.ToJson());
		}
		
		[Test]
		public void Integers_NegativeValue()
		{
			var obj = new IntegerTypesContainer() {
				MyInt = -10,
				MyLong = -10L,
				MyFloat = -10.3f,
				MyDouble = -10.3d,
				MyDecimal = -10.3m
			};
			
			string expected = "{\"MyInt\":-10,\"MyLong\":-10,\"MyFloat\":-10.3,\"MyDouble\":-10.3,\"MyDecimal\":-10.3}";
			
			Assert.AreEqual(expected, obj.ToJson());
		}
		#endregion
	}
}

namespace JsonTest
{
	public class StringContainer
	{
		public string MyString { get; set; }
	}
	
	public class IntegerTypesContainer
	{
		public int MyInt { get; set; }
		public long MyLong { get; set; }
		public float MyFloat { get; set; }
		public double MyDouble { get; set; }
		public decimal MyDecimal { get; set; }
	}
}