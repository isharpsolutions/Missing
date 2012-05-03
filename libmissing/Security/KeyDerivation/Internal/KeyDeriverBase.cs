using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.KeyDerivation.Internal
{

	/// <summary>
	/// Shared base class for all implemented key derivers
	/// </summary>
	internal abstract class KeyDeriverBase
	{
		/// <summary>
		/// Derives a key from the specified password.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public abstract DerivedKey Derive(string password, KeyDerivationOptions options);
	}
}
