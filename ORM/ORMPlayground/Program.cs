using ORM.Contracts;
using System;

namespace ORMPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MyContext(new DatabaseContextOptions("Server=.;Trusted_Connection=True", "MyORMDB"));

            context.CreateDatabase();
        }
    }
}
