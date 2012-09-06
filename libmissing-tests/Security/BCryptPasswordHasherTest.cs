using System;
using NUnit.Framework;
using Missing.Security.PasswordHashing.Internal;
using Missing.Security.PasswordHashing;

namespace Missing
{
	[TestFixture()]
	public class BCryptPasswordHasherTest
	{
		[Test()]
		public void TestInstantiationWorks()
		{
			PasswordHasherBase hasher =  PasswordHasherFactory.GetInstance(Missing.Security.PasswordHashing.PasswordHasherAlgorithm.BCrypt);

			Assert.IsNotNull(hasher);
		}
		[Test()]
		public void TestPasswordDerivationWorks()
		{
			string hash = PasswordHasher.Derive("mysupersecretpassword");

			Assert.IsNotNull(hash);
			Assert.IsTrue(hash.StartsWith("$2a"));
		}
		[Test()]
		public void TestThatPasswordDerivationAndVerificationWorksPositive()
		{
			string passphrase = "awesomehorsesridingalong";
			string hash = PasswordHasher.Derive(passphrase);

			bool valid = PasswordHasher.Validate(passphrase, hash);

			Assert.IsTrue(valid);
		}

		[Test()]
		public void TestThatPasswordDerivationAndVerificationWorksNegative()
		{
			string passphrase = "awesomehorsesridingalong";
			string hash = PasswordHasher.Derive(passphrase);

			bool valid = PasswordHasher.Validate("ShouldNotPass", hash);

			Assert.IsFalse(valid);
		}
	}
}

