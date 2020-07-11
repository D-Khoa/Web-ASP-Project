using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.MVC;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IFM_ManufacturingExecutionSystems.Models.SQL
{
    public class MESContext : DbContext
    {
        public MESContext(DbContextOptions<MESContext> options) : base(options) { }
        public DbSet<aa0001> aa0001 { get; set; }
        public DbSet<aa0002> aa0002 { get; set; }
        public DbSet<aa0003> aa0003 { get; set; }
        public DbSet<aa0004> aa0004 { get; set; }
        public DbSet<aa0005> aa0005 { get; set; }
        public DbSet<aa0006> aa0006 { get; set; }
        public DbSet<TableInformation> TableInformations { get; set; }
        //public DbSet<User> User { get; set; }
        //public DbSet<Role> Role { get; set; }
        //public DbSet<Task> Task { get; set; }
        //public DbSet<Line> Line { get; set; }
        //public DbSet<Site> Site { get; set; }
        //public DbSet<Login> Login { get; set; }
        //public DbSet<Status> Status { get; set; }
        //public DbSet<Machine> Machine { get; set; }
        //public DbSet<Process> Process { get; set; }
        //public DbSet<Register> Register { get; set; }
        //public DbSet<WorkShift> WorkShift { get; set; }
        //public DbSet<Department> Department { get; set; }
        public IQueryable<TableInformation> AllInformationSchemaColumns =>
            TableInformations.FromSqlRaw(@"SELECT tbl_table.TABLE_CATALOG AS [DATABASE],
                                                  tbl_table.TABLE_SCHEMA AS [SCHEMA],
                                                  name AS [TABLE NAME],
                                                  create_date AS [CREATE DATE],
                                                  modify_date AS [MODIFY DATE],
                                                  max_column_id_used AS [MAX COLUMN]
                                                  FROM SYS.TABLES tbl_detail
                                                  INNER JOIN INFORMATION_SCHEMA.TABLES tbl_table
                                                  ON tbl_detail.name = tbl_table.TABLE_NAME
                                                  WHERE lock_escalation_desc = 'TABLE'");
    }
}
