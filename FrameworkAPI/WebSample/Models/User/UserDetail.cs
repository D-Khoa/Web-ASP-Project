using System;

namespace WebSample.Models.User
{
    public class UserDetail
    {
        public string uid { get; set; }
        public string fullName { get; set; }
        public string userMail { get; set; }
        public string userTel { get; set; }
        public string userPosition { get; set; }
        public string userDepartment { get; set; }
        public string userFactory { get; set; }
        public DateTime userStartDate { get; set; }
    }
}