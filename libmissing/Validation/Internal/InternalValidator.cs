using System;
using Missing.Reflection;
using System.Collections.Generic;
using Missing.Collections;
using System.Reflection;
using Missing.Reflection.Extensions;
using System.Collections;
using System.Linq;
using Missing.Validation.Internal.Validators;

namespace Missing.Validation.Internal
{
	internal class InternalValidator
	{
		#region Static: Primitive field name
		/// <summary>
		/// The value used for <see cref="FieldSpecification.FieldName"/> for 
		/// primitive fields
		/// </summary>
		internal static readonly string PrimitiveFieldName = "--PRIMITIVE--";
		#endregion Static: Primitive field name
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Validation.Internal.InternalValidator"/> class.
		/// </summary>
		public InternalValidator()
		{
		}
		#endregion Constructors
		
		#region Find validation specification
		private ValidationSpecification<TModel> FindValidationSpecification<TModel>(TModel input) where TModel : class
		{
			Type vsType = null;
			
			// we do not want to search for types in "System...."
			Assembly[] assemblies = AssemblyHelper.GetAssemblies(y => !y.FullName.StartsWith("System") && !y.FullName.StartsWith("mscorlib"));
			
			// find all possible validation specifications
			List<Type> possibleMatches = TypeHelper.GetTypes(y => y.IsSubclassOf(typeof(ValidationSpecification<TModel>)), assemblies);
		
			// skip the scoring if there is only 1 match
			if (possibleMatches.Count == 1)
			{
				vsType = possibleMatches[0];
			}
		
			else
			{
				OrderedList<TypeMatchScore> scores = new OrderedList<TypeMatchScore>(y => y.Score);
			
				// calculate score for all the matches
				foreach (Type cur in possibleMatches)
				{
					scores.Add(TypeMatchScoreCalculator.Run(input.GetType(), cur));
				}
			
				// if the two highest scores have the same value, then we
				// cannot select between them... notify the user
				if (scores[scores.Count-1].Score == scores[scores.Count-2].Score)
				{
					throw new ArgumentException(String.Format("There was multiple equally valid validation specifications for '{0}'. Consider using the Validator.Validate<TModel>(input, ValidationSpecification<TModel>) overload.", input.GetType().Name));
				}
				
				// highest scoring type wins :)
				vsType = scores[scores.Count-1].Type;
			}
			
			if (vsType == null)
			{
				throw new ArgumentException(String.Format("I was unable to find a validation specification for '{0}'. It should be called '{0}ValidationSpecification'", input.GetType().Name));
			}
			
			//
			// get validation specification
			//
			ValidationSpecification<TModel> vs = TypeHelper.CreateInstance<ValidationSpecification<TModel>>(vsType);
			
			return vs;
		}
		#endregion Find validation specification
		
		#region Validate
		/// <summary>
		/// Find a validation specification and run validation
		/// </summary>
		/// <param name="input">
		/// The input model
		/// </param>
		/// <typeparam name="TModel">
		/// The type of the input model
		/// </typeparam>
		public ValidationResult Validate<TModel>(TModel input) where TModel : class
		{
			ValidationSpecification<TModel> vs = this.FindValidationSpecification<TModel>(input);
			
			// run the actual validation :)
			return Validate<TModel>(input, vs);
		}
		
		/// <summary>
		/// Validate the specified input using the specified validation specification.
		/// </summary>
		/// <param name="input">
		/// The input model
		/// </param>
		/// <param name="specification">
		/// The validation specification
		/// </param>
		/// <typeparam name="TModel">
		/// The type of the model
		/// </typeparam>
		public ValidationResult Validate<TModel>(TModel input, ValidationSpecification<TModel> specification) where TModel : class
		{
			ValidationResult result = new ValidationResult();
			
			ValidationError error;
			
			// run through each element in the specification
			foreach (FieldSpecification field in specification.Fields)
			{
				// check the value of the field
				error = this.ValidateField<TModel>(field, input);
				
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
						result.Merge(this.ValidateIEnumerable<TModel>(input, specification, field, pd));
					}
				}
			}
			
