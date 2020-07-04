using System;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Role
    {
        [Key]
        public int roleID { get; set; }
        public string roleCode { get; set; }
        public string roleName { get; set; }
        public string controlCode { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
