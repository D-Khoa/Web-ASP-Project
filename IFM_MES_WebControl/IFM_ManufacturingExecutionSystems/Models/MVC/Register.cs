using System.ComponentModel.DataAnnotations;

namespace IFM_ManufacturingExecutionSystems.Models.MVC
{
    public class Register
    {
        [Key]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Please enter user name!")]
        public string username { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter your first name!")]
        public string firstname { get; set; }
        public string lastname { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage ="Please enter your mail!")]
        [EmailAddress]
        public string email { get; set; }
        [Phone]
        public string phone { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the password!")]
        [MinLength(6, ErrorMessage = "Password at leat 6 characters!")]
        public string password { get; set; }
        [Compare("password",ErrorMessage ="Password and confirm password are not match!")]
        public string confirmPassword { get; set; }
    }
}
