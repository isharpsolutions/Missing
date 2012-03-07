using System;
using System.Security.Cryptography;

namespace Missing.Security.Cryptography
{
	public static class HashFactory
	{
		public static HashAlgorithm CreateInstance(HashType theHash)
		{
			HashAlgorithm hash = null;
			switch(theHash)
			{
				case HashType.MD5:
				{
					hash = new MD5CryptoServiceProvider();
					break;
				}
				case HashType.RIPEMD160:
				{
					hash = new RIPEMD160Managed();
					break;
				}
				case HashType.SHA1:
				{
					hash = new SHA1CryptoServiceProvider();
					break;
				}
				case HashType.SHA256:
				{
					hash = new SHA256CryptoServiceProvider();
					break;
				}
				case HashType.SHA384:
				{
					hash = new SHA384CryptoServiceProvider();
					break;
				}
				case HashType.SHA512:
				{
					hash = new SHA512CryptoServiceProvider();
					break;
				}
				default:
				{
					throw new NotSupportedException(String.Format("The hash algorithm '{0}' is not supported", theHash.ToString()));
				}
			}
			
			return hash;
		}
	}
}