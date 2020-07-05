using IFM_ManufacturingExecutionSystems.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //ViewData["firstname"] = HttpContext.Session.GetString("firstname");
            //ViewData["username"] = HttpContext.Session.GetString("username");
            GlobalVariable.UserName = HttpContext.Session.GetString("username");
            GlobalVariable.FirstName = HttpContext.Session.GetString("firstname");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
