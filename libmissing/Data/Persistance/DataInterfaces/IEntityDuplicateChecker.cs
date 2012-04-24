using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Data.Persistance.DataInterfaces
{
    public interface IEntityDuplicateChecker
    {
        bool DoesDuplicateExistWithTypedIdOf<TId>(IEntityWithTypedId<TId> entity);
    }
}
