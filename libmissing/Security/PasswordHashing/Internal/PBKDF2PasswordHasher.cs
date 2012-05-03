using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Missing.Security.Cryptography;

namespace Missing.Security.PasswordHashing.Internal
{
	/// <summary>
	/// Implementation of the rfc2898 key derivation function, PBKDF2
	/// </summary>
	internal class PBKDF2PasswordHasher : PasswordHasherBase
	{
		/// <summary>
		/// Derives a key from the specified password.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentExcetption">If the given hash lengh exceeds the lenght defined in the rfc standard</exception>
		public override PasswordHash Compute(string password, PasswordHasherOptions options)
		{
			// create a new HMAC
			HMAC hmac = HMACFactory.CreateInstance(options.HashType);
			ulong hLen = Convert.ToUInt64(hmac.HashSize / 8);
			ulong dkLen = Convert.ToUInt64(options.HashSize);

			// sanity check, section 5.2 in rfc standard.
			ulong maxLength = UInt32.MaxValue * hLen;
			if (options.HashSize > maxLength)
			{
				throw new ArgumentException("Provided hash length is too big for the current options, it violates the rfc standard");
			}

			// algorithm start
			uint byteBlocks = Convert.ToUInt32(Math.Ceiling( (decimal)options.HashSize / (decimal)hLen));
			uint bytesInLastBlock = Convert.ToUInt32(dkLen - (byteBlocks - 1) * hLen);
			IList<byte[]> blocks = new List<byte[]>();
			
			// the algorithm is 1 based in the RFC, so add a dummy byte at index 0
			blocks.Add(new byte[0]);
			for (uint i = 1; i <= byteBlocks; i++)
			{
				byte[] current = F(password, options.Salt, options.Iterations, i, options.HashType, hLen);
				blocks.Add(current);
			}

			// copy all blocks except the last one into the resulting hash
			ulong resultingHashSize = byteBlocks * hLen;
			if (byteBlocks > 1)
			{
				resultingHashSize += bytesInLastBlock;
			}
			
			byte[] resultingHash = new byte[resultingHashSize];
			int startIndex = 0;
			for (uint i = 1; i < blocks.Count-1; i++)
			{
				byte[] current = blocks[(int)i];
				startIndex = Convert.ToInt32((i - 1) * hLen);
				current.CopyTo(resultingHash, startIndex);
			}
			
			// copy "bytesInLastBlock" bytes into the resulting hash
			byte[] last = blocks[blocks.Count-1];
			for (int i = 0; i < bytesInLastBlock; i++)
			{
				resultingHash[startIndex] = last[i];
				startIndex++;
			}

			PasswordHash hash = new PasswordHash()
			{
				Algorithm = options.Algorithm,
				Iterations = options.Iterations,
				Key = resultingHash,
				KeyHex = BitConverter.ToString(resultingHash).Replace("-", String.Empty).ToLower(),
				Salt = options.Salt
			};

			hmac.Clear();
			return hash;
		}

		/// <summary>
		/// The inner function F as defined in the RFC
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="salt">The salt.</param>
		/// <param name="iterationCount">The iteration count.</param>
		/// <param name="currentIteration">The current iteration.</param>
		/// <param name="hmac">The hmac.</param>
		/// <param name="hLen">The block size of the underlying hash algorithm, in bytes.</param>
		/// <returns></returns>
		private byte[] F(string password, byte[] salt, uint iterationCount, uint currentIteration, HashType hmacType, ulong hLen)
		{
			HMAC hmac = HMACFactory.CreateInstance(hmacType);

			hmac.Key = System.Text.Encoding.UTF8.GetBytes(password);
			IList<byte[]> components = new List<byte[]>();
			
			// get INT(currentIteration)
			byte[] bCurrentIteration = new byte[4];
			bCurrentIteration[0] = (byte)(currentIteration << 8);
			bCurrentIteration[1] = (byte)(currentIteration << 16);
			bCurrentIteration[2] = (byte)(currentIteration << 24);
			bCurrentIteration[3] = (byte)(currentIteration << 32);
			// get U_1
			byte[] tmp = new byte[hLen + 4];
			salt.CopyTo(tmp, 0);
			bCurrentIteration.CopyTo(tmp, salt.Length);
			hmac.ComputeHash(tmp);
			components.Add(hmac.Hash);

			
			// get the remaining U's
			for (uint i = 0; i < iterationCount; i++)
			{				
				hmac.ComputeHash(components[(int)i]);
				components.Add(hmac.Hash);
			}

			// XOR all Us together
			byte[] result = new byte[hLen];
			for (int i = 0; i < components.Count; i++)
			{
				byte[] current = components[i];
				for (int j = 0; j < result.Length; j++)
				{
					result[j] ^= current[j];
				}
			}

			hmac.Clear();
			return result;
		}
	}
}
