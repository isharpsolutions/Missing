using System;
using NUnit.Framework;
using Missing.ObjectExtensions;

namespace Missing
{
	[TestFixture]
	public class DumpToStringTests_Primitives
	{
		[Test]
		public void String()
		{
			string obj = "String";
			
			string expected = "'String'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Int()
		{
			int obj = 5;
			
			string expected = "5";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Long()
		{
			long obj = 5L;
			
			string expected = "5";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Float()
		{
			float obj = 5.2f;
			
			string expected = "5.2";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Double()
		{
			double obj = 5.2d;
			
			string expected = "5.2";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Decimal()
		{
			decimal obj = 5.2m;
			
			string expected = "5.2";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void DateTime()
		{
			DateTime obj = new DateTime(2012, 12, 31, 13, 5, 2);
			
			string expected = "'12/31/2012 1:05:02 PM'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Bool()
		{
			bool obj = true;
			
			string expected = "True";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Null()
		{
			string obj = null;
			
			string expected = "null";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Byte()
		{
			byte obj = 23;
			
			string expected = "23";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Char()
		{
			char obj = 'c';
			
			string expected = "'c'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		[Test]
		public void Enum()
		{
			TestEnum obj = TestEnum.Option1;
			
			string expected = "'Option1'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
		
		
		
		
		
		private enum TestEnum
		{
			Option1 = 2
		}
	}
}