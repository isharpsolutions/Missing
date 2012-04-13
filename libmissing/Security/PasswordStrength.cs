using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security
{
	public static class PasswordStrength
	{
		/// <summary>
		/// Evaluates the specified password and returns a <see cref="PasswordStrenghtScore"/>
		/// </summary>
		/// <remarks>
		/// The scoring algorithm is devised into multiple steps.
		/// 
		/// First of all, it attemps to flag the password as <see cref="PasswordStrenghtScore.Useless"/> by
		/// checking an internal dictionary of the most commonly used passwords. If the user has attempted to 
		/// "l33t" up the password, it will also attemp to discover that. i.e "password" and "p4ssw0rd" will 
		/// both be caught by the "Useless" checker.
		/// 
		/// If a password cannot be scored as useless, key space computations will be started. Therefore, all
		/// other scores than <see cref="PasswordStrenghtScore.Useless"/> are based solely on the size of the 
		/// keyspace that the password exercises.
		/// 
		/// This is determined by evaluating which "parts" the password is made up of. I.e "myAwesomePassword1"
		/// will have: alpha, alpha uppercase and numers. Thus this password will have an "symbol pool size" of:
		/// <c>28 (alpha lower) + 28 (alpha upper + 10 (numbers) = 66</c>
		/// The pool size can then be utilized for computing the size of the keyspace that an attacker would have
		/// to traverse during a bruteforce attack. This is simple combination theory:
		/// <c>symbolPoolSize^numerOfChars</c>
		/// Which means that in this example we will have:
		/// <c>66^18 = 5,646649614×(10^32)</c>
		/// Which is quite a big number, and thus the given password would be evaluated as being <see cref="PasswordStrenghtScore.Great"/>
		/// </remarks>
		/// <param name="password">The password.</param>
		/// <returns>A <see cref="PasswordStrenghtScore"/> instance</returns>
		public PasswordStrenghtScore Evaluate(string password)
		{
			throw new NotImplementedException();
		}
	}
}
