using EmployeesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

    }
}
