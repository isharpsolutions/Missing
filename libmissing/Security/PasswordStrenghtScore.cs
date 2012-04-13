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

		/// <summary>
		/// The password is okay and will not be craked easily by attackers
		/// </summary>
		Okay,

		/// <summary>
		/// A very good password which is almost impossible to crack by bruteforce
		/// </summary>
		Great
	}
}
