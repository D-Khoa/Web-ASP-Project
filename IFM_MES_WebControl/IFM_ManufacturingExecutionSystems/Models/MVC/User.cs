using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string isActive { get; set; }
        public string roleGroups { get; set; }
        public string lastLogin { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
