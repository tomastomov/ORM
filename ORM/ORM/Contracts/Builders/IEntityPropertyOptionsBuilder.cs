using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts.Builders
{
    public interface IEntityPropertyOptionsBuilder<TEntity>
    {
        IEntityPropertyOptionsBuilder<TEntity> Ignore();
    }
}
