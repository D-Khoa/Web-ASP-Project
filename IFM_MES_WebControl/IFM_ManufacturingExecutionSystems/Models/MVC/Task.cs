using System;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Task
    {
        //Information
        [Key]
        public int taskID { get; set; }
        public string taskCode { get; set; }
        public string taskName { get; set; }
        public string machineCode { get; set; }
        public string processCode { get; set; }
        //Plan
        public string shiftCode { get; set; }
        public string planStatus { get; set; }
        public DateTime planStart { get; set; }
        public DateTime planStop { get; set; }
        public double planQuantity { get; set; }
        //Actual
        public string actualStatus { get; set; }
        public DateTime actualStart { get; set; }
        public DateTime actualStop { get; set; }
        public double actualQuantity { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
