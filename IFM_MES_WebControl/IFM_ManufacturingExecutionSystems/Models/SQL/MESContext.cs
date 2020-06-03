using IFM_ManufacturingExecutionSystems.Models.Database;
using Microsoft.EntityFrameworkCore;
using IFM_ManufacturingExecutionSystems.Models.MVC;

namespace IFM_ManufacturingExecutionSystems.Models.SQL
{
    public class MESContext : DbContext
    {
        public MESContext(DbContextOptions<MESContext> options) : base(options)
        {
        }

        public DbSet<aa0001> aa0001 { get; set; }
        public DbSet<aa0002> aa0002 { get; set; }
        public DbSet<aa0001> aa0003 { get; set; }
        public DbSet<aa0002> aa0004 { get; set; }
        public DbSet<aa0001> aa0005 { get; set; }
        public DbSet<aa0002> aa0006 { get; set; }
        public DbSet<IFM_ManufacturingExecutionSystems.Models.MVC.Register> Register { get; set; }
        public DbSet<IFM_ManufacturingExecutionSystems.Models.MVC.Login> Login { get; set; }
        public DbSet<IFM_ManufacturingExecutionSystems.Models.MVC.Status> Status { get; set; }
    }
}
