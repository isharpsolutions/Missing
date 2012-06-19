using System;
using System.Collections.Generic;

namespace Missing.MongoProvider
{
	/// <summary>
	/// Mapping information for MongoDB
	/// </summary>
	/// <remarks>
	/// Currently this only contains "type -> collection name" mappings
	/// </remarks>
	public class MongoMappings
	{
		/// <summary>
		/// Singleton instance
		/// </summary>
		private static MongoMappings instance = default(MongoMappings);
		
		#region Get instance
		/// <summary>
		/// Get an instance
		/// </summary>
		/// <returns>
		/// The instance
		/// </returns>
		public static MongoMappings GetInstance()
		{
			if (instance == default(MongoMappings))
			{
				instance = new MongoMappings();
			}
			
			return instance;
		}
		#endregion Get instance

		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.MongoProvider.MongoMappings"/> class.
		/// </summary>
		/// <remarks>
		/// You should always use <see cref="GetInstance"/>
		/// </remarks>
		public MongoMappings()
		{
		}

		/// <summary>
		/// "type -> collection name" dictionary
		/// </summary>
		private Dictionary<Type, string> typeToCollectionName = new Dictionary<Type, string>();

		/// <summary>
		/// Add collection name for given type
		/// </summary>
		/// <returns>
		/// MongoMappings instance for chainability
		/// </returns>
		/// <param name="collectionName">
		/// The collection name
		/// </param>
		/// <param name="type">
		/// The type
		/// </param>
		public MongoMappings CollectionNameFor(string collectionName, Type type)
		{
			this.typeToCollectionName.Add(type, collectionName);
			
			return this;
		}

		/// <summary>
		/// Get the collection name for a given type
		/// </summary>
		/// <returns>
		/// The collection name for the given type
		/// </returns>
		/// <param name="type">
		/// The type for which to get the collection name
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if the given type does not have a registered collection name
		/// </exception>
		public string GetCollectionNameFor(Type type)
		{
			if (!this.typeToCollectionName.ContainsKey(type))
			{
				throw new ArgumentException(String.Format("There is no collection name registered for type '{0}'. Call 'MongoMappings.GetInstance().CollectionNameFor(..)' during initialization of your application.", type.FullName));
			}
			
			return this.typeToCollectionName[type];
		}
	}
}