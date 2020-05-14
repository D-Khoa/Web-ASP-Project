using System;
using System.ComponentModel.DataAnnotations;

namespace WebSample.Models.User
{
    public class UserLog
    {
        public string uid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        [Compare("password")]
        public string confirmPassword { get; set; }
        public string userToken { get; set; }
        public DateTime lastLogin { get; set; }
        public DateTime regDate { get; set; }
        public bool userActive { get; set; }
        public bool userOnline { get; set; }
    }
}