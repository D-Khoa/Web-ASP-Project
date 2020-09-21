using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class PLCManagement
    {
        [Key]
        public int plcId { get; set; }
        public string plcCode { get; set; }
        public string plcAddress { get; set; }
        public string model { get; set; }
        public string line { get; set; }
        public string process { get; set; }
        public bool status { get; set; }
    }
}
