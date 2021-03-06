﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Missing.Data.Persistance.DataInterfaces;

namespace Missing.Data.Persistance
{
    /// <summary>
    ///     Provides a standard interface for Repositories which are data-access mechanism agnostic.
    /// 
    ///     Since nearly all of the domain objects you create will have a type of int Id, this 
    ///     base IRepository assumes that.  If you want an entity with a type 
    ///     other than int, such as string, then use <see cref = "IRepositoryWithTypedId{T, TId}" />.
    /// </summary>
    public interface IRepository<T> : IRepositoryWithTypedId<T, int> where T : IEntityWithTypedId<int> { }

    public interface IRepositoryWithTypedId<T, TId> where T : IEntityWithTypedId<TId>
    {
        /// <summary>
        /// Provides a handle to application wide DB activities such as committing any pending changes,
        /// beginning a transaction, rolling back a transaction, etc.
        /// </summary>
        IDbContext DbContext { get; }

        /// <summary>
        /// Returns null if a row is not found matching the provided Id.
        /// </summary>
        T Get(TId id);

        IQueryable<T> GetAll();

        IQueryable<T> GetByPredicate(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// For entities with automatically generated Ids, such as identity or Hi/Lo, SaveOrUpdate may 
        /// be called when saving or updating an entity.  If you require separate Save and Update
        /// methods, you'll need to extend the base repository interface when using NHibernate.
        /// 
        /// Updating also allows you to commit changes to a detached object.  More info may be found at:
        /// http://www.hibernate.org/hib_docs/nhibernate/html_single/#manipulatingdata-updating-detached
        /// </summary>
        T Commit(T entity);

        /// <summary>
        /// Flag an entity as deleted by updating the <see cref="RecordState"/> to deleted
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

		/// <summary>
		/// Permanently remove an entity from the datastore
		/// </summary>
		/// <remarks>The Missing.NHibernateProvider.Repository commits the purge (deletion) immediately; 
		/// see that class for details.</remarks>
		void Purge(T entity);
    }
}
