using Microsoft.EntityFrameworkCore;

namespace CRUD_Web_MES.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<u10000> u10000 { get; set; }
        public DbSet<u20000> u20000 { get; set; }
    }
}
