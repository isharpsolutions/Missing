using System;
using System.Reflection;
using Missing.Validation.Enforcers;
using System.Text.RegularExpressions;
using Missing.Reflection;
using System.Collections.Generic;
using System.Text;
using Missing.Validation.Internal;

namespace Missing.Validation
{
	/// <summary>
	/// Specification of how to validate a specific field
	/// </summary>
	public class FieldSpecification
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.FieldSpecification"/> class.
		/// </summary>
		public FieldSpecification()
		{
			this.PropertyPath = new PropertyPath();
		}
		#endregion Constructors
		
		#region Property path
		/// <summary>
		/// Get/set the property path
		/// </summary>
		internal PropertyPath PropertyPath { get; set; }
		
		/// <summary>
		/// Get the name of the underlying property
		/// </summary>
		internal string Name {
			get {
				return this.PropertyPath.FieldName;
			}
		}
		#endregion Property path
		
		#region Required
		private bool isRequired = false;
		
		/// <summary>
		/// Get/set whether this field is required
		/// </summary>
		internal bool IsRequired {
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
		internal int MaxLength {
			get { return this.maxLength; }
			set { this.maxLength = value; }
		}
		
		/// <summary>
		/// Get/set minimum allowed length of the value
		/// </summary>
		internal int MinLength {
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
		internal Enforcer Enforcer {
			get { return this.enforcer; }
			set { this.enforcer = value; }
		}
		#endregion Enforcer
		
		#region Allowed
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
		#endregion Allowed
		
		#region Invalid values
		/// <summary>
		/// The invalid values.
		/// </summary>
		private List<object> invalidValues = new List<object>();
		
		/// <summary>
		/// Gets a list of invalid values.
		/// </summary>
		internal List<object> InvalidValues {
			get { return this.invalidValues; }
		}
		
		/// <summary>
		/// Add a specific value as being invalid even
		/// though it passes all the other tests.
		/// </summary>
		/// <param name="invalidValue">
		/// The value that is invalid
		/// </param>
		/// <remarks>
		/// You can add as many invalid values as you want
		/// </remarks>
		public FieldSpecification Invalid(object invalidValue)
		{
			this.InvalidValues.Add(invalidValue);
			return this;
		}
		#endregion Invalid values
		
		#region Not empty
		/// <summary>
		/// Whether the field is allowed to be empty
		/// </summary>
		private bool emptyIsAllowed = true;
		
		/// <summary>
		/// Get whether the field is allowed to be empty
		/// </summary>
		internal bool EmptyIsAllowed {
			get { return this.emptyIsAllowed; }
		}
		
		/// <summary>
		/// Specify that the field may not be empty.
		/// </summary>
		/// <remarks>
		/// This is currently only used when validating lists/arrays
		/// </remarks>
		public FieldSpecification NotEmpty()
		{
			this.emptyIsAllowed = false;
			
			return this;
		}
		#endregion Not empty
		
		#region IEnumerable
		/// <summary>
		/// The <see cref="ValidationSpecification"/> to use
		/// for each item in the list
		/// </summary>
		private object itemValidationSpecification = null;
		
		/// <summary>
		/// Gets <see cref="ValidationSpecification"/> to use
		/// for each item in the list
		/// </summary>
		internal dynamic ItemValidationSpecification {
			get { return this.itemValidationSpecification; }
		}
		
		/// <summary>
		/// Get whether this instance has an item-specific
		/// <see cref="ValidationSpecification"/>
		/// </summary>
		internal bool HasItemValidationSpecification {
			get { return this.itemValidationSpecification != null; }
		}
		
		/// <summary>
		/// Define how each item in an <see cref="IEnumerable"/>
		/// should be validated.
		/// </summary>
		/// <param name="fieldMapping">
		/// The field mapping for each item.
		/// </param>
		/// <typeparam name="TItem">
		/// The type of the list item
		/// </typeparam>
		/// <exception cref="ArgumentException">
		/// Thrown if <typeparamref name="TItem"/> is considered a primitive type.
		/// </exception>
		public FieldSpecification Each<TItem>(Action<ValidationSpecification<TItem>> fieldMapping) where TItem : class
		{
			Type type = typeof(TItem);
			if (this.IsPrimitive(type)) {
				throw new ArgumentException(String.Format("The specified type, '{0}', is recognized as a primitive type. You should therefore use 'EachPrimitive<TItem>' instead." +
					"The following are considered primitive: {1}",
				                                          type.FullName,
				                                          this.PrimitivesAsString()));
			}
			
			ValidationSpecification<TItem> spec = new ValidationSpecification<TItem>();
			fieldMapping.Invoke(spec);
			
			this.itemValidationSpecification = spec;
			
			return this;
		}
		
		/// <summary>
		/// Define how each item in an <see cref="IEnumerable"/>
		/// of primitive values should be validated.
		/// 
		/// A primitive value is a type where the instance itself should
		/// be validated, and not individual properties, e.g. a
		/// <see cref="String"/>, <see cref="Int32"/>, <see cref="Decimal"/> etc
		/// </summary>
		/// <param name="valueMapping">
		/// The validation mapping for each item.
		/// </param>
		/// <typeparam name="TItem">
		/// The type of the list item
		/// </typeparam>
		/// <exception cref="ArgumentException">
		/// Thrown if <typeparamref name="TItem"/> is not considered a primitive type.
		/// </exception>
		public FieldSpecification EachPrimitive<TItem>(Action<PrimitiveValidationSpecification<TItem>> valueMapping) where TItem : class
		{
			Type type = typeof(TItem);
			if (!this.IsPrimitive(type)) {
				throw new ArgumentException(String.Format("The specified type, '{0}', is not recognized as a primitive type. The following are considered primitive: {1}",
				                                          type.FullName,
				                                          this.PrimitivesAsString()));
			}
			
			PrimitiveValidationSpecification<TItem> spec = new PrimitiveValidationSpecification<TItem>();
			valueMapping.Invoke(spec);
			
			this.itemValidationSpecification = spec;
			
			return this;
		}
		#endregion IEnumerable
		
		#region Primitives
		/// <summary>
		/// List of types recognized as primitives
		/// </summary>
		private List<Type> primitives = new List<Type>() {
				typeof(Int16),
				typeof(Int32),
				typeof(Int64),
				typeof(bool),
				typeof(string),
				typeof(decimal),
				typeof(float),
				typeof(double),
				typeof(byte),
				typeof(sbyte),
				typeof(UInt16),
				typeof(UInt32),
				typeof(UInt64),
				typeof(char)
			};
		
		/// <summary>
		/// Get a comma separated list of primitives
		/// </summary>
		/// <returns>
		/// Comma separated list of primitives
		/// </returns>
		private string PrimitivesAsString()
		{
			StringBuilder sb = new StringBuilder();
			
			foreach (Type t in this.primitives) {
				sb.Append(t.FullName);
				sb.Append(",");
			}
			
			string res = sb.ToString();
			
			// remove last comma
			res = res.Remove(res.Length - 1);
			
			return res;
		}
		
		/// <summary>
		/// Check whether a given type is primitive
		/// </summary>
		/// <returns>
		/// <c>true</c> if the given type is recognized as primitive; otherwise, <c>false</c>.
		/// </returns>
		/// <param name="type">
		/// The type to check
		/// </param>
		private bool IsPrimitive(Type type)
		{
			return this.primitives.Contains(type);
		}
		#endregion Primitives
		
		#region Number ranges
		private IRange range = null;
		
		internal IRange DefinedRange
		{
			get { return this.range; }
		}
		
		
		/// <summary>
		/// Define the valid range for an integer field
		/// </summary>
		/// <param name="min">
		/// The lowest accepted value
		/// </param>
		/// <param name="max">
		/// The highest accepted value
		/// </param>
		public FieldSpecification Range(int min, int max)
		{
			this.range = new Range<int>() {
				Min = min,
				Max = max
			};
			
			return this;
		}
		
		/// <summary>
		/// Define the valid range for a long field
		/// </summary>
		/// <param name="min">
		/// The lowest accepted value
		/// </param>
		/// <param name="max">
		/// The highest accepted value
		/// </param>
		public FieldSpecification Range(long min, long max)
		{
			this.range = new Range<long>() {
				Min = min,
				Max = max
			};
			
			return this;
		}
		
		/// <summary>
		/// Define the valid range for a float field
		/// </summary>
		/// <param name="min">
		/// The lowest accepted value
		/// </param>
		/// <param name="max">
		/// The highest accepted value
		/// </param>
		public FieldSpecification Range(float min, float max)
		{
			this.range = new Range<float>() {
				Min = min,
				Max = max
			};
			
			return this;
		}
		
		/// <summary>
		/// Define the valid range for a double field
		/// </summary>
		/// <param name="min">
		/// The lowest accepted value
		/// </param>
		/// <param name="max">
		/// The highest accepted value
		/// </param>
		public FieldSpecification Range(double min, double max)
		{
			this.range = new Range<double>() {
				Min = min,
				Max = max
			};
			
			return this;
		}
		
		/// <summary>
		/// Define the valid range for a decimal field
		/// </summary>
		/// <param name="min">
		/// The lowest accepted value
		/// </param>
		/// <param name="max">
		/// The highest accepted value
		/// </param>
		public FieldSpecification Range(decimal min, decimal max)
		{
			this.range = new Range<decimal>() {
				Min = min,
				Max = max
			};
			
			return this;
		}
		#endregion Number ranges
	}
}