using ORM.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IModelDataStorage<TEntity>
    {
        void Add(TEntity key, ModelData modelData);

        ModelData Get(TEntity key);
    }
}
