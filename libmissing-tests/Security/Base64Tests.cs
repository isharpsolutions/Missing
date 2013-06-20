using System;
using NUnit.Framework;
using Missing.Security;

namespace Missing
{
	[TestFixture]
	public class Base64Tests
	{
		[Test]
		public void Encode_Default()
		{
			string plaintext = "Missing is awesome";
			
			// encoded with http://www.base64encode.org/
			string expected = "TWlzc2luZyBpcyBhd2Vzb21l";
			
			Assert.AreEqual(expected, Base64.Encode(plaintext), "Encoded string does not match expected");
		}

		[Test]
		public void Encode_Utf8()
		{
			string plaintext = "Missing is awesome";

			// encoded with http://www.base64encode.org/
			string expected = "TWlzc2luZyBpcyBhd2Vzb21l";

			Assert.AreEqual(expected, Base64.Encode(plaintext, System.Text.Encoding.UTF8), "Encoded string does not match expected");
		}

		[Test]
		public void Encode_Ascii()
		{
			string plaintext = "Missing is awesome";
			
			// encoded with http://www.base64encode.org/
			string expected = "TWlzc2luZyBpcyBhd2Vzb21l";
			
			Assert.AreEqual(expected, Base64.Encode(plaintext, System.Text.Encoding.ASCII), "Encoded string does not match expected");
		}

		[Test]
		public void Decode_Default()
		{
			// encoded with http://www.base64encode.org/
			string encodedtext = "QXdlc29tZSBpcyBNaXNzaW5nLi4uIHllZWVlcw==";

			string expected = "Awesome is Missing... yeeees";
			
			Assert.AreEqual(expected, Base64.Decode(encodedtext), "Decoded string does not match expected");
		}

		[Test]
		public void Decode_Utf8()
		{
			// encoded with http://www.base64encode.org/
			string encodedtext = "QXdlc29tZSBpcyBNaXNzaW5nLi4uIHllZWVlcw==";
			
			string expected = "Awesome is Missing... yeeees";
			
			Assert.AreEqual(expected, Base64.Decode(encodedtext, System.Text.Encoding.UTF8), "Decoded string does not match expected");
		}

		[Test]
		public void Decode_Ascii()
		{
			// encoded with http://www.base64encode.org/
			string encodedtext = "QXdlc29tZSBpcyBNaXNzaW5nLi4uIHllZWVlcw==";
			
			string expected = "Awesome is Missing... yeeees";
			
			Assert.AreEqual(expected, Base64.Decode(encodedtext, System.Text.Encoding.ASCII), "Decoded string does not match expected");
		}

		[Test]
		public void EncodeDecode()
		{
			string plaintext = "Missing is meant to be an awesome tool. What do you think about it?";

			string encodedtext = Base64.Encode(plaintext);
			string decoded = Base64.Decode(encodedtext);

			Assert.AreEqual(plaintext, decoded, "The decoded string does not math the original plaintext");
		}
	}
}