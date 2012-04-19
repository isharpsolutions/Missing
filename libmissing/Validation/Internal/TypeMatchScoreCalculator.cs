using System;
using Missing.Reflection.Extensions;

namespace Missing.Validation.Internal
{
	/// <summary>
	/// Calculates how well an entity type and a
	/// validation specification type suits each other.
	/// 
	/// This is used by <see cref="InternalValidator"/> to
	/// find a validation specification for a given model.
	/// </summary>
	internal static class TypeMatchScoreCalculator
	{
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
		public static TypeMatchScore Run(Type entity, Type specification)
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
	}
}