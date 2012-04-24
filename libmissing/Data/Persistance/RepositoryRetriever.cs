using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Missing.Data.Persistance.DataInterfaces;
using Microsoft.Practices.ServiceLocation;

namespace Missing.Data.Persistance
{
    public static class RepositoryRetriever
    {
        public static object CreateEntityRepositoryFor(Type entityType, Type idType)
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
