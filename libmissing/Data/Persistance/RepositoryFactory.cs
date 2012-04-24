using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Missing.Data.Persistance.DataInterfaces;

namespace Missing.Data.Persistance
{
    public static class RepositoryFactory
    {
        /// <summary>
        /// Returns a IRepository for a given business object TEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IRepository<TEntity> Get<TEntity>() where TEntity : Entity
        {
            System.Reflection.PropertyInfo p = typeof(TEntity).GetProperty("Id");
            Type t = p.PropertyType;
            object repos = Missing.Data.Persistance.RepositoryRetriever.CreateEntityRepositoryFor(typeof(TEntity));
            return (IRepository<TEntity>)repos;
        }

        /// <summary>
        /// Returns a IRepositoryWithTypedId for a given business object TEntity having id of type TId
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <returns></returns>
        public static IRepositoryWithTypedId<TEntity, TId> Get<TEntity, TId>() where TEntity : EntityWithTypedId<TId>
        {
            return (IRepositoryWithTypedId<TEntity, TId>)Missing.Data.Persistance.RepositoryRetriever.CreateEntityRepositoryFor(typeof(TEntity), typeof(TId));
        }
    }
}
