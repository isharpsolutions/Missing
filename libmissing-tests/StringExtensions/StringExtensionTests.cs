using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Missing.StringExtensions;

namespace Missing.StringsExtensions
{
	/// <summary>
	/// 
	/// </summary>
	[TestFixture]
	public class StringExtensionTests
	{

		#region Alpha lowercase
		/// <summary>
		/// Tests the contains only alpha lowercase.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaLowercasePositive()
		{
			string str = "thisislowercase";
			Assert.IsTrue(str.ContainsOnlyAlphaLowercase());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaLowercaseNegative_1()
		{
			string str = "thisIsNotLowecASE";
			Assert.IsFalse(str.ContainsOnlyAlphaLowercase());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative_2.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaLowercaseNegative_2()
		{
			string str = "thisislowercasebutwitnumberds12312";
			Assert.IsFalse(str.ContainsOnlyAlphaLowercase());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative_3.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaLowercaseNegative_3()
		{
			string str = "thisislowercasebutwitsymbols#!";
			Assert.IsFalse(str.ContainsOnlyAlphaLowercase());
		}
		#endregion Alpha lowercase

		#region Alpha capital
		/// <summary>
		/// Tests the contains only alpha lowercase.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaCapsPositive()
		{
			string str = "THISISCAPITAL";
			Assert.IsTrue(str.ContainsOnlyAlphaCapital());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaCapitalNegative_1()
		{
			string str = "THISISALMOSTcapital";
			Assert.IsFalse(str.ContainsOnlyAlphaCapital());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative_2.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaCapitalNegative_2()
		{
			string str = "THISISCAPITAL12312";
			Assert.IsFalse(str.ContainsOnlyAlphaCapital());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative_3.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaCapitalNegative_3()
		{
			string str = "THISISCAPITAL!";
			Assert.IsFalse(str.ContainsOnlyAlphaCapital());
		}
		#endregion Alpha capital

		#region Numeric
		/// <summary>
		/// Tests the contains only alpha lowercase.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaNumericPositive()
		{
			string str = "1234545";
			Assert.IsTrue(str.ContainsOnlyNumbers());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaNumericNegative_1()
		{
			string str = "1231243ASLc";
			Assert.IsFalse(str.ContainsOnlyNumbers());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative_2.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaNumericNegative_2()
		{
			string str = "c2f34324!!cd";
			Assert.IsFalse(str.ContainsOnlyNumbers());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative_3.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaNumericNegative_3()
		{
			string str = "fdfa3223!";
			Assert.IsFalse(str.ContainsOnlyNumbers());
		}
		#endregion Numeric

		#region Symbols
		/// <summary>
		/// Tests the contains only alpha lowercase.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaSymbolsPositive()
		{
			string str = "!\"#¤%&/()=?``.,-";
			Assert.IsTrue(str.ContainsOnlySymbols());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaSymbolsNegative_1()
		{
			string str = "!\"#¤%&/()=?``.,-324";
			Assert.IsTrue(str.ContainsOnlySymbols());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative_2.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaSymbolsNegative_2()
		{
			string str = "!\"#¤%&/()=?``.,-aa";
			Assert.IsTrue(str.ContainsOnlySymbols());
		}

		/// <summary>
		/// Tests the contains only alpha lowercase negative_3.
		/// </summary>
		[Test]
		public void TestContainsOnlyAlphaSymbolsNegative_3()
		{
			string str = "!\"#¤%&/()=?``.,-FFaac2";
			Assert.IsTrue(str.ContainsOnlySymbols());
		}
		#endregion Symbols

		#region Alpha lowercase + uppercase
		[Test]
		public void TestContainsOnlyAlphaLowerAndUpperPositive()
		{
			string str = "thisisLowerAndUpper";
			Assert.IsTrue(str.ContainsOnlyAlphaLowercaseAndUppercase());
		}
		[Test]
		public void TestContainsOnlyAlphaLowerAndUpperNegative_1()
		{
			string str = "thisisLowerAndUpper123";
			Assert.IsFalse(str.ContainsOnlyAlphaLowercaseAndUppercase());
		}
		[Test]
		public void TestContainsOnlyAlphaLowerAndUpperNegative_2()
		{
			string str = "thisisLowerAndUpper¤#%";
			Assert.IsFalse(str.ContainsOnlyAlphaLowercaseAndUppercase());
		}
		[Test]
		public void TestContainsOnlyAlphaLowerAndUpperNegative_3()
		{
			string str = "thisisLowerAndUpper123#/";
			Assert.IsFalse(str.ContainsOnlyAlphaLowercaseAndUppercase());
		}
		#endregion 
	}
}
