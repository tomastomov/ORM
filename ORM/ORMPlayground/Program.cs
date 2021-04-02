using ORM.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ORMPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MyContext(new DatabaseContextOptions("Server=.;Trusted_Connection=True", "MyORMDB"));

            context.CreateDatabase();

            var query = context.Entities.Where(e => e.Name == "pesho")
                .Select(e => new MySimpleDTO()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age
                })
                .OrderBy(e => e.Id);

            var list = query.ToList();

            foreach (var entity in list)
            {
                Console.WriteLine(entity);
            }
        }

        private class MySimpleDTO
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }

            public override string ToString()
            {
                return $"{Id} {Name} {Age}";
            }
        }
    }
}
