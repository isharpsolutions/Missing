using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.PasswordHashing.Internal
{
	internal class PasswordHasherFactory
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
				case PasswordHasherAlgorithm.BCrypt:
				{
					return new BCryptPasswordHasher();
				}
				default:
				{
					throw new ArgumentException(String.Format("Key derivation algorithm '{0}' is not supported", algo));
				}
			}
		}

		/// <summary>
		/// Gets the instance based on the signature of an encoded password
		/// </summary>
		/// <returns>
		/// The instance.
		/// </returns>
		/// <param name='encodedPassword'>
		/// Encoded password.
		/// </param>
		public static PasswordHasherBase GetInstance(string encodedPassword)
		{
			// bcrypt hashes starts with $2a, so if we find this, return a bcrypt thing, otherwise it must be PBKDF2
			if(encodedPassword.StartsWith("$2a"))
			{
				return GetInstance(PasswordHasherAlgorithm.BCrypt);
			}
			else
			{
				return GetInstance(PasswordHasherAlgorithm.PBKDF2);
			}
		}
	}
}
