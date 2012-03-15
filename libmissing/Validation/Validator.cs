using System;
using Missing.Reflection;
using System.Reflection;

namespace Missing.Validation
{
	public static class Validator
	{
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
				vsType = TypeHelper.GetTypeEndsWith(vsTypeName, false);
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
			foreach (ValidationProperty prop in vs.Properties)
			{
				// check the value of the property
				error = ValidateProperty<T>(prop, input);
				
				if (error != default(ValidationError))
				{
					// the input was not valid
					
					result.Errors.Add(error);
				}
			}
			
			return result;
		}
		
		private static ValidationError ValidateProperty<T>(ValidationProperty prop, T input) where T : class
		{
			PropertyData pd = TypeHelper.GetPropertyData(input, prop.PropertyPath);
			
			var val = pd.Value;
			
			#region Is required
			if (prop.IsRequired)
			{
				if (val is string)
				{
					if (String.IsNullOrWhiteSpace((string)val))
					{
						return new ValidationError(prop.Name, "Property is required but was 'null', 'String.Empty' or consisted of only whitespace.");
					}
				}
				
				else if (val == null)
				{
					return new ValidationError(prop.Name, "Property is required but was 'null'");
				}
			}
			
			else
			{
				// if the property is not required
				// and it has no value, skip the rest
				// of the validation
				if (val == null)
				{
					return default(ValidationError);
				}
			}
			#endregion Is required
			
			#region Length
			if (val is string)
			{
				if (prop.MaxLength > 0)
				{
					if ( ((string)val).Length > prop.MaxLength )
					{
						return new ValidationError(prop.Name, "Value exceeds max length of '{0}'", prop.MaxLength);
					}
				}
				
				if (prop.MinLength >= 0)
				{
					if ( ((string)val).Length < prop.MinLength )
					{
						return new ValidationError(prop.Name, "Value is shorter than allowed minimum length of '{0}'", prop.MinLength);
					}
				}
			}
			#endregion Length
			
			#region Enforcer
			if (prop.Enforcer != default(Enforcer))
			{
				string enforcerResult = String.Empty;
				
				try
				{
					enforcerResult = prop.Enforcer.Check(val);
				}
				catch (Exception ex)
				{
					enforcerResult = ex.Message;
				}
				
				if (!enforcerResult.Equals(String.Empty))
				{
					return new ValidationError(prop.Name, "Enforcer '{0}' says: {1}", prop.Enforcer.GetType(), enforcerResult);
				}
			}
			#endregion Enforcer
			
			return default(ValidationError);
		}
	}
}