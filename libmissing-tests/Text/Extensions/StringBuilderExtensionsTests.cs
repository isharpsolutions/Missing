using System;
using NUnit.Framework;
using Missing.Text.Extensions;
using System.Text;

namespace Missing
{
	[TestFixture]
	public class StringBuilderExtensionsTests
	{
		[Test]
		public void RemoveLast()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Something awesome!!");
			
			string expected = "Something awesome!";
			
			sb.RemoveLast("!");
			
			Assert.AreEqual(expected, sb.ToString());
		}
		
		[Test]
		public void RemoveLastNewLine()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Something awesome!");
			sb.AppendLine();
			
			string expected = "Something awesome!";
			
			sb.RemoveLastNewLine();
			
			Assert.AreEqual(expected, sb.ToString());
		}
		
		[Test]
		public void RemoveLastComma()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Something awesome!");
			sb.Append(",");
			
			string expected = "Something awesome!";
			
			sb.RemoveLastComma();
			
			Assert.AreEqual(expected, sb.ToString());
		}

		[Test]
		public void RemoveLastEmptyInstance()
		{
			StringBuilder sb = new StringBuilder();

			string expected = "";
			
			sb.RemoveLast("!");
			
			Assert.AreEqual(expected, sb.ToString());
		}

		[Test]
		public void RemoveLastExactMatch()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("!");

			string expected = "";
			
			sb.RemoveLast("!");
			
			Assert.AreEqual(expected, sb.ToString());
		}
	}
}