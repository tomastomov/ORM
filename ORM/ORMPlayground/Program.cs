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

            var query = context.Entities.Where(e => e.Id == "pesho" && e.Name == "ivan")
                .OrderBy(e => e.Name);

            var list = query.ToList();
        }
    }
}
