using System;
using Missing.Reflection;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Missing.Reflection.Extensions;
using Missing.Validation.Validators;
using System.Collections;

namespace Missing.Validation
{
	/// <summary>
	/// Holds methods for Validation purposes
	/// </summary>
	public static class Validator
	{
		/// <summary>
		/// The score for a given type
		/// </summary>
		private class TypeMatchScore
		{
			public Type Type { get; set; }
			public int Score { get; set; }
		}
		
		/// <summary>
		/// Type match score comparer.
		/// </summary>
		private class TypeMatchScoreComparer : IComparer<TypeMatchScore>
		{
			#region IComparer[TypeMatchScore] implementation
			/// <summary>
			/// Compare the specified x and y.
			/// </summary>
			/// <param name='x'>
			/// X.
			/// </param>
			/// <param name='y'>
			/// Y.
			/// </param>
			public int Compare(TypeMatchScore x, TypeMatchScore y)
			{
				return x.Score.CompareTo(y.Score);
			}
			#endregion
		}
		
		#region Validate
		/// <summary>
		/// Get a score for how good a match the given
		/// specification type is to the given entity type.
		/// </summary>
		/// <returns>
		/// The type match score.
		/// </returns>
		/// <param name="entity">
		/// The entity type
		/// </param>
		/// <param name="specification">
		/// The specification type
		/// </param>
		private static TypeMatchScore GetTypeMatchScore(Type entity, Type specification)
		{
			TypeMatchScore res = new TypeMatchScore();
			res.Type = specification;
			res.Score = 0;
			
			//
			// same namespace is a good clue
			//
			if (entity.Namespace.Equals(specification.Namespace))
			{
				res.Score += 10;
			}
			
			//
			// in a sub-namespace is also good
			//
			if (specification.Namespace.StartsWith(entity.Namespace))
			{
				res.Score += 5;
			}
			
			//
			// begins with the entity name (as per MS framework design guidelines)
			// entity: SimpleModel
			// spec: SimpleModelValidationSpecification
			//
			if (specification.Name.StartsWith(entity.Name))
			{
				res.Score += 1;
			}
			
			//
			// matches the default convention name
			// this is to allow the situation where multiple specifications
			// are defined in the same namespace and they all follow the
			// MS framework design guidelines.
			//
			string defaultConventionName = String.Format("{0}{1}", entity.Name, typeof(ValidationSpecification<>).GetNonGenericName());
			if (specification.Name.Equals(defaultConventionName))
			{
				res.Score += 1;
			}
			
			return res;
		}
		
