using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MES_IFM_MVC.Models;
using MES_IFM_MVC.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MES_IFM_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly MESContext _db;

        public AccountController(MESContext db)
        {
            _db = db;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(aa0001 user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (string.IsNullOrEmpty(user.aa0001c13))
            {
                ViewBag.Message = "Please enter your email for register!";
                return View();
            }
            if (string.IsNullOrEmpty(user.aa0001c21) || user.aa0001c21.Length < 6)
            {
                ViewBag.Message = "Password must contain at least 6 characters!";
                return View();
            }
            if (string.IsNullOrEmpty(user.aa0001c22) || user.aa0001c22 != user.aa0001c21)
            {
                ViewBag.Message = "Password and confirm password are not match!";
                return View();
            }
            string baseUrl = "https://localhost:44304/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            string stringData = JsonConvert.SerializeObject(user);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/account", contentData).Result;
            string stringResponse = response.Content.ReadAsStringAsync().Result;
            string result = JsonConvert.DeserializeObject<string>(stringResponse);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                ViewBag.Message = result;
                return View(user);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(aa0001 user)
        {
            if (string.IsNullOrEmpty(user.aa0001c13))
            {
                ViewBag.Message = "Please enter your email for login!";
                return View();
            }
            if (string.IsNullOrEmpty(user.aa0001c21))
            {
                ViewBag.Message = "Please enter your password for login!";
                return View();
            }
            string baseUrl = "https://localhost:44304/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            string stringData = JsonConvert.SerializeObject(user);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/security", contentData).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JWT jwt = JsonConvert.DeserializeObject<JWT>(result);
                HttpContext.Session.SetString("token", jwt.Token);
            }
            else
            {
                string message = JsonConvert.DeserializeObject<string>(result);
                ViewBag.Message = message;
                return View(user);
            }
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult ActiveUser(string userMail)
        {
            aa0001 checkUser = _db.aa0001
                .Where(a => a.aa0001c13 == userMail)
                .Select(a => a).FirstOrDefault();
            if (checkUser == null)
            {
                return View("Invalid email");
            }
            checkUser.aa0001c15 = "1";
            _db.Update(checkUser);
            _db.SaveChangesAsync();
            ViewBag.Message = string.Format("Email {0} is actived!", userMail);
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            ViewBag.Message = "Your logged out successfully!";
            return Redirect("/Home/Index");
        }
    }
}
