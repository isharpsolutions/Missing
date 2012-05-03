using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.PasswordHashing.Internal
{
	internal class PBKDF2PasswordHasher : PasswordHasherBase
	{
		/// <summary>
		/// Derives a key from the specified password.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public override PasswordHash Derive(string password, PasswordHasherOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
