using System.Web.Mvc;
using WepAPI.Models.User;

namespace WepAPI.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index(string role, string username)
        {
            if (!string.IsNullOrEmpty(role)) ViewBag.roleid = role;
            if (!string.IsNullOrEmpty(username)) ViewBag.message = "Wellcome " + username;
            return View();
        }
    }
}