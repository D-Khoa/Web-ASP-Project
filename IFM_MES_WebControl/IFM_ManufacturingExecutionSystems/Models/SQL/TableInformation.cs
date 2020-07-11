using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFM_ManufacturingExecutionSystems.Models.SQL
{
    [Table("TableInfomation")]
    public class TableInformation
    {
        [Column("DATABASE")]
        public string Database { get; set; }
        [Column("SCHEMA")]
        public string Schema { get; set; }
        [Key]
        [Column("TABLE NAME")]
        public string Table { get; set; }
        [Column("CREATE DATE")]
        public DateTime CreateDate { get; set; }
        [Column("MODIFY DATE")]
        public DateTime ModifyDate { get; set; }
        [Column("MAX COLUMN")]
        public int MaxColumn { get; set; }
    }
}
