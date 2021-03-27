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
                .HasProperty(e => e.Products)
                .Ignore();

            builder.Entity<Product>()
                .HasProperty(p => p.Entity)
                .Ignore();

            builder.Entity<MySimpleEntity>()
                .HasKey(k => k.Id);

            builder.Entity<MySimpleEntity>()
                .HasMany(k => k.Products)
                .WithOne(c => c.Entity)
                .HasForeignKey(c => c.EntityId);
        }

        public DatabaseTable<MySimpleEntity> Entities { get; set; }

        public DatabaseTable<Product> Products { get; set; }
    }
}
