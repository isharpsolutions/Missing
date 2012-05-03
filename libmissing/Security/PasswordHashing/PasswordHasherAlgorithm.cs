using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.PasswordHashing
{
	/// <summary>
	/// Enumerates the possible key derivation algorithm that 
	/// the functions of <see cref="PasswordHasher"/> supports
	/// </summary>
	public enum PasswordHasherAlgorithm
	{
		/// <summary>
		/// Use the RFC2898 PBKDF2 algorithm for deriving keys
		/// </summary>
		PBKDF2
	}
}
