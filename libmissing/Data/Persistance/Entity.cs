﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Missing.Data.Persistance
{
	/// <summary>
	///     Provides a base class for your objects which will be persisted to the database.
	///     Benefits include the addition of an Id property along with a consistent manner for comparing
	///     entities.
	/// 
	///     Since nearly all of the entities you create will have a type of int Id, this 
	///     base class leverages this assumption.  If you want an entity with a type other 
	///     than int, such as string, then use <see cref = "EntityWithTypedId{IdT}" /> instead.
	/// </summary>
	[Serializable]
	public abstract class Entity : EntityWithTypedId<int>
	{
	}

	/// <summary>
	///     For a discussion of this object, see 
	///     http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx
	/// </summary>
	[Serializable]
	public abstract class EntityWithTypedId<TId> : ComparableObject, IEntityWithTypedId<TId>
	{
		/// <summary>
		///     To help ensure hashcode uniqueness, a carefully selected random number multiplier 
		///     is used within the calculation.  Goodrich and Tamassia's Data Structures and
		///     Algorithms in Java asserts that 31, 33, 37, 39 and 41 will produce the fewest number
		///     of collissions.  See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		///     for more information.
		/// </summary>
		private const int HashMultiplier = 31;

		private int? cachedHashcode;

		/// <summary>
		///     Id may be of type string, int, custom type, etc.
		///     Setter is protected to allow unit tests to set this property via reflection and to allow 
		///     domain objects more flexibility in setting this for those objects with assigned Ids.
		///     It's virtual to allow NHibernate-backed objects to be lazily loaded.
		/// 
		///     This is ignored for XML serialization because it does not have a public setter (which is very much by design).
		///     See the FAQ within the documentation if you'd like to have the Id XML serialized.
		/// </summary>
		[XmlIgnore]
		public virtual TId Id { get; protected set; }

		/// <summary>
		/// Version property. Useful when using NHibernate with concurrency handling
		/// </summary>
		public virtual int Version { get; set; }


		public override bool Equals(object obj)
		{
			var compareTo = obj as EntityWithTypedId<TId>;

			if (ReferenceEquals(this, compareTo))
			{
				return true;
			}

			if (compareTo == null || !GetType().Equals(compareTo.GetTypeUnproxied()))
			{
				return false;
			}

			if (HasSameNonDefaultIdAs(compareTo))
			{
				return true;
			}

			// Since the Ids aren't the same, both of them must be transient to 
			// compare domain signatures; because if one is transient and the 
			// other is a persisted entity, then they cannot be the same object.
			return IsTransient() && compareTo.IsTransient() && HasSameObjectSignatureAs(compareTo);
		}

		public override int GetHashCode()
		{
			if (cachedHashcode.HasValue)
			{
				return cachedHashcode.Value;
			}

			if (IsTransient())
			{
				cachedHashcode = base.GetHashCode();
			}
			else
			{
				unchecked
				{
					// It's possible for two objects to return the same hash code based on 
					// identically valued properties, even if they're of two different types, 
					// so we include the object's type in the hash calculation
					var hashCode = GetType().GetHashCode();
					cachedHashcode = (hashCode * HashMultiplier) ^ Id.GetHashCode();
				}
			}

			return cachedHashcode.Value;
		}

		/// <summary>
		///     Transient objects are not associated with an item already in storage.  For instance,
		///     a Customer is transient if its Id is 0.  It's virtual to allow NHibernate-backed 
		///     objects to be lazily loaded.
		/// </summary>
		public virtual bool IsTransient()
		{
			return Id == null || Id.Equals(default(TId));
		}

		/// <summary>
		///     The property getter for SignatureProperties should ONLY compare the properties which make up 
		///     the "domain signature" of the object.
		/// 
		///     If you choose NOT to override this method (which will be the most common scenario), 
		///     then you should decorate the appropriate property(s) with [DomainSignature] and they 
		///     will be compared automatically.  This is the preferred method of managing the domain
		///     signature of entity objects.
		/// </summary>
		/// <remarks>
		///     This ensures that the entity has at least one property decorated with the 
		///     [DomainSignature] attribute.
		/// </remarks>
		protected override IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties()
		{
			return
				GetType().GetProperties().Where(
					p => Attribute.IsDefined(p, typeof(DomainSignatureAttribute), true));
		}

		/// <summary>
		///     Returns true if self and the provided entity have the same Id values 
		///     and the Ids are not of the default Id value
		/// </summary>
		private bool HasSameNonDefaultIdAs(EntityWithTypedId<TId> compareTo)
		{
			return !IsTransient() && !compareTo.IsTransient() && Id.Equals(compareTo.Id);
		}

		/// <summary>
		/// Gets or sets the state of the record.
		/// </summary>
		/// <value>
		/// The state of the record.
		/// </value>
		public RecordState RecordState
		{
			get; set;
		}
	}
}