			return result;
		}
		#endregion Validate
		
		#region Validate IEnumerable
		/// <summary>
		/// Run validation on an <see cref="IEnumerable"/> field
		/// </summary>
		/// <returns>
		/// The combined <see cref="ValidationResult"/> for all entries in
		/// the list
		/// </returns>
		/// <param name="input">
		/// The model that is being validated
		/// </param>
		/// <param name="specification">
		/// The validation specification being used
		/// </param>
		/// <param name="field">
		/// The field specification
		/// </param>
		/// <param name="pd">
		/// Info about the property
		/// </param>
		/// <typeparam name="TModel">
		/// The type of the model
		/// </typeparam>
		private ValidationResult ValidateIEnumerable<TModel>(TModel input, ValidationSpecification<TModel> specification, FieldSpecification field, PropertyData pd) where TModel : class
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
			
			// this occurs for arrays that initialized like so
			// string[] strings = new string[3]; => all items are null (or more correct; default(T))
			if (curItemFromProperty == null)
			{
				return result;
			}
			
			Type curItemType = curItemFromProperty.GetType();
			
			MethodInfo generic = this.GetValidateMethod(curItemType);
			
			string fieldPropertyPath = field.PropertyPath.AsString();
			
			int itemIndex = -1;
			foreach (var item in (IEnumerable)pd.Value)
			{
				itemIndex++;
				object subResObject = generic.Invoke(this, new object[] { item, valspec });
				ValidationResult subRes = (ValidationResult)subResObject;
				
				subRes.PrependAllPropertyPathsWith(String.Format("{0}[{1}]", fieldPropertyPath, itemIndex));
				
				result.Merge(subRes);
			}
			
			return result;
		}
		
		/// <summary>
		/// Get a generic reflection invoke-ready <see cref="MethodInfo"/>
		/// for <see cref="InternalValidator.Validate{TModel}"/>
		/// </summary>
		/// <returns>
		/// Generic invoke-ready validate method
		/// </returns>
		/// <param name="typeToValidate">
		/// The instance type to validate
		/// </param>
		/// <example>
		/// <code lang="csharp">
		/// 	ValidationSpecification<TModel> valSpec = .....;
		/// 	string myValue = "Something";
		/// 	MethodInfo validate = this.GetValidateMethod(myValue.GetType());
		/// 	ValidationResult valResult = validate.Invoke(this, new Type[] { myValue, valSpec });
		/// </code>
		/// </example>
		private MethodInfo GetValidateMethod(Type typeToValidate)
		{
			MethodInfo result = null;
			
			var allMethods = this.GetType().GetMethods();
			MethodInfo foundMi = allMethods.FirstOrDefault(
				mi => mi.Name == "Validate" && mi.GetParameters().Count() == 2
			);
			
			if (foundMi == null)
			{
				throw new InvalidOperationException("I was unable to find the validation method");
			}
			
			result = foundMi.MakeGenericMethod(new Type[] { typeToValidate });
			
			return result;
		}
		#endregion Validate IEnumerable
		
		#region Validate field
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
		/// <typeparam name="TModel">
		/// The model type
		/// </typeparam>
		private ValidationError ValidateField<TModel>(FieldSpecification field, TModel input) where TModel : class
		{
			//
			// handle primitive fields
			//
			if (field.PropertyPath.FieldName.Equals(InternalValidator.PrimitiveFieldName))
			{
				return ValidatorFactory
						.GetValidatorFor(input.GetType())
						.ValidatePrimitive(field, input);
			}
			
			//
			// handle complex (non-primitive) fields
			//
			PropertyData pd = TypeHelper.GetPropertyData(input, field.PropertyPath);
			
			return ValidatorFactory
						.GetValidatorFor(pd.PropertyInfo.PropertyType)
						.ValidateField<TModel>(field, input, pd);
		}
		#endregion Validate field
	}
}