using IFM_ManufacturingExecutionSystems.Models.Database;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<IFM_ManufacturingExecutionSystems.Models.Database.aa0003> aa0003_1 { get; set; }
        public DbSet<IFM_ManufacturingExecutionSystems.Models.Database.aa0004> aa0004_1 { get; set; }
        public DbSet<IFM_ManufacturingExecutionSystems.Models.Database.aa0005> aa0005_1 { get; set; }
        public DbSet<IFM_ManufacturingExecutionSystems.Models.Database.aa0006> aa0006_1 { get; set; }

    }
}
