using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Web_MES.Model
{
    /// <summary>
    /// User profile
    /// </summary>
    public class u20000
    {
        [Key]
        public string u20001 { get; set; }  //uid
        public string u20002 { get; set; }  //pass
        public string u20003 { get; set; }  //salt
        public string u20004 { get; set; }  //token
        public string u20005 { get; set; }  //status
        public string u20006 { get; set; }  //last ip
        public string u20007 { get; set; }
        public string u20008 { get; set; }
        public string u20009 { get; set; }
        public string u20010 { get; set; }
        public int u20011 { get; set; }
        public int u20012 { get; set; }
        public int u20013 { get; set; }
        public int u20014 { get; set; }
        public int u20015 { get; set; }
        public double u20016 { get; set; }
        public double u20017 { get; set; }
        public double u20018 { get; set; }
        public double u20019 { get; set; }
        public double u20020 { get; set; }
        public bool u20021 { get; set; }
        public bool u20022 { get; set; }
        public bool u20023 { get; set; }
        public bool u20024 { get; set; }
        public bool u20025 { get; set; }
        public DateTime u20026 { get; set; }    //last login
        public DateTime u20027 { get; set; }
        public DateTime u20028 { get; set; }
        public DateTime u20029 { get; set; }
        public DateTime u20030 { get; set; }
    }
}
