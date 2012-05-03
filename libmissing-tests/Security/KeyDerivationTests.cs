using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Missing.Security;
using Missing.Security.PasswordHashing;

namespace Missing.Security
{
	[TestFixture]
	public class KeyDerivationTests
	{
		#region RandomSalt tests
		[Test]
		public void TestGetSalt()
		{
			byte[]salt = PasswordHasher.RandomSalt();
			// default get salt uses 128 bits, that's 16 bytes
			Assert.IsTrue(salt.Length == 16, "Incorrect salt length");
		}

		[Test]
		public void TestGetSaltLargeByteSize()
		{
			byte[] salt = PasswordHasher.RandomSalt(128);
			Assert.IsTrue(salt.Length == 128, "Incorrect salt length");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestGetSaltWithZeroBytes()
		{
			byte[] salt = PasswordHasher.RandomSalt(0);
			Assert.Fail("Exception of type ArgumentException was not thrown, which is an error");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestGetSaltWithNegativeBits()
		{
			byte[] salt = PasswordHasher.RandomSalt(-5);
			Assert.Fail("Exception of type ArgumentException was not thrown, which is an error");
		}
		#endregion RandomSalt tests

		#region KeyDeriver tests
		[Test]
		public void TestKeyDeriverDefaultOptions()
		{
			PasswordHash key = PasswordHasher.Derive("thisisapassword");

			Assert.IsTrue(key.Iterations >= 8000, "Too few iterations was done by the deriver");
			Assert.IsTrue(key.Iterations <= 9000, "Too many iterations was done by the deriver");
			Assert.IsTrue(key.Key.Length == 32, "Returned key is too small");
			Assert.IsTrue(key.Salt.Length == 16, "Salt is too small");
		}
		[Test]
		public void TestKeyDeriverWithOptions()
		{
			PasswordHasherOptions options = new PasswordHasherOptions()
			{
				HashType = Cryptography.HashType.SHA512,
				Iterations = 16854,
				HashSize = 64,
				Salt = PasswordHasher.RandomSalt(32)
			};
			PasswordHash key = PasswordHasher.Derive("thisisapassword", options);

			Assert.IsTrue(key.Iterations == options.Iterations, "Wrong number of iterations was used");
			Assert.IsTrue(key.Key.Length == Convert.ToInt32(options.HashSize), "Derived key is of incorrect length");
			Assert.AreEqual(options.Salt, key.Salt, "returned salt did not equal the salt that was input");
		}
		#endregion KeyDeriver tests

		#region rfc6070 test vectors
		[Test]
		public void Vector1()
		{
			PasswordHasherOptions options = new PasswordHasherOptions()
			{
				HashType = Cryptography.HashType.SHA1,
				Iterations = 1,
				HashSize = 20,
				Salt = Encoding.ASCII.GetBytes("salt")
			};
			string password = "password";
			byte[] expectedKey = new byte[20] 
			{
				0x0c, 0x60, 0xc8, 0x0f, 0x96, 0x1f, 0x0e, 0x71,
				0xf3, 0xa9, 0xb5, 0x24, 0xaf, 0x60, 0x12, 0x06,
				0x2f, 0xe0, 0x37, 0xa6
			};

			PasswordHash derivedKey = PasswordHasher.Derive(password, options);
			Assert.AreEqual(expectedKey, derivedKey.Key, "The derived key does not match the test vector");
		}

		[Test]
		public void Vector2()
		{
			PasswordHasherOptions options = new PasswordHasherOptions()
			{
				HashType = Cryptography.HashType.SHA1,
				Iterations = 2,
				HashSize = 20,
				Salt = Encoding.ASCII.GetBytes("salt")
			};
			string password = "password";
			byte[] expectedKey = new byte[20] 
			{
				0xea, 0x6c, 0x01, 0x4d, 0xc7, 0x2d, 0x6f, 0x8c,
				0xcd, 0x1e, 0xd9, 0x2a, 0xce, 0x1d, 0x41, 0xf0,
				0xd8, 0xde, 0x89, 0x57
			};

			PasswordHash derivedKey = PasswordHasher.Derive(password, options);
			Assert.AreEqual(expectedKey, derivedKey.Key, "The derived key does not match the test vector");
		}

		[Test]
		public void Vector3()
		{
			PasswordHasherOptions options = new PasswordHasherOptions()
			{
				HashType = Cryptography.HashType.SHA1,
				Iterations = 4096,
				HashSize = 20,
				Salt = Encoding.ASCII.GetBytes("salt")
			};
			string password = "password";
			byte[] expectedKey = new byte[20] 
			{
				0x4b, 0x00, 0x79, 0x01, 0xb7, 0x65, 0x48, 0x9a,
				0xbe, 0xad, 0x49, 0xd9, 0x26, 0xf7, 0x21, 0xd0,
				0x65, 0xa4, 0x29, 0xc1
			};

			PasswordHash derivedKey = PasswordHasher.Derive(password, options);
			Assert.AreEqual(expectedKey, derivedKey.Key, "The derived key does not match the test vector");
		}

		[Test]
		public void Vector4()
		{
			PasswordHasherOptions options = new PasswordHasherOptions()
			{
				HashType = Cryptography.HashType.SHA1,
				Iterations = 16777216,
				HashSize = 20,
				Salt = Encoding.ASCII.GetBytes("salt")
			};
			string password = "password";
			byte[] expectedKey = new byte[20] 
			{
				0x4b, 0x00, 0x79, 0x01, 0xb7, 0x65, 0x48, 0x9a,
				0xbe, 0xad, 0x49, 0xd9, 0x26, 0xf7, 0x21, 0xd0,
				0x65, 0xa4, 0x29, 0xc1
			};

			PasswordHash derivedKey = PasswordHasher.Derive(password, options);
			Assert.AreEqual(expectedKey, derivedKey.Key, "The derived key does not match the test vector");
		}

		[Test]
		public void Vector5()
		{
			PasswordHasherOptions options = new PasswordHasherOptions()
			{
				HashType = Cryptography.HashType.SHA1,
				Iterations = 4096,
				HashSize = 25,
				Salt = Encoding.ASCII.GetBytes("saltSALTsaltSALTsaltSALTsaltSALTsalt")
			};
			string password = "passwordPASSWORDpassword";
			byte[] expectedKey = new byte[25] 
			{
				0x3d, 0x2e, 0xec, 0x4f, 0xe4, 0x1c, 0x84, 0x9b,
				0x80, 0xc8, 0xd8, 0x36, 0x62, 0xc0, 0xe4, 0x4a,
				0x8b, 0x29, 0x1a, 0x96, 0x4c, 0xf2, 0xf0, 0x70,
				0x38
			};

			PasswordHash derivedKey = PasswordHasher.Derive(password, options);
			Assert.AreEqual(expectedKey, derivedKey.Key, "The derived key does not match the test vector");
		}

		[Test]
		public void Vector6()
		{
			PasswordHasherOptions options = new PasswordHasherOptions()
			{
				HashType = Cryptography.HashType.SHA1,
				Iterations = 4096,
				HashSize = 16,
				Salt = Encoding.ASCII.GetBytes("sa\0lt")
			};
			string password = "pass\0word";
			byte[] expectedKey = new byte[16] 
			{
				0x56, 0xfa, 0x6a, 0xa7, 0x55, 0x48, 0x09, 0x9d,
				0xcc, 0x37, 0xd7, 0xf0, 0x34, 0x25, 0xe0, 0xc3
			};

			PasswordHash derivedKey = PasswordHasher.Derive(password, options);
			Assert.AreEqual(expectedKey, derivedKey.Key, "The derived key does not match the test vector");
		}
		#endregion rfc6070 test vectors
	}
}
