using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IModelBuilder
    {
        IEntityBuilder<TEntity> Entity<TEntity>();
    }
}
