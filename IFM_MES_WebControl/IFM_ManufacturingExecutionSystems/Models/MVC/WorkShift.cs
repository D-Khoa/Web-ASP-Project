using System;
using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class WorkShift
    {
        [Key]
        public int shiftID { get; set; }
        public string shiftCode { get; set; }
        public string shiftName { get; set; }
        public DateTime beginTime { get; set; }
        public DateTime endTime { get; set; }
        public string updateUser { get; set; }
        public DateTime updateTime { get; set; }
        public string creator { get; set; }
        public DateTime createTime { get; set; }
    }
}
