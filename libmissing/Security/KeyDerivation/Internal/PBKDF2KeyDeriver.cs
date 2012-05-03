using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.KeyDerivation.Internal
{
	internal class PBKDF2KeyDeriver : KeyDeriverBase
	{
		/// <summary>
		/// Derives a key from the specified password.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public override DerivedKey Derive(string password, KeyDeriverOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
