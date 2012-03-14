using System;
using System.Text.RegularExpressions;

namespace Missing.Validation
{
	/// <summary>
	/// Checks an input string against a supplied
	/// reguler expression
	/// </summary>
	public class RegExpEnforcer : Enforcer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.RegExpEnforcer"/> class.
		/// </summary>
		public RegExpEnforcer()
		{
		}
		
		/// <summary>
		/// Get/set the regular expression to use in Check
		/// </summary>
		public Regex Regex { get; set; }
		
		
		#region implemented abstract members of Missing.Validation.Enforcer
		public override string Check(object input)
		{
			if (this.Regex == default(Regex))
			{
				throw new InvalidOperationException("You must supply me with a regular expression through my property 'Regex' before running 'Check'");
			}
			
			if ( !(input is string) )
			{
				throw new ArgumentException("Wrong input type. I only know how to handle strings");
			}
			
			if (!this.Regex.IsMatch((string)input))
			{
				return "Input does not match the specified regular expression";
			}
			
			return String.Empty;
		}
		#endregion
	}
}