		/// <summary>
		/// Validate the specified input.
		/// 
		/// The validation is performed after finding a proper <see cref="ValidationSpecification"/>
		/// </summary>
		/// <param name="input">
		/// The model that should be validated
		/// </param>
		/// <typeparam name="T">
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
		public static ValidationResult Validate<T>(T input) where T : class
		{
			Type vsType = null;
			
			try
			{
				// we do not want to search for types in "System...."
				Assembly[] assemblies = AssemblyHelper.GetAssemblies(y => !y.FullName.StartsWith("System") && !y.FullName.StartsWith("mscorlib"));
				
				// find all possible validation specifications
				List<Type> possibleMatches = TypeHelper.GetTypes(y => y.IsSubclassOf(typeof(ValidationSpecification<T>)), assemblies);
			
				// skip the scoring if there is only 1 match
				if (possibleMatches.Count == 1)
				{
					vsType = possibleMatches[0];
				}
			
				else
				{
					List<TypeMatchScore> scores = new List<TypeMatchScore>();
				
					// calculate score for all the matches
					foreach (Type cur in possibleMatches)
					{
						scores.Add(GetTypeMatchScore(input.GetType(), cur));
					}
				
					// sort the scores in ascending order
					scores.Sort(new TypeMatchScoreComparer());
				
					// if the two highest scores have the same value, then we
					// cannot select between them... notify the user
					if (scores[scores.Count-1].Score == scores[scores.Count-2].Score)
					{
						throw new ArgumentException(String.Format("There was multiple equally valid validation specifications for '{0}'. Consider using the Validator.Validate<T>(input, ValidationSpecification<T>) overload.", input.GetType().Name));
					}
					
					// highest scoring type wins :)
					vsType = scores[scores.Count-1].Type;
				}
			}
			
			catch (Exception)
			{
				throw new ArgumentException(String.Format("I was unable to find a validation specification for '{0}'. It should be called '{0}ValidationSpecification'", input.GetType().Name));
			}
			
			//
			// get validation specification
			//
			ValidationSpecification<T> vs = TypeHelper.CreateInstance<ValidationSpecification<T>>(vsType);
			
			// run the actual validation :)
			return Validate<T>(input, vs);
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
		/// <typeparam name="T">
		/// The type of the model
		/// </typeparam>
		public static ValidationResult Validate<T>(T input, ValidationSpecification<T> specification) where T : class
		{
			ValidationResult result = new ValidationResult();
			
			ValidationError error;
			
			// run through each element in the specification
			foreach (FieldSpecification field in specification.Fields)
			{
				// check the value of the field
				error = ValidateField<T>(field, input);
				
				if (error != default(ValidationError))
				{
					// the input was not valid
					
					result.Errors.Add(error);
				}
				
				//
				// if the field is a list'ish type, we need to
				// validate each item
				//
				if (field.HasItemValidationSpecification)
				{
					PropertyData pd = TypeHelper.GetPropertyData(input, field.PropertyPath);
					
					if (pd.PropertyInfo.PropertyType.ImplementsInterface(typeof(IEnumerable)))
					{
						result.Merge(ValidateIEnumerable<T>(input, specification, field, pd));
					}
				}
			}
			
			return result;
		}
		#endregion Validate
		
		private static ValidationResult ValidateIEnumerable<T>(T input, ValidationSpecification<T> specification, FieldSpecification field, PropertyData pd) where T : class
		{
			ValidationResult result = new ValidationResult();
			
			object valspec = field.ItemValidationSpecification;
			
			IEnumerable enumerable = ((IEnumerable)pd.Value);
			
			IEnumerator enumerator = enumerable.GetEnumerator();
			
			// list is empty... no need to do more
			if (!enumerator.MoveNext())
			{
				return result;
			}
			
			object curItemFromProperty = enumerator.Current;
			
			Type curItemType = curItemFromProperty.GetType();
			
			MethodInfo generic = GetValidateMethod(curItemType);
			
			string fieldPropertyPath = field.PropertyPath.AsString();
			
			int itemIndex = -1;
			foreach (var item in (IEnumerable)pd.Value)
			{
				itemIndex++;
				object subResObject = generic.Invoke(null, new object[] { item, valspec });
				ValidationResult subRes = (ValidationResult)subResObject;
				
				subRes.PrependAllPropertyPathsWith(String.Format("{0}[{1}]", fieldPropertyPath, itemIndex));
				
				result.Merge(subRes);
			}
			
			return result;
		}
		
		private static MethodInfo GetValidateMethod(Type typeToValidate)
		{
			MethodInfo result = null;
			
			var allMethods = typeof(Validator).GetMethods(BindingFlags.Public | BindingFlags.Static);
			MethodInfo foundMi = allMethods.FirstOrDefault(
				mi => mi.Name == "Validate" && mi.GetParameters().Count() == 2
			);
			
			if (foundMi != null)
			{
				result = foundMi.MakeGenericMethod(new Type[] { typeToValidate });
			}
			
			return result;
		}
		
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
			PropertyData pd = TypeHelper.GetPropertyData(input, field.PropertyPath);
			
			return ValidatorFactory
						.GetValidatorFor(pd.PropertyInfo.PropertyType)
						.ValidateField<T>(field, input, pd);
		}
		#endregion Validate field helper
	}
}