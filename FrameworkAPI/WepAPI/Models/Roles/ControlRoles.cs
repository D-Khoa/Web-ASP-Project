using System;

namespace WepAPI.Models.Roles
{
    public class ControlRoles
    {
        public int control_role_id { get; set; }
        public string control_cd { get; set; }
        public string role_cd { get; set; }
        public string reg_user { get; set; }
        public DateTime reg_date { get; set; }
    }
}