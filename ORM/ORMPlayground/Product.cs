using System;
using System.Collections.Generic;
using System.Text;

namespace ORMPlayground
{
    public class Product
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual MySimpleEntity Entity { get; set; }

        public string EntityId { get; set; }
    }
}
