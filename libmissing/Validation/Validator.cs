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
	}
}