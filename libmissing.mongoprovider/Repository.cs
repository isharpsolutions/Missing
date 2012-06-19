using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Missing.Data.Persistance;
using Missing.Data.Persistance.DataInterfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using System.Configuration;

namespace Missing.MongoProvider
{
	/// <summary>
	/// Repository working on a given entity type with a given ID type
	/// </summary>
	/// <typeparam name="TEntity">
	/// The type of the entity being worked on
	/// </typeparam>
	/// <typeparam name="TId">
	/// The type of the ID of the entity
	/// </typeparam>
	public class RepositoryWithTypedId<TEntity, TId> : IRepositoryWithTypedId<TEntity, TId> where TEntity : IEntityWithTypedId<TId>
	{
		/// <summary>
		/// The connection string.
		/// </summary>
		private string connectionString = String.Empty;

		/// <summary>
		/// The name of the database.
		/// </summary>
		private string databaseName = String.Empty;
		
		#region Config stuff
		/// <summary>
		/// Gets the name of the collection to use for the entity
		/// </summary>
		/// <returns>
		/// The collection name for the entity
		/// </returns>
		private string GetCollectionName()
		{
			return MongoMappings.GetInstance().GetCollectionNameFor(typeof(TEntity));
		}

		/// <summary>
		/// Get stuff from configuration file
		/// </summary>
		private void GetConfig()
		{
			this.connectionString = ConfigurationManager.ConnectionStrings["mongo"].ConnectionString;
			MongoUrlBuilder builder = new MongoUrlBuilder(this.connectionString);
			this.databaseName = builder.DatabaseName;
		}

		/// <summary>
		/// Get server instance
		/// </summary>
		/// <returns>
		/// The server
		/// </returns>
		private MongoServer GetServer()
		{
			if (this.connectionString.Equals(String.Empty))
			{
				this.GetConfig();
			}
			
			return MongoServer.Create(this.connectionString);
		}

		/// <summary>
		/// Get database instance
		/// </summary>
		/// <returns>
		/// The database
		/// </returns>
		private MongoDatabase GetDatabase()
		{
			return this.GetServer().GetDatabase(this.databaseName);
		}

		/// <summary>
		/// Get collection instance
		/// </summary>
		/// <returns>
		/// The collection
		/// </returns>
		private MongoCollection<TEntity> GetCollection()
		{
			return this.GetDatabase().GetCollection<TEntity>(this.GetCollectionName());
		}
		#endregion Config stuff
		
		#region IRepositoryWithTypedId[T,TId] implementation
		/// <summary>
		/// Returns null if a row is not found matching the provided Id. 
		/// </summary>
		/// <returns>
		/// The entity instance with the given ID or <c>null</c> if a
		/// mathing entity was not found.
		/// </returns>
		/// <param name="id">
		/// ID of the wanted entity
		/// </param>
		public TEntity Get(TId id)
		{
			TEntity result = this.GetByPredicate(y => y.RecordState != RecordState.Deleted && y.Id.Equals(id)).FirstOrDefault();
						
			return result;
		}

		/// <summary>
		/// Gets all non-deleted entities
		/// </summary>
		/// <returns>
		/// A LINQ collection of all non-deleted entities
		/// </returns>
		public IQueryable<TEntity> GetAll()
		{
			return this.GetByPredicate(y => y.RecordState != RecordState.Deleted);
		}

		/// <summary>
		/// Get all entities matching a custom predicate
		/// 
		/// <remarks>Be aware that you have to check the <see cref="IEntity.RecordState"/> manually</remarks>
		/// </summary>
		/// <returns>
		/// A LINQ collection of all entities matching the given predicate
		/// </returns>
		/// <param name="predicate">
		/// The predicate
		/// </param>
		public IQueryable<TEntity> GetByPredicate(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
		{
			return this.GetCollection().AsQueryable<TEntity>().Where(predicate);
		}

		/// <summary>
		/// Commits an entity to Mongo. Whether to use
		/// "insert" or "save" is automatically determined.
		/// </summary>
		/// <returns>
		/// The updated entity (as in ID will have been set etc)
		/// </returns>
		/// <param name="entity">
		/// The entity to commit
		/// </param>
		public TEntity Commit(TEntity entity)
		{
			MongoCollection<TEntity> collection = this.GetCollection();
			
			if (entity.IsTransient())
			{
				collection.Insert(entity);
			}
			else
			{
				collection.Save(entity);
			}
			
			return entity;
		}

		/// <summary>
		/// Flags the entity as deleted
		/// and commits the entity (if entity already exists in
		/// the collection)
		/// </summary>
		/// <param name="entity">
		/// The entity to delete
		/// </param>
		public void Delete(TEntity entity)
		{
			entity.RecordState = RecordState.Deleted;
			
			// no reason to commit a transient entity
			if (!entity.IsTransient())
			{
				this.Commit(entity);
			}
		}

		/// <summary>
		/// Permanently remove an entity from Mongo
		/// </summary>
		/// <remarks>
		/// The Missing.NHibernateProvider.Repository commits the purge (deletion) immediately; see that class for details.
		/// </remarks>
		/// <param name="entity">
		/// The entity to purge
		/// </param>
		public void Purge(TEntity entity)
		{
			MongoCollection<TEntity> collection = this.GetCollection();
			
			IMongoQuery query = Query.EQ("_id", BsonValue.Create(entity.Id));

			collection.Remove(query, RemoveFlags.Single);
		}

		/// <summary>
		/// Provides a handle to application wide DB activities such as committing any pending changes, beginning a
		/// transaction, rolling back a transaction, etc. 
		/// </summary>
		/// <exception cref="NotImplementedException">
		/// Always thrown
		/// </exception>
		public IDbContext DbContext
		{
			get { throw new NotImplementedException(); }
		}
		#endregion
	}
}