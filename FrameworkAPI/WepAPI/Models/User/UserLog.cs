using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WepAPI.Models.User
{
    /// <summary>
    /// User login/logout
    /// </summary>
    public class UserLog
    {
        /// <summary>
        /// User log id
        /// </summary>
        public int userlog_id { get; set; }
        /// <summary>
        /// User code
        /// </summary>
        [Required(ErrorMessage="Please enter user code.")]
        public string user_cd { get; set; }
        /// <summary>
        /// User password MD5
        /// </summary>
        [Required(ErrorMessage="Please enter password.")]
        public string user_password { get; set; }
        [Compare("user_password")]
        public string confirm_password { get; set; }
        /// <summary>
        /// User is active
        /// </summary>
        public bool user_active { get; set; }
        /// <summary>
        /// User is online
        /// </summary>
        public bool user_online { get; set; }
        /// <summary>
        /// Last online ip address
        /// </summary>
        public string last_ipadd { get; set; }
        /// <summary>
        /// Last online date time
        /// </summary>
        public DateTime last_login { get; set; }
        /// <summary>
        /// Registrator datetime
        /// </summary>
        public DateTime reg_date { get; set; }
        public string userRoles { get; set; }
    }
}