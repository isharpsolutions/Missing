using System;

namespace Missing.Security
{
	/// <summary>
	/// Mickey mouse encryption.
	/// 
	/// This IS NOT proper encryption. It is a simple XOR between plaintext
	/// and key. It should only be used for non-essential stuff like "hiding"
	/// a plaintext password during transmission (if transmission is done using
	/// an encrypted channel).
	/// </summary>
	public class MickeyMouseEncryption
	{
		/// <summary>
		/// The default encryption key
		/// </summary>
		private static readonly string DefaultKey = "MickeyMouseInDaHouse";
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Security.MickeyMouseEncryption"/> class.
		/// </summary>
		public MickeyMouseEncryption()
		{
		}
		
		#region Encrypt
		/// <summary>
		/// Encrypt the specified plaintext with the given key.
		/// </summary>
		/// <param name="plaintext">
		/// The text to encrypt
		/// </param>
		/// <param name="key">
		/// The key to use
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if the input text is longer than <see cref="Int32.MaxValue"/>
		/// </exception>
		public string Encrypt(string plaintext, string key)
		{
			if (plaintext.Length > Int32.MaxValue)
			{
				throw new ArgumentException("Input text is too long. Max length equals Int32.MaxValue");
			}
			
			char[] encrypted = new char[plaintext.Length];
			
			for (int i=0; i!=plaintext.Length; i++)
			{
				encrypted[i] = (char)(plaintext[i] ^ key[i % key.Length]);
			}
			
			return new string(encrypted);
		}
		
		/// <summary>
		/// Encrypt the specified plaintext using the default key
		/// </summary>
		/// <param name="plaintext">
		/// The text to encrypt
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if the input text is longer than <see cref="Int32.MaxValue"/>
		/// </exception>
		public string Encrypt(string plaintext) {
			return this.Encrypt(plaintext, DefaultKey);
		}
		#endregion Encrypt
		
		#region Decrypt
		/// <summary>
		/// Decrypt the specified ciphertext with the given key.
		/// </summary>
		/// <param name="ciphertext">
		/// The text to decrypt
		/// </param>
		/// <param name="key">
		/// The key to use
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if the input text is longer than <see cref="Int32.MaxValue"/>
		/// </exception>
		public string Decrypt(string ciphertext, string key) {
			return this.Encrypt(ciphertext, key);
		}
		
		/// <summary>
		/// Decrypt the specified ciphertext using the default key
		/// </summary>
		/// <param name="ciphertext">
		/// The text to decrypt
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if the input text is longer than <see cref="Int32.MaxValue"/>
		/// </exception>
		public string Decrypt(string ciphertext)
		{
			return this.Decrypt(ciphertext, DefaultKey);
		}
		#endregion Decrypt
	}
}