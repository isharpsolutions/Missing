using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security
{
	/// <summary>
	/// Implements the rfc2898 key derivation function PBKDF2
	/// </summary>
	public static class KeyDerivation
	{

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
			throw new NotImplementedException();
		}
	}
}
