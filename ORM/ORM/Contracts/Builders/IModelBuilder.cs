using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts.Builders
{
    public interface IModelBuilder
    {
        IEntityBuilder<TEntity> Entity<TEntity>();
    }
}
