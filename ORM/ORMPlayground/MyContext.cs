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

        public DatabaseTable<MySimpleEntity> Entities { get; private set; }

        public DatabaseTable<Product> Products { get; private set; }
    }
}
