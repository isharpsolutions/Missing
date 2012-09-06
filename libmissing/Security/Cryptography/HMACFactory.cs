using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Missing.Security.Cryptography
{
	/// <summary>
	/// Factory for instantiating different HMACS
	/// </summary>
	public static class HMACFactory
	{

		/// <summary>
		/// Creates a new HMAC instance
		/// </summary>
		/// <param name="theHash">The hash.</param>
		/// <returns>A <see cref="HMAC"/> instance</returns>
		/// <exception cref="NotSupportedException">If the given <paramref name="theHash"/> is not a supported HMAC</exception>
		public static System.Security.Cryptography.HMAC CreateInstance(HashType theHash)
		{
			System.Security.Cryptography.HMAC hmac = null;
			switch (theHash)
			{
				case HashType.MD5:
					{
						hmac = new HMACMD5();
						break;
					}
				case HashType.RIPEMD160:
					{
						hmac = new HMACRIPEMD160();
						break;
					}
				case HashType.SHA1:
					{
						hmac = new HMACSHA1();
						break;
					}
				case HashType.SHA256:
					{
						hmac = new HMACSHA256();
						break;
					}
				case HashType.SHA384:
					{
						hmac = new HMACSHA384();
						break;
					}
				case HashType.SHA512:
					{
						hmac = new HMACSHA512();
						break;
					}
				default:
					{
						throw new NotSupportedException(String.Format("The given hash :'{0}' is not a supported hmac", theHash));
					}
			}

			return hmac;
		}
	}
}
