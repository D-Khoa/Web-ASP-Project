using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Control
    {
        [Key]
        public string controlCode { get; set; }
        public string controlName { get; set; }
    }
}
