using System;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Status
    {
        [Key]
        public int statusID { get; set; }
        public string statusCode { get; set; }
        public string statusName { get; set; }
        public string color { get; set; }
        public string style { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
