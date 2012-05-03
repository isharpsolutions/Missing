using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Missing.Security.Cryptography;

namespace Missing.Security.KeyDerivation
{
	public class KeyDerivationOptions
	{
		public KeyDerivationOptions()
		{
			Algorithm = KeyDerivationAlgorithm.PBKDF2;
			SaltSize = 128;
			HashType = Cryptography.HashType.SHA256;
			KeySize = 256;
			Random rng = new Random();
			Iterations = rng.Next(8000, 9000);
		}

		/// <summary>
		/// Determines which underlying algorithm that should be used during key derivation
		/// </summary>
		public KeyDerivationAlgorithm Algorithm { get; set; }

		/// <summary>
		/// Size of the salt in bits, must be a multiple of 8
		/// </summary>
		public int SaltSize { get; set; }

		/// <summary>
		/// Determines which type of HMAC that should be used.
		/// </summary>
		public HashType HashType { get; set; }

		/// <summary>
		/// Size of the returned key, in bits
		/// </summary>
		public int KeySize { get; set; }

		/// <summary>
		/// Determines the number of iterations the underlying algorithm should use
		/// Defaults to a random value between 8000 and 9000
		/// </summary>
		public int Iterations { get; set; }
	}
}
