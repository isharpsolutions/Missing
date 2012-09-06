using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.PasswordHashing
{
	/// <summary>
	/// Contains all neccesary information about a key that has been derived using
	/// the <see cref="KeyDerivation"/> functions
	/// </summary>
	public class PasswordHash
	{
		/// <summary>
		/// The encoded has value, containing iterations, salt and the password hash
		/// </summary>
		public string Hash {get; set;}

		/// <summary>
		/// Gets or sets the algorithm that was used for generating the hash.
		/// </summary>
		/// <value>
		/// The algorithm.
		/// </value>
		public PasswordHasherAlgorithm Algorithm { get; set; }
	}
}
