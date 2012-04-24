using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Missing.Data.Persistance.DataInterfaces;
using Microsoft.Practices.ServiceLocation;

namespace Missing.Data.Persistance
{
    internal static class RepositoryRetriever
    {
        /// <summary>
        /// Creates an IRepository{entityType}
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        internal static object CreateEntityRepositoryFor(Type entityType)
        {
            Type concreteRepositoryType = typeof(IRepository<>)
                .MakeGenericType(new[] { entityType});

            object repository = ServiceLocator.Current.GetService(concreteRepositoryType);

            if (repository == null)
                throw new TypeLoadException(concreteRepositoryType.ToString() + " has not been registered with IoC");

            return repository;
        }

        /// <summary>
        /// Creates an IRepositoryWithTypedId{entityType, idType}
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="idType"></param>
        /// <returns></returns>
        internal static object CreateEntityRepositoryFor(Type entityType, Type idType)
        {
            Type concreteRepositoryType = typeof(IRepositoryWithTypedId<,>)
                .MakeGenericType(new[] { entityType, idType });

            object repository = ServiceLocator.Current.GetService(concreteRepositoryType);

            if (repository == null)
                throw new TypeLoadException(concreteRepositoryType.ToString() + " has not been registered with IoC");

            return repository;
        }
    }
}
