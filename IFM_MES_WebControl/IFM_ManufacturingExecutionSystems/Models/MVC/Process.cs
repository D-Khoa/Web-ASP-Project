using System;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Process
    {
        [Key]
        public int processID { get; set; }
        public string processCode { get; set; }
        public string processName { get; set; }
        public string lineCode { get; set; }
        public string deptCode { get; set; }
        public string siteCode { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
