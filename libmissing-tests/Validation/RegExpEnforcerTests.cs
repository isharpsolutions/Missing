using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Missing.Validation;

namespace Missing
{
	[TestFixture]
	public class RegExpEnforcerTests
	{
		[Test]
		public void RegexNotSet()
		{
			try
			{
				(new RegExpEnforcer()).Check("my input");
				
				Assert.Fail("An InvalidOperationException should have been thrown");
			}
			
			catch (InvalidOperationException)
			{
			}
		}
		
		[Test]
		public void InvalidInputType()
		{
			RegExpEnforcer enforcer = new RegExpEnforcer();
			
			enforcer.Regex = new Regex("");
			
			try
			{
				enforcer.Check(34);
				
				Assert.Fail("An ArgumentException should have been thrown");
			}
			
			catch (ArgumentException)
			{
			}
		}
		
		[Test]
		public void SimpleAbcValid()
		{
			RegExpEnforcer enforcer = new RegExpEnforcer();
			enforcer.Regex = new Regex("abc");
			
			string input = "abc";
			
			Assert.IsEmpty(enforcer.Check(input));
		}
		
		[Test]
		public void SimpleAbcInvalid()
		{
			RegExpEnforcer enforcer = new RegExpEnforcer();
			enforcer.Regex = new Regex("abc");
			
			string input = "def";
			
			Assert.IsNotEmpty(enforcer.Check(input));
		}
	}
}

