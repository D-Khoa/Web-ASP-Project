using System;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Machine
    {
        //Information
        [Key]
        public int machineID { get; set; }
        public string machineCode { get; set; }
        public string machineName { get; set; }
        public string supplier { get; set; }
        public string width { get; set; }
        public string length { get; set; }
        public string hight { get; set; }
        //Inventory
        public double life { get; set; }
        public double accumCost { get; set; }
        public double acquisitionCost { get; set; }
        public DateTime acquisitionDate { get; set; }
        public DateTime depreciationStart { get; set; }
        public DateTime depreciationEnd { get; set; }
        public double depreciationMonthly { get; set; }
        //Factory
        public string processCode { get; set; }
        public string statusCode { get; set; }
        public double workTime { get; set; }
        public double maintainTime { get; set; }
        public double stopTime { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
