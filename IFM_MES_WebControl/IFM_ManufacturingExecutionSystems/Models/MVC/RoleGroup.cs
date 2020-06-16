using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class RoleGroup
    {
        [Key]
        public string roleGroup { get; set; }
        public string roleGroupName { get; set; }
        public string roleCode { get; set; }
    }
}
