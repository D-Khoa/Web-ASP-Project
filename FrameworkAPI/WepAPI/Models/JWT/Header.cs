using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WepAPI.Models.JWT
{
    public class Header
    {
        public string alg { get; set; }
        public string type { get; set; }
    }
}