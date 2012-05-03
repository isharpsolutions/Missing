using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Missing.Security.KeyDerivation;
using Missing.Security.KeyDerivation.Internal;

namespace Missing.Security.KeyDerivation
{
	/// <summary>
	/// Implements the rfc2898 key derivation function PBKDF2
	/// </summary>
	public static class KeyDeriver
	{
		#region RandomSalt
		/// <summary>
		/// Gets a new randomly generated salt of length 128 bits
		/// </summary>
		/// <returns></returns>
		public static byte[] RandomSalt()
		{
			return RandomSalt(128);
		}


		/// <summary>
		/// Gets a new randomly generated salt of the given bit length
		/// <paramref name="length"/>
		/// </summary>
		/// <param name="length">The length, in bits, of the returned salt. Must be a multiple of 8</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">If <paramref name="length"/> is not a multiple of 8</exception>
		public static byte[] RandomSalt(int length)
		{
			if ( (length % 8 != 0) || length < 8)
			{
				throw new ArgumentException("Length must be a multiple of 8");
			}

			RNGCryptoServiceProvider crng = new RNGCryptoServiceProvider();
			byte[] salt = new byte[length / 8];
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
		/// of size 128 bits, and an interation count in the interval [8000;9000].
		/// 
		/// The SHA2 hash is used for the HMAC algorithm
		/// </remarks>
		/// <param name="password">The master password to derive a key from</param>
		/// <returns></returns>
		public static DerivedKey Derive(string password)
		{
			// default settings are good..
			KeyDeriverOptions options = new KeyDeriverOptions();
			return Derive(password, options);
		}

		/// <summary>
		/// Derives the specified password by applying the given <paramref name="options"/> to
		/// the underlying implementations.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static DerivedKey Derive(string password, KeyDeriverOptions options)
		{
			KeyDeriverBase kdb = KeyDeriverFactory.GetInstance(options.Algorithm);
			return kdb.Derive(password, options);
		}
		#endregion Key derivation
	}
}
