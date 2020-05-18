using MES_IFM_Web.Models.Account;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MES_IFM_Web.Models
{
    public class MESContext : DbContext
    {
        public MESContext(DbContextOptions<MESContext> options):base(options)
        {
        }

        public DbSet<aa0001> Aa0001 { get; set; }
    }
}
