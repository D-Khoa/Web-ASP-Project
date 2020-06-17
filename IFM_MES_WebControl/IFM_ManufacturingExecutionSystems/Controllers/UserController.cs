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
    public class UserController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public UserController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL:LocalURL"];
        }

        // GET: UserController
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            GetUser(out List<User> users, baseURI, token);
            dynamic myUsers = new ExpandoObject();
            myUsers.users = users;
            return View(myUsers);
        }

        public void GetUser(out List<User> users, string baseURI, string token)
        {
            IEnumerable<aa0001> aa0001s = Enumerable.Empty<aa0001>();
            users = new List<User>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0001/Accounts");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("accounts");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0001>>();
                    readTask.Wait();
                    aa0001s = readTask.Result;
                    foreach (aa0001 aa0001 in aa0001s)
                    {
                        users.Add(new User
                        {
                            userID = aa0001.aa0001c01,
                            userName = aa0001.aa0001c11,
                            firstName = aa0001.aa0001c12,
                            lastName = aa0001.aa0001c13,
                            email = aa0001.aa0001c14,
                            phone = aa0001.aa0001c15,
                            isActive = aa0001.aa0001c16,
                            roleGroups = aa0001.aa0001c17,
                            lastLogin = aa0001.aa0001c25,
                            updateUser = aa0001.aa0001c07,
                            updateTime = DateTime.TryParse(aa0001.aa0001c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0001.aa0001c50,
                            createTime = DateTime.TryParse(aa0001.aa0001c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User inUser)
        {
            try
            {
                aa0001 aa0001 = new aa0001
                {
                    aa0001c05 = inUser.creator,
                    aa0001c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c11 = inUser.userName,
                    aa0001c12 = inUser.firstName,
                    aa0001c13 = inUser.lastName,
                    aa0001c14 = inUser.email,
                    aa0001c55 = inUser.phone,
                    aa0001c16 = inUser.isActive,
                    aa0001c17 = inUser.roleGroups,
                    aa0001c25 = inUser.lastLogin,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/Accounts")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0001>("accounts", aa0001);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new user successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new user fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new user fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User inUser)
        {
            try
            {
                aa0001 aa0001 = new aa0001
                {
                    aa0001c01 = inUser.userID,
                    aa0001c07 = inUser.updateUser,
                    aa0001c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c11 = inUser.userName,
                    aa0001c12 = inUser.firstName,
                    aa0001c13 = inUser.lastName,
                    aa0001c14 = inUser.email,
                    aa0001c55 = inUser.phone,
                    aa0001c16 = inUser.isActive,
                    aa0001c17 = inUser.roleGroups,
                    aa0001c25 = inUser.lastLogin,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/Accounts")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0001>("accounts", aa0001);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update user successful!";
                }
                else
                {
                    ViewData["Message"] = "Update user fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update user fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/Accounts")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("accounts/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Delete user successful!";
                }
                else
                {
                    ViewData["Message"] = "Delete user fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete user fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
