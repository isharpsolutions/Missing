using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.PasswordHashing.Internal
{

	/// <summary>
	/// Shared base class for all implemented key derivers
	/// </summary>
	internal abstract class PasswordHasherBase
	{
		/// <summary>
		/// Derives a key from the specified password.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public abstract PasswordHash Compute(string password, PasswordHasherOptions options);

		/// <summary>
		/// Verify the specified password against the encoded hash
		/// </summary>
		/// <param name='password'>
		/// If set to <c>true</c> password.
		/// </param>
		/// <param name='encodedHash'>
		/// If set to <c>true</c> encoded hash.
		/// </param>
		public abstract bool Verify(string password, string encodedHash);
	}
}
