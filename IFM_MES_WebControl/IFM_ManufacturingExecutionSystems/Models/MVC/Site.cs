using System;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Site
    {
        [Key]
        public int siteID { get; set; }
        public string siteCode { get; set; }
        public string siteName { get; set; }
        public string location { get; set; }
        public string country { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
