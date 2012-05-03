using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Missing.Security.PasswordHashing;
using Missing.Security.PasswordHashing.Internal;

namespace Missing.Security.PasswordHashing
{
	/// <summary>
	/// Implements the rfc2898 key derivation function PBKDF2
	/// </summary>
	public static class PasswordHasher
	{
		#region RandomSalt
		/// <summary>
		/// Gets a new randomly generated salt of length 16 bytes
		/// </summary>
		/// <returns></returns>
		public static byte[] RandomSalt()
		{
			return RandomSalt(16);
		}


		/// <summary>
		/// Gets a new randomly generated salt of the given byte length
		/// <paramref name="length"/>
		/// </summary>
		/// <param name="length">The length, in bytes, of the returned salt.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">If <paramref name="length"/> is smaller than 1</exception>
		public static byte[] RandomSalt(int length)
		{
			if (length < 1)
			{
				throw new ArgumentException("Length must be at least 1");
			}

			RNGCryptoServiceProvider crng = new RNGCryptoServiceProvider();
			byte[] salt = new byte[length];
			crng.GetBytes(salt);
			crng = null;

			return salt;
		}
		#endregion RandomSalt

		#region Key derivation
		/// <summary>
		/// Derives a key from the given password, using sane defaults.
		/// </summary>
		/// <remarks>
		/// This method is using the PBKDF2 algorithm underneath, with random salts
		/// of size 16 bytes, and an interation count in the interval [8000;9000].
		/// 
		/// The SHA2 hash is used for the HMAC algorithm
		/// </remarks>
		/// <param name="password">The master password to derive a key from</param>
		/// <returns></returns>
		public static PasswordHash Derive(string password)
		{
			// default settings are good..
			PasswordHasherOptions options = new PasswordHasherOptions();
			return Derive(password, options);
		}

		/// <summary>
		/// Derives the specified password by applying the given <paramref name="options"/> to
		/// the underlying implementations.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static PasswordHash Derive(string password, PasswordHasherOptions options)
		{
			PasswordHasherBase kdb = PasswordHasherFactory.GetInstance(options.Algorithm);
			return kdb.Derive(password, options);
		}
		#endregion Key derivation
	}
}
