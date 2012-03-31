using System;
using System.Reflection;
using Missing.Validation.Enforcers;
using System.Text.RegularExpressions;

namespace Missing.Validation
{
	/// <summary>
	/// Specification of how to validate a specific field
	/// </summary>
	public class FieldSpecification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.FieldSpecification"/> class.
		/// </summary>
		public FieldSpecification()
		{
			this.PropertyPath = new PropertyPath();
		}
		
		/// <summary>
		/// Get/set the property path
		/// </summary>
		internal PropertyPath PropertyPath { get; set; }
		
		/// <summary>
		/// Get the name of the underlying property
		/// </summary>
		internal string Name
		{
			get
			{
				return this.PropertyPath.FieldName;
			}
		}
		
		#region Required
		private bool isRequired = false;
		
		/// <summary>
		/// Get/set whether this field is required
		/// </summary>
		internal bool IsRequired
		{
			get { return this.isRequired; }
			set { this.isRequired = value; }
		}
		
		/// <summary>
		/// Marks this field as being required
		/// </summary>
		/// <remarks>
		/// Currently we do not support "Required"
		/// for fields of types where default(..type..) = null
		/// </remarks>
		public FieldSpecification Required()
		{
			this.IsRequired = true;
			
			return this;
		}
		#endregion Required
		
		#region Length
		private int maxLength = -1;
		private int minLength = -1;
		
		/// <summary>
		/// Get/set maximum allowed length of the value
		/// </summary>
		internal int MaxLength
		{
			get { return this.maxLength; }
			set { this.maxLength = value; }
		}
		
		/// <summary>
		/// Get/set minimum allowed length of the value
		/// </summary>
		internal int MinLength
		{
			get { return this.minLength; }
			set { this.minLength = value; }
		}
		
		/// <summary>
		/// Maximum allowed length of the value
		/// </summary>
		/// <param name="maxLength">
		/// Length
		/// </param>
		/// <remarks>
		/// This is currently only used when validating strings
		/// </remarks>
		public FieldSpecification Length(int maxLength)
		{
			this.MaxLength = maxLength;
			
			return this;
		}
		
		/// <summary>
		/// Maximum and minimum length of the value
		/// </summary>
		/// <param name="maxLength">
		/// Max length.
		/// </param>
		/// <param name="minLength">
		/// Minimum length.
		/// </param>
		/// <remarks>
		/// This is currently only used when validating strings
		/// </remarks>
		public FieldSpecification Length(int maxLength, int minLength)
		{
			this.MaxLength = maxLength;
			this.MinLength = minLength;
			
			return this;
		}
		#endregion Length
		
		#region Enforcer
		/// <summary>
		/// The enforcer (may be null)
		/// </summary>
		private Enforcer enforcer = default(Enforcer);
		
		/// <summary>
		/// Get/set the enforcer to use
		/// </summary>
		/// <remarks>
		/// If the value is default(Enforcer) then
		/// an enforcer should not be used
		/// </remarks>
		internal Enforcer Enforcer
		{
			get { return this.enforcer; }
			set { this.enforcer = value; }
		}
		#endregion Enforcer
		
		/// <summary>
		/// The value must be a valid email address
		/// </summary>
		public FieldSpecification AllowedEmail()
		{
			this.Enforcer = new EmailEnforcer();
			
			return this;
		}
		
		/// <summary>
		/// Use the supplied regular expression for validation
		/// </summary>
		/// <param name="regex">
		/// The Regex
		/// </param>
		public FieldSpecification Allowed(Regex regex)
		{
			this.Enforcer = new RegExpEnforcer() {
				Regex = regex
			};
			
			return this;
		}
		
		/// <summary>
		/// Use the supplied enforcer for validation
		/// </summary>
		/// <param name="enforcer">
		/// The enforcer
		/// </param>
		public FieldSpecification Allowed(Enforcer enforcer)
		{
			this.Enforcer = enforcer;
			
			return this;
		}
		
		/// <summary>
		/// Creates a regular expression with the given
		/// allowed characters.
		/// 
		/// The regular expression is "case insensitive"
		/// </summary>
		/// <param name="allowedCharacters">
		/// The set of allowed characters, just like you would write
		/// it in a regular expression "[]" block
		/// </param>
		public FieldSpecification Allowed(string allowedCharacters)
		{
			this.Enforcer = new RegExpEnforcer() {
				Regex = new Regex(String.Format("^[{0}]*$", allowedCharacters), RegexOptions.Compiled | RegexOptions.IgnoreCase)
			};
			
			return this;
		}
	}
}