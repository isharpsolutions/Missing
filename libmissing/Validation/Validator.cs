using System;
using Missing.Reflection;
using System.Reflection;
using System.Linq;

namespace Missing.Validation
{
	/// <summary>
	/// Holds methods for Validation purposes
	/// </summary>
	public static class Validator
	{
		#region Validate
		/// <summary>
		/// Validate the specified input
		/// </summary>
		/// <param name="input">
		/// The model that should be validated
		/// </param>
		/// <typeparam name="T">
		/// The type of the model
		/// </typeparam>
		/// <exception cref="ArgumentException">
		/// Thrown if a proper <see cref="ValidationSpecification"/> cannot be found
		/// </exception>
		public static ValidationResult Validate<T>(T input) where T : class
		{
			ValidationResult result = new ValidationResult();
		
			//
			// find validation specification class
			//
			string nameOfBaseValidationSpecification = typeof(ValidationSpecification<String>).Name;
			nameOfBaseValidationSpecification = nameOfBaseValidationSpecification.Remove(nameOfBaseValidationSpecification.IndexOf('`'));
			
			// type name of the actual validation specification
			string vsTypeName = String.Format("{0}{1}", input.GetType().Name, nameOfBaseValidationSpecification);
			
			Type vsType = null;
			
			try
			{
				// we do not want to search for types in "System...."
				Assembly[] assemblies = AssemblyHelper.GetAssemblies(y => !y.FullName.StartsWith("System") && !y.FullName.StartsWith("mscorlib"));
				vsType = TypeHelper.GetType(y => y.FullName.EndsWith(vsTypeName), assemblies);
			}
			
			catch (Exception)
			{
				throw new ArgumentException(String.Format("I was unable to find a validation specification for '{0}'. It should be called '{1}'", input.GetType().Name, vsTypeName));
			}
			
			//
			// get validation specification
			//
			ValidationSpecification<T> vs = TypeHelper.CreateInstance<ValidationSpecification<T>>(vsType);
			
			ValidationError error;
			// run through each element in the specification
			foreach (FieldSpecification field in vs.Fields)
			{
				// check the value of the field
				error = ValidateField<T>(field, input);
				
				if (error != default(ValidationError))
				{
					// the input was not valid
					
					result.Errors.Add(error);
				}
			}
			
			return result;
		}
		#endregion Validate
		
		#region Validate field helper
		/// <summary>
		/// Validates a specific field based on a field specification
		/// and an instance of the model
		/// </summary>
		/// <returns>
		/// An instance of <see cref="ValidationError"/> if field's value
		/// in the model does not correspond with the field specification.
		/// If the value is valid, then default(ValidationError) is returned.
		/// </returns>
		/// <param name="field">
		/// The field specification
		/// </param>
		/// <param name="input">
		/// The input model
		/// </param>
		/// <typeparam name="T">
		/// The model type
		/// </typeparam>
		private static ValidationError ValidateField<T>(FieldSpecification field, T input) where T : class
		{
			PropertyData pd = TypeHelper.GetPropertyData(input, field.PropertyPath.Parts);
			
			var val = pd.Value;
			
			#region Is required
			if (field.IsRequired)
			{
				if (val is string)
				{
					if (String.IsNullOrWhiteSpace((string)val))
					{
						return new ValidationError(field.PropertyPath.AsString(), "Field is required but was 'null', 'String.Empty' or consisted of only whitespace.");
					}
				}
				
				else if (val == null)
				{
					return new ValidationError(field.PropertyPath.AsString(), "Field is required but was 'null'");
				}
			}
			
			else
			{
				// if the field is not required
				// and it has no value, skip the rest
				// of the validation
				
				
				if (val is string)
				{
					if (String.IsNullOrWhiteSpace((string)val))
					{
						return default(ValidationError);
					}
				}
				
				else if (val == null)
				{
					return default(ValidationError);
				}
			}
			#endregion Is required
			
			#region Length
			if (val is string)
			{
				if (field.MaxLength > 0)
				{
					if ( ((string)val).Length > field.MaxLength )
					{
						return new ValidationError(field.PropertyPath.AsString(), "Value exceeds max length of '{0}'", field.MaxLength);
					}
				}
				
				if (field.MinLength >= 0)
				{
					if ( ((string)val).Length < field.MinLength )
					{
						return new ValidationError(field.PropertyPath.AsString(), "Value is shorter than allowed minimum length of '{0}'", field.MinLength);
					}
				}
			}
			#endregion Length
			
			#region Enforcer
			if (field.Enforcer != default(Enforcer))
			{
				string enforcerResult = String.Empty;
				
				try
				{
					enforcerResult = field.Enforcer.Check(val);
				}
				catch (Exception ex)
				{
					enforcerResult = ex.Message;
				}
				
				if (!enforcerResult.Equals(String.Empty))
				{
					return new ValidationError(field.PropertyPath.AsString(), enforcerResult) {
						EnforcerName = field.Enforcer.GetType().FullName
					};
				}
			}
			#endregion Enforcer
			
			return default(ValidationError);
		}
		#endregion Validate field helper
	}
}