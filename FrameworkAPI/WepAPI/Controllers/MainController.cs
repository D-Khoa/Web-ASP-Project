using System.Web.Mvc;
using WepAPI.Models.User;

namespace WepAPI.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index(string role, string message)
        {
            if (!string.IsNullOrEmpty(role)) ViewBag.roleid = role;
            if (!string.IsNullOrEmpty(message)) ViewBag.message = message;
            return View();
        }
    }
}