using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class MasterController : Controller
    {
        public IActionResult WorkShift()
        {
            return View();
        }

        public IActionResult AddWorkShift()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult AddWorkShift()
        //{
        //    return View();
        //}

        public IActionResult UpdateWorkShift()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult UpdateWorkShift()
        //{
        //    return View();
        //}
    }
}