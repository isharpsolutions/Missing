using System;
using Missing.Reflection;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Missing.Reflection.Extensions;
using System.Collections;
using Missing.Validation.Internal;

namespace Missing.Validation
{
	/// <summary>
	/// Public entry point for the validation API
	/// </summary>
	public static class Validator
	{
		/// <summary>
		/// Validate the specified input.
		/// 
		/// The validation is performed after finding a proper <see cref="ValidationSpecification"/>
		/// </summary>
		/// <param name="input">
		/// The model that should be validated
		/// </param>
		/// <typeparam name="TModel">
		/// The type of the model
		/// </typeparam>
		/// <exception cref="ArgumentException">
		/// Thrown if a proper <see cref="ValidationSpecification"/> cannot be found
		/// <br/>
		/// Thrown if multiple <see cref="ValidationSpecification"/> was found and they
		/// seem equally valid (see remarks)
		/// </exception>
		/// <remarks>
		/// If multiple <see cref="ValidationSpecification"/> are found for the given input type,
		/// they are scored with the following weight:
		/// <list type="bullet">
		/// 	<item><description>Same namespace</description></item>
		/// 	<item><description>Sub-namespace</description></item>
		/// 	<item><description>Name starts with the name of the entity (SimpleModel => SimpleModelValidationSpecification)</description></item>
		/// 	<item><description>Different namespace</description></item>
		/// 	<item><description>Name follows the default convention "<ModelName>ValidationSpecification"</description></item>
		/// </list>
		/// </remarks>
		public static ValidationResult Validate<TModel>(TModel input) where TModel : class
		{
			InternalValidator val = new InternalValidator();
			
			return val.Validate<TModel>(input);
		}

		/// <summary>
		/// Validates the specified input if a validation specification can be found.
		/// Otherwise a positive validation result is returned.
		/// </summary>
		/// <returns>
		/// The validation result (always positive for inputs with no specification)
		/// </returns>
		/// <param name="input">
		/// The model that should be validated
		/// </param>
		/// <typeparam name="TModel">
		/// The type of the model
		/// </typeparam>
		public static ValidationResult ValidateIfSpecExists<TModel>(TModel input) where TModel : class
		{
			InternalValidator val = new InternalValidator();
			
			return val.ValidateIfSpecExists<TModel>(input);
		}
		
		/// <summary>
		/// Validate the specified input.
		/// </summary>
		/// <param name="input">
		/// The model that should be validated
		/// </param>
		/// <param name="specification">
		/// The validation specification to use
		/// </param>
		/// <typeparam name="TModel">
		/// The type of the model
		/// </typeparam>
		public static ValidationResult Validate<TModel>(TModel input, ValidationSpecification<TModel> specification) where TModel : class
		{
			InternalValidator val = new InternalValidator();
			
			return val.Validate<TModel>(input, specification);
		}
		
		/// <summary>
		/// Validate the specified input.
		/// 
		/// This method is merely a convenience wrapper for the generic versions.
		/// 
		/// It imposes a runtime overhead, by using reflection to determine the type
		/// of the input model and to call the generic validate method to perform
		/// the actual validation.
		/// 
		/// <remarks>
		/// This method should only be used in cases where you do not know the type
		/// of the model at compile time.
		/// </remarks>
		/// </summary>
		/// <param name="input">
		/// The input model instance to validate
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the underlying reflection fails
		/// </exception>
		public static ValidationResult Validate(object input)
		{
			return ObjectBasedWorker("Validate", input);
		}

		/// <summary>
		/// Validates if spec exists.
		/// </summary>
		/// <returns>
		/// The validation result (always positive for inputs with no specification)
		/// </returns>
		/// <param name="input">
		/// The input model instance to validate
		/// </param>
		public static ValidationResult ValidateIfSpecExists(object input)
		{
			return ObjectBasedWorker("ValidateIfSpecExists", input);
		}

		/// <summary>
		/// Common validation method used by validate methods that
		/// takes an "object" input (aka non-generic validate methods).
		/// </summary>
		/// <returns>
		/// The validation result
		/// </returns>
		/// <param name="methodToUse">
		/// The name of the generic method to use
		/// </param>
		/// <param name="input">
		/// The model to validate
		/// </param>
		private static ValidationResult ObjectBasedWorker(string methodToUse, object input)
		{
			ValidationResult val = null;
			
			#region Make generic validate method
			MethodInfo result = null;
			
			var allMethods = typeof(Validator).GetMethods();
			MethodInfo foundMi = allMethods.FirstOrDefault(
				mi => mi.Name == methodToUse && mi.GetParameters().Count() == 1
			);
			
			if (foundMi == null)
			{
				throw new InvalidOperationException("I was unable to find the validation method");
			}
			
			result = foundMi.MakeGenericMethod(new Type[] { input.GetType() });
			#endregion
			
			val = (ValidationResult)result.Invoke(null, new object[] { input });
			
			return val;
		}
	}
}