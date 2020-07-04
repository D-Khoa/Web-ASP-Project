using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class RoleGroup
    {
        [Key]
        public int roleGroupID { get; set; }
        public string roleGroupCode { get; set; }
        public string roleGroupName { get; set; }
        public List<Role> roles { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
