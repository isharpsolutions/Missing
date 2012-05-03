using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Missing.Security.Cryptography;

namespace Missing.Security.KeyDerivation
{
	public class KeyDeriverOptions
	{
		public KeyDeriverOptions()
		{
			Algorithm = KeyDeriverAlgorithm.PBKDF2;
			Salt = KeyDeriver.RandomSalt();
			HashType = Cryptography.HashType.SHA256;
			KeySize = 32;
			Random rng = new Random();
			Iterations = rng.Next(8000, 9000);
		}

		/// <summary>
		/// Determines which underlying algorithm that should be used during key derivation
		/// </summary>
		public KeyDeriverAlgorithm Algorithm { get; set; }

		/// <summary>
		/// The salt to use during the derivation
		/// </summary>
		public byte[] Salt { get; set; }

		/// <summary>
		/// Determines which type of HMAC that should be used.
		/// </summary>
		public HashType HashType { get; set; }

		/// <summary>
		/// Size of the returned key, in bytes / octets
		/// </summary>
		public int KeySize { get; set; }

		/// <summary>
		/// Determines the number of iterations the underlying algorithm should use
		/// Defaults to a random value between 8000 and 9000
		/// </summary>
		public int Iterations { get; set; }
	}
}
