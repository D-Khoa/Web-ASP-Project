using Microsoft.EntityFrameworkCore;

namespace IFM_MES_WebControl.Models.SQL
{
    public class MESContext : DbContext
    {
        public MESContext(DbContextOptions<MESContext> options) : base(options)
        {

        }
    }
}
