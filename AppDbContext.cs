using System.Collections.Generic;
using System.Data.Entity;

namespace EFcrud2
{
    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "John", LastName = "Doe", City = "New York", Address = "123 Main St" },
                new Customer { FirstName = "Jane", LastName = "Smith", City = "Los Angeles", Address = "456 Sunset Blvd" },
                new Customer { FirstName = "Ali", LastName = "Hassan", City = "Cairo", Address = "789 Nile Ave" },
                new Customer { FirstName = "Maria", LastName = "Garcia", City = "Madrid", Address = "101 Plaza Mayor" }
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
