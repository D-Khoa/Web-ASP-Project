using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.MVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class PLCManagementController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public PLCManagementController(IConfiguration configuration)
        {
            _config = configuration;
            baseURI = GlobalVariable.BaseURI;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
