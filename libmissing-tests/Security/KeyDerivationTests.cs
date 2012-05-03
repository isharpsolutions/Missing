﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Missing.Security;
using Missing.Security.KeyDerivation;

namespace Missing.Security
{
	[TestFixture]
	public class KeyDerivationTests
	{
		[Test]
		public void TestGetSalt()
		{
			byte[]salt = KeyDeriver.RandomSalt();
			// default get salt uses 128 bits, that's 16 bytes
			Assert.IsTrue(salt.Length == 128/8, "Incorrect salt length");
		}

		[Test]
		public void TestGetSaltLargeBitSize()
		{
			byte[] salt = KeyDeriver.RandomSalt(1024);
			Assert.IsTrue(salt.Length == 1024 / 8, "Incorrect salt length");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestGetSaltIncorrectBitSize()
		{
			byte[] salt = KeyDeriver.RandomSalt(801);
			Assert.Fail("Exception of type ArgumentException was not thrown, which is an error");
		}
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestGetSaltWithZeroBits()
		{
			byte[] salt = KeyDeriver.RandomSalt(0);
			Assert.Fail("Exception of type ArgumentException was not thrown, which is an error");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestGetSaltWithNegativeBits()
		{
			byte[] salt = KeyDeriver.RandomSalt(-5);
			Assert.Fail("Exception of type ArgumentException was not thrown, which is an error");
		}
	}
}
