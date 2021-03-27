using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    public abstract class DatabaseTable<TEntity> : IQueryable<TEntity>, IOrderedQueryable<TEntity>
    {
        public virtual Type ElementType => throw new NotImplementedException();

        public virtual Expression Expression => throw new NotImplementedException();

        public virtual IQueryProvider Provider => throw new NotImplementedException();

        public virtual IEnumerator<TEntity> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
