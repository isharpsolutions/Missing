using System;
using System.IO;
using System.Security.Cryptography;

namespace Missing.Security.Cryptography
{
	/// <summary>
	/// Defines methods to easy the work with hash classes from <see cref="System.Security.Cryptography"/>
	/// </summary>
	public static class Hash
	{
		/// <summary>
		/// Compute the hash of a given file
		/// </summary>
		/// <param name="hashType">
		/// Defines the type of hash algorithm to use
		/// </param>
		/// <param name="filename">
		/// A <see cref="System.String"/> with the filename
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/> with the hash. The string is in lowercase HEX and
		/// all "-" are removed.
		/// </returns>
		public static string FromFile(HashType hashType, string filename)
		{
			HashAlgorithm provider = HashFactory.CreateInstance(hashType);
			
			
			FileStream file = new FileStream(filename, FileMode.Open);
			byte[] retVal = provider.ComputeHash(file);
			file.Close();
			file = null;
			provider = null;
			return BitConverter.ToString(retVal).ToLower().Replace("-", String.Empty);
		}
		
		/// <summary>
		/// Compute a hash of a string
		/// </summary>
		/// <param name="hashType">
		/// Defines the type of hash algorithm to use
		/// </param>
		/// <param name="input">
		/// The <see cref="System.String"/> to hash. It will be converted to bytes using UTF8 encoding
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/> with the MD5 hash. The string is in lowercase
		/// and all "-" are removed.
		/// </returns>
		public static string FromString(HashType hashType, string input)
		{
			byte[] raw = System.Text.Encoding.UTF8.GetBytes( input );
			
			HashAlgorithm provider = HashFactory.CreateInstance(hashType);
			provider.ComputeHash(raw);
			
			string hex = BitConverter.ToString( provider.Hash );
			provider = null;
			raw = null;
			
			return hex.Replace("-", String.Empty).ToLower();
		}
	}
}