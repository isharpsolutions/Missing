using System;
using System.Text.RegularExpressions;

namespace Missing.Validation.Enforcers
{
	/// <summary>
	/// Enforces that a given string contains a valid email address
	/// </summary>
	public class EmailEnforcer : Enforcer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.EmailEnforcer"/> class.
		/// </summary>
		public EmailEnforcer() : base()
		{
		}
		
		/// <summary>
		/// Tests the user part of an email address
		/// </summary>
		private static Regex userExp = new Regex(@"^[a-z0-9_\.-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
		
		/// <summary>
		/// Tests the domain part of an email address
		/// </summary>
		/// <remarks>
		/// First parantheses is for all subdomains.
		/// Allowed characters are letters, digits and "-".
		/// Subdomains must end with a "."
		/// 
		/// Second parantheses is for the top-level-domain
		/// Only letters allowed and must be 2 to 5 chars long
		/// </remarks>
		private static Regex domainExp = new Regex(@"^([a-z0-9-]+\.)+([a-z]{2,6})$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
		
		#region implemented abstract members of Missing.Validation.Enforcer
		/// <summary>
		/// Check the specified input.
		/// </summary>
		/// <param name='input'>
		/// Input.
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if the given input is not a string
		/// </exception>
		public override string Check(object input)
		{
			if ( !(input is string) )
			{
				throw new ArgumentException("Wrong input type. I only know how to handle strings");
			}
			
			string bla = (string)input;
			
			// split the address in user and domain
			string[] parts = bla.Split('@');
			
			// if there is more than 2 parts, the address is invalid
			if (parts.Length != 2) {
				return String.Format("There should be a user part (before @) and a domain part (after @), but found {0} parts", parts.Length);
			}
			
			string user = parts[0];
			string domain = parts[1];
			
			// validate the user... it is easier to test for the invalid
			// first character in this line rather than the reg.exp
			bool userIsOk = user[0] != '.' && userExp.IsMatch(user);
			
			if (!userIsOk)
			{
				return "The user part of the email address is invalid";
			}
			
			// run domain test
			bool domainIsOk = domainExp.IsMatch(domain);
			
			if (!domainIsOk)
			{
				return "The domain part of the email address is invalid";
			}
			
			return String.Empty;
		}
		#endregion
	}
}

