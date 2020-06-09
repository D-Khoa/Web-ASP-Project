using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.Mail;
using IFM_ManufacturingExecutionSystems.Models.MVC;
using IFM_ManufacturingExecutionSystems.Models.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string baseURI;
        public AccountController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL:LocalURL"];
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register inUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(inUser);
                }
                string salt = EncryptData.RandomSalt(12);
                string ipAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                string tokenActive = inUser.username.StringToHash(salt, MD5.Create());
                aa0001 regUserInfo = new aa0001
                {
                    aa0001c05 = inUser.username,
                    aa0001c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c11 = inUser.username,
                    aa0001c12 = inUser.firstname,
                    aa0001c13 = inUser.lastname,
                    aa0001c14 = inUser.email,
                    aa0001c15 = inUser.phone,
                    aa0001c16 = "0",
                    aa0001c17 = "None",
                    aa0001c21 = salt,
                    aa0001c22 = ipAdress,
                    aa0001c23 = EncryptData.StringToHash(inUser.password, salt, MD5.Create()),
                    aa0001c24 = tokenActive
                };
                // Register API
                //string jsonRegUserInfoString = JsonConvert.SerializeObject(regUserInfo);
                //var client = new RestClient(baseURI + "/aa0001/Accounts/")
                //{
                //    Timeout = -1
                //};
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("Content-Type", "application/json");
                //request.AddParameter("application/json", jsonRegUserInfoString, ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(baseURI + @"/aa0001/Accounts");
                    var respone = client.PostAsJsonAsync<aa0001>("accounts", regUserInfo);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        // Send mail
                        MailInfo mail = new MailInfo
                        {
                            mailTo = inUser.email,
                            mailSubject = "Active your account in IFM MES",
                            mailMessage = string.Format(@"Click the link below for active your account: {0}/account/activeuser/?token={1}", baseURI, tokenActive)
                        };
                        SendMail.SendMailAuto(mail);
                        ViewData["Message"] = "Your account is registed successful! Please check mail for active it!";
                        return RedirectToAction(nameof(Login));
                    }
                    else
                    {
                        ViewData["Message"] = result.StatusCode.ToString();
                        Console.WriteLine(result.Content);
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ActiveUser(string token)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(token);
                HttpContent httpContent = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json-patch+json");
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(baseURI + @"/aa0001/Security");
                    var respone = client.PatchAsync("security", httpContent);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewData["Message"] = "User active successful!";
                    }
                    else
                    {
                        ViewData["Message"] = "User active fail!";
                    }
                    return RedirectToAction(nameof(Login));
                }
            }
            catch
            {
                ViewData["Message"] = "Error when active user";
                return View(nameof(Login));
            }
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login inUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                aa0001 loginUser = new aa0001
                {
                    aa0001c11 = inUser.username,
                    aa0001c23 = inUser.password,
                };
                // Login API
                //string jsonLoginUserString = JsonConvert.SerializeObject(loginUser);
                //var client = new RestClient(baseURI + "/aa0001/Security/")
                //{
                //    Timeout = -1
                //};
                //var request = new RestRequest(Method.PUT);
                //request.AddHeader("Content-Type", "application/json");
                //request.AddParameter("application/json", jsonLoginUserString, ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(baseURI + @"/aa0001/Security");
                    var respone = client.PutAsJsonAsync<aa0001>("security", loginUser);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //JWT jwtToken = JsonConvert.DeserializeObject<JWT>(result.Content.ReadAsStringAsync());
                        var jwtToken = result.Content.ReadAsAsync<JWT>();
                        jwtToken.Wait();
                        HttpContext.Session.SetString("token", jwtToken.Result.Token);
                        ViewData["Message"] = "Wellcome " + inUser.username;
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ViewData["Message"] = result.StatusCode.ToString();
                        Console.WriteLine(result.Content);
                        return View();
                    }
                }
            }
            catch(Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            ViewData["Message"] = "Goodbye and see you later!";
            return RedirectToAction(nameof(Login));
        }
    }
}