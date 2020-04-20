using System;

namespace WepAPI.Models.Control
{
    public class MesControl
    {
        public int control_id { get; set; }
        public string control_cd { get; set; }
        public string control_name { get; set; }
        public string parent_cd { get; set; }
        public DateTime reg_date { get; set; }
    }
}