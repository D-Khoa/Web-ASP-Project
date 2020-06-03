using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.Mail;
using IFM_ManufacturingExecutionSystems.Models.Security;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class AccountController : Controller
    {
        private string baseUrl;
        private readonly MESContext _context;
        private readonly IConfiguration _config;
        public AccountController(IConfiguration configuration, MESContext context)
        {
            _context = context;
            _config = configuration;
            baseUrl = _config["BaseURL:DefaultURL"];
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(aa0001 aa0001)
        {
            #region CHECK FIELDS
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (string.IsNullOrEmpty(aa0001.aa0001c11))
            {
                ViewData["Error"] = "Please enter your username for register!";
                return View();
            }
            if (string.IsNullOrEmpty(aa0001.aa0001c12))
            {
                ViewData["Error"] = "Please enter your first name for register!";
                return View();
            }
            if (string.IsNullOrEmpty(aa0001.aa0001c14))
            {
                ViewData["Error"] = "Please enter your email for register!";
                return View();
            }
            if (string.IsNullOrEmpty(aa0001.aa0001c23) || aa0001.aa0001c23.Length < 6)
            {
                ViewData["Error"] = "Password must contain at least 6 characters!";
                return View();
            }
            if (string.IsNullOrEmpty(aa0001.aa0001c24) || aa0001.aa0001c24 != aa0001.aa0001c23)
            {
                ViewData["Error"] = "Password and confirm password are not match!";
                return View();
            }
            #endregion

            //string salt = EncryptData.RandomSalt(12);
            //aa0001.aa0001c05 = aa0001.aa0001c11;
            //aa0001.aa0001c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //aa0001.aa0001c16 = "False";
            //aa0001.aa0001c17 = "None";
            //aa0001.aa0001c23 = EncryptData.StringToHash(aa0001.aa0001c23, salt, MD5.Create());
            //_context.aa0001.Add(aa0001);
            //_context.SaveChanges();

            HttpClientHandler clienthandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            HttpClient client = new HttpClient(clienthandler)
            {
                BaseAddress = new Uri(baseUrl)
            };
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            string stringData = JsonConvert.SerializeObject(aa0001);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/aa0001/account", contentData).Result;
            string stringResponse = response.Content.ReadAsStringAsync().Result;
            string result = JsonConvert.DeserializeObject<string>(stringResponse);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MailInfo mail = new MailInfo
                {
                    mailTo = aa0001.aa0001c14,
                    mailSubject = "Active your account in IFM MES",
                    mailMessage = string.Format(@"Click the link below for active your account: {0}/account/activeuser/?email={1}", baseUrl, aa0001.aa0001c14)
                };
                SendMail.SendMailAuto(mail);
                ViewData["Message"] = "Your account is registed! Please check mail for active it!";
                return View();
            }
            else
            {
                ViewData["Error"] = result;
                return View(aa0001);
            }
        }

        public IActionResult ActiveUser([FromQuery] string email)
        {
            aa0001 checkUser = _context.aa0001
                .Where(a => a.aa0001c14 == email)
                .Select(a => a).FirstOrDefault();
            if (checkUser == null)
            {
                ViewData["Message"] = string.Format("Email {0} invaild!", email);
                return View();
            }
            checkUser.aa0001c16 = "True";
            _context.Update(checkUser);
            _context.SaveChanges();
            ViewData["Message"] = string.Format("Email {0} is actived!", email);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(aa0001 aa0001, bool rememberMe)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (string.IsNullOrEmpty(aa0001.aa0001c11))
            {
                ViewData["Error"] = "Please enter your username for login!";
                return View();
            }
            if (string.IsNullOrEmpty(aa0001.aa0001c23) || aa0001.aa0001c23.Length < 6)
            {
                ViewData["Error"] = "Password must contain at least 6 characters!";
                return View();
            }
            HttpClientHandler clienthandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };
            HttpClient client = new HttpClient(clienthandler)
            {
                BaseAddress = new Uri(baseUrl)
            };
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            string stringData = JsonConvert.SerializeObject(aa0001);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/aa0001/security", contentData).Result;
            string stringResponse = response.Content.ReadAsStringAsync().Result;
            JWT jwtResult = JsonConvert.DeserializeObject<JWT>(stringResponse);
            HttpContext.Session.SetString("token", jwtResult.Token);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, aa0001.aa0001c11));
                if (aa0001.aa0001c17.Contains(","))
                {
                    string[] roles = aa0001.aa0001c17.Split(',');
                    foreach (string role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }
                else claims.Add(new Claim(ClaimTypes.Role, aa0001.aa0001c17));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                props.IsPersistent = rememberMe;
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                ViewData["Message"] = string.Format("Wellcome {0} log in!", aa0001.aa0001c11);
                return View();
            }
            else
            {
                ViewData["Error"] = jwtResult;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}