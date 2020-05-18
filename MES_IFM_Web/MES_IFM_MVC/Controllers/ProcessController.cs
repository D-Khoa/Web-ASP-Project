using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MES_IFM_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MES_IFM_MVC.Controllers
{
    public class ProcessController : Controller
    {
        private readonly MESContext _db;

        public ProcessController(MESContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.aa0003.ToList());
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
