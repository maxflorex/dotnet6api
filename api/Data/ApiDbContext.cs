using api.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApiDbContext: DbContext
    {

        public ApiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { set; get; }

    }
}
