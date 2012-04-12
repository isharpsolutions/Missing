using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security
{
	public enum PasswordStrenghtScore
	{
		/// <summary>
		/// The strenght of the password is non existant
		/// </summary>
		Useless,

		/// <summary>
		/// The password is weak and will be easily cracked / guessed by an attacker
		/// </summary>
		Weak,

		Okay,

		Great
	}
}
