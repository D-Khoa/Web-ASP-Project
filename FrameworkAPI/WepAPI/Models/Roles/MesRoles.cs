using System;

namespace WepAPI.Models.Roles
{
    public class MesRoles
    {
        public int role_id { get; set; }
        public string role_cd { get; set; }
        public string role_name { get; set; }
        public string reg_user { get; set; }
        public DateTime reg_date { get; set; }
    }
}