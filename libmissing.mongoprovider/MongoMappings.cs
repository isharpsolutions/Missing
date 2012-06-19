using System;
using System.Collections.Generic;

namespace Missing.MongoProvider
{
	public class MongoMappings
	{
		private static MongoMappings instance = default(MongoMappings);
		
		#region Get instance
		public static MongoMappings GetInstance()
		{
			if (instance == default(MongoMappings))
			{
				instance = new MongoMappings();
			}
			
			return instance;
		}
		#endregion Get instance
		
		public MongoMappings()
		{
		}
		
		private Dictionary<Type, string> typeToCollectionName = new Dictionary<Type, string>();
		
		public MongoMappings CollectionNameFor(string collectionName, Type type)
		{
			this.typeToCollectionName.Add(type, collectionName);
			
			return this;
		}
		
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