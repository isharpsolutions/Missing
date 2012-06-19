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
	public class RepositoryWithTypedId<TEntity, TId> : IRepositoryWithTypedId<TEntity, TId> where TEntity : IEntityWithTypedId<TId>
	{
		private string connectionString = String.Empty;
		private string databaseName = String.Empty;
		
		#region Config stuff
		private string GetCollectionName()
		{
			return MongoMappings.GetInstance().GetCollectionNameFor(typeof(TEntity));
		}
		
		private void GetConfig()
		{
			this.connectionString = ConfigurationManager.ConnectionStrings["mongo"].ConnectionString;
			MongoUrlBuilder builder = new MongoUrlBuilder(this.connectionString);
			this.databaseName = builder.DatabaseName;
		}
		
		private MongoServer GetServer()
		{
			if (this.connectionString.Equals(String.Empty))
			{
				this.GetConfig();
			}
			
			return MongoServer.Create(this.connectionString);
		}
		
		private MongoDatabase GetDatabase()
		{
			return this.GetServer().GetDatabase(this.databaseName);
		}
		
		private MongoCollection<TEntity> GetCollection()
		{
			return this.GetDatabase().GetCollection<TEntity>(this.GetCollectionName());
		}
		#endregion Config stuff
		
		#region IRepositoryWithTypedId[T,TId] implementation
		public TEntity Get(TId id)
		{
			TEntity result = this.GetByPredicate(y => y.RecordState != RecordState.Deleted && y.Id.Equals(id)).FirstOrDefault();
						
			return result;
		}

		public IQueryable<TEntity> GetAll()
		{
			return this.GetByPredicate(y => y.RecordState != RecordState.Deleted);
		}

		public IQueryable<TEntity> GetByPredicate(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
		{
			return this.GetCollection().AsQueryable<TEntity>().Where(predicate);
		}

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

		public void Delete(TEntity entity)
		{
			entity.RecordState = RecordState.Deleted;
			
			// no reason to commit a transient entity
			if (!entity.IsTransient())
			{
				this.Commit(entity);
			}
		}

		public void Purge(TEntity entity)
		{
			MongoCollection<TEntity> collection = this.GetCollection();
			
			IMongoQuery query = Query.EQ("_id", BsonValue.Create(entity.Id));

			collection.Remove(query, RemoveFlags.Single);
		}

		public IDbContext DbContext
		{
			get { throw new NotImplementedException(); }
		}
		#endregion
	}
}