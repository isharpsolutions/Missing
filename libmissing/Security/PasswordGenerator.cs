using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security
{
	public static class PasswordGenerator
	{
		#region Arrays with chars
		/// <summary>
		/// Contains a list lowercase alpha chars
		/// </summary>
		private static char[] alpha =
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
			'u', 'v', 'w', 'x', 'y', 'z'
		};

		/// <summary>
		/// Contains a list of capital alpha chars
		/// </summary>
		private static char[] alphaCaps = (new string(alpha).ToUpper()).ToArray<char>();

		/// <summary>
		/// Contains a list of capital and non capital chars
		/// </summary>
		private static char[] numeric =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
		};

		/// <summary>
		/// Contains a list of the most common symbols
		/// </summary>
		private static char[] symbols =
		{
			'!', '"', '#', '$','%', '&','\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<','=', '>',
			'?', '@', '[' ,'\\' ,']', '^', '_', '`', '{', '|', '}', '~'
		};
		#endregion Arrays with chars

		#region GeneratePassword
		/// <summary>
		/// Genreate a password of length 7 with [lowercase + capital] letters and numbers
		/// </summary>
		/// <returns>A password string</returns>
		public static string GeneratePassword()
		{
			return GeneratePassword(7);
		}


		/// <summary>
		/// Generates a password of length <paramref name="passwordLength"/> with [lowercase + capital] letters and numbers
		/// </summary>
		/// <param name="passwordLength">Length of the password.</param>
		/// <returns>A password string</returns>
		public static string GeneratePassword(int passwordLength)
		{
			return GeneratePassword(
				passwordLength,
				PasswordGeneratorParameters.AlphaLowercase
				|
				PasswordGeneratorParameters.AlphaCapital
				|
				PasswordGeneratorParameters.Numeric);
		}

		/// <summary>
		/// Generates a password of length <paramref name="passwordLength"/> from the given pool of 
		/// symbols in <paramref name="parameters"/>
		/// </summary>
		/// <param name="passwordLength">Length of the password.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		public static string GeneratePassword(int passwordLength, PasswordGeneratorParameters parameters)
		{
			#region generate pool of symbols
			List<char> pool = GenerateSymbolPool(parameters);
			#endregion generate pool of symbols

			#region generate password
			char[] password = new char[passwordLength];
			Random rand = new Random();
			int poolSize = pool.Count - 1;
			for (int i = 0; i < passwordLength; i++)
			{
				password[i] = pool[rand.Next(0, poolSize)];
			}
			#endregion generate password

			return new string(password);
		}

		/// <summary>
		/// Generates a pool of symbols
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		private static List<char> GenerateSymbolPool(PasswordGeneratorParameters parameters)
		{
			List<char> pool = new List<char>();

			// copy the alpha chars into the source pool 
			if (parameters.HasFlag(PasswordGeneratorParameters.AlphaLowercase))
			{
				pool.AddRange(alpha);
			}

			// add the alpha capital to the source pool
			if (parameters.HasFlag(PasswordGeneratorParameters.AlphaCapital))
			{
				pool.AddRange(alphaCaps);
			}

			// add the numeric values to the source pool
			if (parameters.HasFlag(PasswordGeneratorParameters.Numeric))
			{
				pool.AddRange(numeric);
			}

			// add the symbols to the source pool
			if (parameters.HasFlag(PasswordGeneratorParameters.Symbols))
			{
				pool.AddRange(symbols);
			}
			return pool;
		}
		#endregion GeneratePassword
	}
}
