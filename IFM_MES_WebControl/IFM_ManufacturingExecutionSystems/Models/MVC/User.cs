using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class User
    {
        [Key]
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string isactive { get; set; }
        public string rolegroups { get; set; }
        public string lastlogin { get; set; }
    }
}
