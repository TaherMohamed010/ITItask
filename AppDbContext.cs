using System.Data.Entity;

namespace EFcrud2
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext"){}
        public DbSet<Customer> Customers { get; set; }
    }
}
