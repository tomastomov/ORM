using System;
using System.Collections.Generic;
using System.Text;

namespace ORMPlayground
{
    public class MySimpleEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
