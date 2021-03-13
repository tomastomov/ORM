using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORMPlayground
{
    public class MyContext : DatabaseContext
    {
        public MyContext(DatabaseContextOptions options) : base(options)
        {
        }

        public override void OnModelCreating(IModelBuilder builder)
        {
            //TODO fix builders
            builder.Entity<Product>()
                .HasKey(k => k.Id)
                .HasOne(k => k.Entity)
                .WithMany(p => p.Products)
                .HasForeignKey(k => k.EntityId);

            builder.Entity<MySimpleEntity>()
                .HasKey(k => k.Id)
                .HasMany(p => p.Products)
                .WithOne(k => k.Entity);
        }

        public DatabaseTable<MySimpleEntity> Entities { get; private set; }

        public DatabaseTable<Product> Products { get; private set; }
    }
}
