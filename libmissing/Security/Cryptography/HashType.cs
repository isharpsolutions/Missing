using System;

namespace Missing.Security.Cryptography
{
	/// <summary>
	/// Enumerates the possible hash algorithms for the <see cref="Missing.Security.Cryptography"/>
	/// </summary>
	public enum HashType
	{
		/// <summary>
		/// The MD5 algorithm.
		/// </summary>
		/// <remarks>
		/// Not adviced to use anymore due to secirty issues. Consider using <see cref="HashType.SHA256"/> instead
		/// </remarks>
		MD5,
		
		/// <summary>
		/// The RIPEMD 160 algorithm
		/// </summary>
		RIPEMD160,
		
		/// <summary>
		/// The SHA1 algorithm
		/// </summary>
		/// <remarks>
		/// Not adviced to use anymore due to security issues. Consider using <see cref="HashType.SHA256"/> instead
		/// </remarks>
		SHA1,
		
		/// <summary>
		/// The SHA 256 algorithm
		/// </summary>
		SHA256,
		
		/// <summary>
		/// The SHA 384 algorithm
		/// </summary>
		SHA384,
		
		/// <summary>
		/// The stronges version of the SHA2 algorithms
		/// </summary>
		SHA512
	}
}