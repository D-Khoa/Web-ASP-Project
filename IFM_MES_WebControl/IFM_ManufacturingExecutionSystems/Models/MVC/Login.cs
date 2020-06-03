using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Login
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your user name!")]
        public string username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password!")]
        public string password { get; set; }
    }
}
