using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Missing.Security
{
	/// <summary>
	/// Unit tests for the <see cref="PasswordStrenght"/> evaluator
	/// </summary>
	public class PasswordStrengthTests
	{
		#region Test "useless" passwords
		[Test]
		public void TestThatUselessPasswordsAreCaught()
		{
			Assert.AreEqual(PasswordStrenghtScore.Useless, PasswordStrength.Evaluate("password"));
			Assert.AreEqual(PasswordStrenghtScore.Useless, PasswordStrength.Evaluate("p4ssword"));
			Assert.AreEqual(PasswordStrenghtScore.Useless, PasswordStrength.Evaluate("p4ssw0rd"));
			Assert.AreEqual(PasswordStrenghtScore.Useless, PasswordStrength.Evaluate("dogg1e"));
			Assert.AreEqual(PasswordStrenghtScore.Useless, PasswordStrength.Evaluate("D0gg1e"));
		}
		#endregion Test "useless" passwords

		#region test weak passwords
		#endregion test weak passwords

		#region test medium passwords
		#endregion test medium passwords

		#region test great/strong passwords
		#endregion test great/strong passwords
	}
}
