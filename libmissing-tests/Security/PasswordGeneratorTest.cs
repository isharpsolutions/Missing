using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Missing.StringExtensions;

namespace Missing.Security
{
	[TestFixture]
	public class PasswordGeneratorTest
	{

		#region Simple passwords of only one pool source 
		/// <summary>
		/// Tests whether the generator can generate a password with only alpha chars
		/// </summary>
		[Test]
		public void TestAlphaLowerPassword()
		{
			string pwd = PasswordGenerator.GeneratePassword(8, PasswordGeneratorParameters.AlphaLowercase);

			Assert.AreEqual(8, pwd.Length);
			Assert.IsTrue(pwd.ContainsOnlyAlphaLowercase());
		}

		/// <summary>
		/// Tests the generator can create alpha capital password.
		/// </summary>
		[Test]
		public void TestAlphaCapitalPassword()
		{
			string pwd = PasswordGenerator.GeneratePassword(8, PasswordGeneratorParameters.AlphaCapital);

			Assert.AreEqual(8, pwd.Length);
			Assert.IsTrue(pwd.ContainsOnlyAlphaCapital());
		}

		/// <summary>
		/// Tests if the password contains only numbers
		/// </summary>
		[Test]
		public void TestNumericPassword()
		{
			string pwd = PasswordGenerator.GeneratePassword(8, PasswordGeneratorParameters.Numeric);

			Assert.AreEqual(8, pwd.Length);
			Assert.IsTrue(pwd.ContainsOnlyNumbers());
		}

		/// <summary>
		/// Tests if the password contains only symbols
		/// </summary>
		[Test]
		public void TestSymbolPassword()
		{
			string pwd = PasswordGenerator.GeneratePassword(8, PasswordGeneratorParameters.Symbols);

			Assert.AreEqual(8, pwd.Length);
			Assert.IsTrue(pwd.ContainsOnlySymbols());
		}
		#endregion Simple passwords of only one pool source

		#region More complex passwords of two pool sources
		[Test]
		public void TestAlphaLowerAndCaps()
		{
			string pwd = PasswordGenerator.GeneratePassword(8, PasswordGeneratorParameters.Symbols);

			Assert.AreEqual(8, pwd.Length);
			// it must not contain symbols and numers
			Assert.IsTrue( !pwd.ContainsOnlyNumbers() && !pwd.ContainsOnlySymbols() );
		}
		#endregion More complex passwords of two pool sources

		#region Even more compilcated with three pool sources
		#endregion Even more complidated with three pool sources

		#region Complex with all four pool sources
		[Test]
		public void TestAllCharsInPassword()
		{
			string pwd = PasswordGenerator.GeneratePassword(
				8,
				PasswordGeneratorParameters.AlphaLowercase
				|
				PasswordGeneratorParameters.AlphaCapital
				|
				PasswordGeneratorParameters.Numeric
				|
				PasswordGeneratorParameters.Symbols
				);

			Assert.AreEqual(8, pwd.Length);

			// it should contain at least one of each char
			Assert.IsTrue(!pwd.ContainsOnlyAlphaCapital() && !pwd.ContainsOnlyAlphaLowercase() && !pwd.ContainsOnlyNumbers() && !pwd.ContainsOnlySymbols());

		}
		#endregion Complex with all four pool sources

	}
}
