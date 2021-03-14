using ORM.Contracts;
using ORM.Contracts.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.Builders
{
    internal class ModelBuilder : IModelBuilder
    {
        private readonly IModelDataStorage<Type> storage_;
        public ModelBuilder()
        {
            storage_ = new ModelDataStorage<Type>();
        }
        public IEntityBuilder<TEntity> Entity<TEntity>()
        {
            storage_.Add(typeof(TEntity), new ModelData());
            return new EntityBuilder<TEntity>(storage_);
        }
    }
}
