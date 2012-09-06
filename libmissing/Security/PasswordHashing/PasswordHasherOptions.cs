using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Missing.Security.Cryptography;

namespace Missing.Security.PasswordHashing
{
	public class PasswordHasherOptions
	{
		public PasswordHasherOptions()
		{
			Algorithm = PasswordHasherAlgorithm.PBKDF2;
			Salt = PasswordHasher.RandomSalt();
			HashType = Cryptography.HashType.SHA256;
			HashSize = 32;
			WorkAmount = 10;
		}

		/// <summary>
		/// Determines which underlying algorithm that should be used during key derivation
		/// </summary>
		public PasswordHasherAlgorithm Algorithm { get; set; }

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
		public ulong HashSize { get; set; }

		/// <summary>
		/// Determines the number of iterations, in log2 format, the underlying algorithm should use
		/// Defaults to 10, so 2^10 iterations = 1024
		/// </summary>
		public uint WorkAmount { get; set; }
	}
}
