using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WepAPI.Models.JWT
{
    public class Payload
    {
        public string iss { get; set; }
        public string user { get; set; }
        public string roles { get; set; }
    }
}