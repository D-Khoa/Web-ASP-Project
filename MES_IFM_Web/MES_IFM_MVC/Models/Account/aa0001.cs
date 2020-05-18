using System.ComponentModel.DataAnnotations;

namespace MES_IFM_MVC.Models.Account
{
    public class aa0001
    {
        /// <summary>
        /// primary key
        /// </summary>
        [Key]
        public int aa0001c01 { get; set; }
        /// <summary>
        /// country
        /// </summary>
        public string aa0001c02 { get; set; }
        /// <summary>
        /// language
        /// </summary>
        public string aa0001c03 { get; set; }
        /// <summary>
        /// location
        /// </summary>
        public string aa0001c04 { get; set; }
        /// <summary>
        /// factory
        /// </summary>
        public string aa0001c05 { get; set; }
        /// <summary>
        /// flag
        /// </summary>
        public string aa0001c06 { get; set; }
        /// <summary>
        /// reg user
        /// </summary>
        public string aa0001c07 { get; set; }
        /// <summary>
        /// reg date
        /// </summary>
        public string aa0001c08 { get; set; }
        /// <summary>
        /// update user
        /// </summary>
        public string aa0001c09 { get; set; }
        /// <summary>
        /// update time
        /// </summary>
        public string aa0001c10 { get; set; }
        /// <summary>
        /// firstname
        /// </summary>
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter your first name")]
        public string aa0001c11 { get; set; }   //firstname
        /// <summary>
        /// lastname
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your last name")]
        public string aa0001c12 { get; set; }   //lastname
        /// <summary>
        /// mail
        /// </summary>
        [EmailAddress(ErrorMessage = "Please enter a valid mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your mail")]
        public string aa0001c13 { get; set; }   //mail
        /// <summary>
        /// phone
        /// </summary>
        public string aa0001c14 { get; set; }   //phone
        /// <summary>
        /// is active
        /// </summary>
        public string aa0001c15 { get; set; }   //is active
        /// <summary>
        /// image
        /// </summary>
        public string aa0001c16 { get; set; }   //image
        public string aa0001c17 { get; set; }
        public string aa0001c18 { get; set; }
        public string aa0001c19 { get; set; }
        /// <summary>
        /// salt
        /// </summary>
        public string aa0001c20 { get; set; }   //salt
        /// <summary>
        /// pass
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password")]
        [StringLength(maximumLength:100, MinimumLength = 6, ErrorMessage = "Password must contain at least 6 characters")]
        public string aa0001c21 { get; set; }   //pass
        /// <summary>
        /// confirm pass / token
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm your password")]
        [Compare(otherProperty: "aa0001c21", ErrorMessage = "Password and Confirm Password does not match")]
        public string aa0001c22 { get; set; }   //confirm pass
        /// <summary>
        /// status
        /// </summary>
        public string aa0001c23 { get; set; }   //status
        /// <summary>
        /// last login
        /// </summary>
        public string aa0001c24 { get; set; }   //last login
        /// <summary>
        /// last ip
        /// </summary>
        public string aa0001c25 { get; set; }   //last ip
        /// <summary>
        /// token
        /// </summary>
        public string aa0001c26 { get; set; }   //token
        public string aa0001c27 { get; set; }
        public string aa0001c28 { get; set; }
        public string aa0001c29 { get; set; }
        /// <summary>
        /// role group
        /// </summary>
        public string aa0001c30 { get; set; }   //role group
        public string aa0001c31 { get; set; }
        public string aa0001c32 { get; set; }
        public string aa0001c33 { get; set; }
        public string aa0001c34 { get; set; }
        public string aa0001c35 { get; set; }
        public string aa0001c36 { get; set; }
        public string aa0001c37 { get; set; }
        public string aa0001c38 { get; set; }
        public string aa0001c39 { get; set; }
        public string aa0001c40 { get; set; }
        public string aa0001c41 { get; set; }
        public string aa0001c42 { get; set; }
        public string aa0001c43 { get; set; }
        public string aa0001c44 { get; set; }
        public string aa0001c45 { get; set; }
        public string aa0001c46 { get; set; }
        public string aa0001c47 { get; set; }
        public string aa0001c48 { get; set; }
        public string aa0001c49 { get; set; }
        public string aa0001c50 { get; set; }
        public string aa0001c51 { get; set; }
        public string aa0001c52 { get; set; }
        public string aa0001c53 { get; set; }
        public string aa0001c54 { get; set; }
        public string aa0001c55 { get; set; }
        public string aa0001c56 { get; set; }
        public string aa0001c57 { get; set; }
        public string aa0001c58 { get; set; }
        public string aa0001c59 { get; set; }
        public string aa0001c60 { get; set; }
        public string aa0001c61 { get; set; }
        public string aa0001c62 { get; set; }
        public string aa0001c63 { get; set; }
        public string aa0001c64 { get; set; }
        public string aa0001c65 { get; set; }
        public string aa0001c66 { get; set; }
        public string aa0001c67 { get; set; }
        public string aa0001c68 { get; set; }
        public string aa0001c69 { get; set; }
        public string aa0001c70 { get; set; }
        public string aa0001c71 { get; set; }
        public string aa0001c72 { get; set; }
        public string aa0001c73 { get; set; }
        public string aa0001c74 { get; set; }
        public string aa0001c75 { get; set; }
        public string aa0001c76 { get; set; }
        public string aa0001c77 { get; set; }
        public string aa0001c78 { get; set; }
        public string aa0001c79 { get; set; }
        public string aa0001c80 { get; set; }
        public string aa0001c81 { get; set; }
        public string aa0001c82 { get; set; }
        public string aa0001c83 { get; set; }
        public string aa0001c84 { get; set; }
        public string aa0001c85 { get; set; }
        public string aa0001c86 { get; set; }
        public string aa0001c87 { get; set; }
        public string aa0001c88 { get; set; }
        public string aa0001c89 { get; set; }
        public string aa0001c90 { get; set; }
        public string aa0001c91 { get; set; }
        public string aa0001c92 { get; set; }
        public string aa0001c93 { get; set; }
        public string aa0001c94 { get; set; }
        public string aa0001c95 { get; set; }
        public string aa0001c96 { get; set; }
        public string aa0001c97 { get; set; }
        public string aa0001c98 { get; set; }
        public string aa0001c99 { get; set; }
    }
}
