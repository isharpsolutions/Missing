using System;
using NUnit.Framework;
using Missing.Security;

namespace Missing
{
	[TestFixture]
	public class MickeyMouseEncryptionTests
	{
		[Test]
		public void EncryptDecrypt_DefaultKey()
		{
			MickeyMouseEncryption enc = new MickeyMouseEncryption();
			
			string plaintext = "Missing is freaking awesome!";
			string ciphertext = enc.Encrypt(plaintext);
			
			Assert.AreEqual(plaintext, enc.Decrypt(ciphertext), "Decrypted string does not match plaintext");
		}
		
		[Test]
		public void EncryptDecrypt_CustomKey()
		{
			MickeyMouseEncryption enc = new MickeyMouseEncryption();
			
			string key = "DanceALittleDanceAndPeeYourNameInTheSnow";
			
			string plaintext = "Missing is freaking awesome!";
			string ciphertext = enc.Encrypt(plaintext, key);
			
			Assert.AreEqual(plaintext, enc.Decrypt(ciphertext, key), "Decrypted string does not match plaintext");
		}
		
		[Test]
		public void CipherTextDiffersFromPlainText()
		{
			MickeyMouseEncryption enc = new MickeyMouseEncryption();
			
			string plaintext = "Missing is freaking awesome!";
			string ciphertext = enc.Encrypt(plaintext);
			
			Assert.AreNotEqual(plaintext, ciphertext, "Ciphertext and plaintext are equals... this is not good");
		}
	}
}