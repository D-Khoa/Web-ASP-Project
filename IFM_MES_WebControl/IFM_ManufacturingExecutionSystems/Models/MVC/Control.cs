using System;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Control
    {
        [Key]
        public int controlID { get; set; }
        public string controlCode { get; set; }
        public string controlName { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
