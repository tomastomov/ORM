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

            var query = context.Entities.Where(p => p.Name == "pesho")
                .OrderBy(e => e.Age)
                .ThenByDescending(e => e.Id)
                .ToList();

            query[0].Name = "vanio";

            context.SaveChanges();
            //foreach (var entity in query)
            //{
            //    Console.WriteLine(entity);
            //}
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
