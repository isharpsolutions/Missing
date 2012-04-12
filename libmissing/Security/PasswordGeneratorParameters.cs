using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security
{
	/// <summary>
	/// Used for selecting the charachters the <see cref="PasswordGenerator"/> should include when generating passwords
	/// 
	/// They are bit-flags so they can be combined to enlarge the keyspace
	/// </summary>
	[Flags]
	public enum PasswordGeneratorParameters
	{
		AlphaLowercase = 1,

		AlphaCapital = 2,

		Numeric = 4,

		Symbols = 8
	}
}
