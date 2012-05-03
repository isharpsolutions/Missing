using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.KeyDerivation.Internal
{
	internal class PasswordHahserFactory
	{
		/// <summary>
		/// Gets the an instance specified by <paramref name="algo"/>
		/// </summary>
		/// <param name="algo">The algorithm to get an implementation for.</param>
		/// <returns></returns>
		public static PasswordHasherBase GetInstance(PasswordHasherAlgorithm algo)
		{
			switch (algo)
			{
				case PasswordHasherAlgorithm.PBKDF2:
					{
						return new PBKDF2PasswordHasher();
					}
				default:
					{
						throw new ArgumentException(String.Format("Key derivation algorithm '{0}' is not supported", algo));
					}
			}
		}
	}
}
