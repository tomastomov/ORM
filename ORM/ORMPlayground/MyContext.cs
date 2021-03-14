using ORM.Contracts;
using ORM.Contracts.Builders;
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
            builder.Entity<Product>()
                .HasKey(k => k.Id);

            builder.Entity<MySimpleEntity>()
                .HasKey(k => k.Id);

            builder.Entity<MySimpleEntity>()
                .HasMany(k => k.Products)
                .WithOne(c => c.Entity)
                .HasForeignKey(c => c.EntityId);

            builder.Entity<Product>()
                .HasOne(c => c.Entity)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.EntityId);
        }

        public DatabaseTable<MySimpleEntity> Entities { get; private set; }

        public DatabaseTable<Product> Products { get; private set; }
    }
}
