using Microsoft.EntityFrameworkCore;
using ZoobookTest.Domain.Employee;

namespace ZoobookTest.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
