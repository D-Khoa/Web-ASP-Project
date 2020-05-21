using MES_IFM_MVC.Models.Account;
using MES_IFM_MVC.Models.Process;
using Microsoft.EntityFrameworkCore;

namespace MES_IFM_MVC.Models
{
    public class MESContext : DbContext
    {
        public MESContext(DbContextOptions<MESContext> options) : base(options)
        {
        }

        public DbSet<aa0001> aa0001 { get; set; }
        public DbSet<aa0002> aa0002 { get; set; }
        public DbSet<aa0003> aa0003 { get; set; }

    }
}
