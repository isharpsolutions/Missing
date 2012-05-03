using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security.KeyDerivation
{
	/// <summary>
	/// Contains all neccesary information about a key that has been derived using
	/// the <see cref="KeyDerivation"/> functions
	/// </summary>
	public class DerivedKey
	{
		public byte[] Salt { get; set; }

		public byte[] Key { get; set; }

		public string KeyHex { get; set; }

		public int Iterations { get; set; }

		public KeyDeriverAlgorithm Algorithm { get; set; }
	}
}
