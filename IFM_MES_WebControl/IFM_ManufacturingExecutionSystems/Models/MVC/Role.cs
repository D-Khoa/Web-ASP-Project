using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Role
    {
        [Key]
        public string roleCode { get; set; }
        public string roleName { get; set; }
        public string controlCode { get; set; }
    }
}
