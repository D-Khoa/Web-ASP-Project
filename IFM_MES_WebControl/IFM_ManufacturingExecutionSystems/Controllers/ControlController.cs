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
    public class ControlController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public ControlController(IConfiguration configuration)
        {
            _config = configuration;
            baseURI = GlobalVariable.BaseURI;
            //baseURI = _config["BaseURL"];
        }

        // GET: ControlController
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(GlobalVariable.Token))
            {
                return RedirectToAction("Login", "Account");
            }
            string token = HttpContext.Session.GetString("token");
            GetControl(out List<Control> controls, baseURI, token);
            dynamic myControls = new ExpandoObject();
            myControls.controls = controls;
            return View(myControls);
        }

        public void GetControl(out List<Control> controls, string baseURI, string token)
        {
            IEnumerable<aa0001> aa0001s = Enumerable.Empty<aa0001>();
            controls = new List<Control>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0001/Controls");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("controls");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0001>>();
                    readTask.Wait();
                    aa0001s = readTask.Result;
                    foreach (aa0001 aa0001 in aa0001s)
                    {
                        controls.Add(new Control
                        {
                            controlID = aa0001.aa0001c01,
                            controlCode = aa0001.aa0001c51,
                            controlName = aa0001.aa0001c52,
                            updateUser = aa0001.aa0001c07,
                            updateTime = DateTime.TryParse(aa0001.aa0001c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0001.aa0001c50,
                            createTime = DateTime.TryParse(aa0001.aa0001c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
        }

        // GET: ControlController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ControlController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ControlController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Control inControl)
        {
            try
            {
                aa0001 aa0001 = new aa0001
                {
                    aa0001c05 = inControl.creator,
                    aa0001c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c51 = inControl.controlCode,
                    aa0001c52 = inControl.controlName,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/Controls")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0001>("controls", aa0001);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new control successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new control fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new control fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ControlController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ControlController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Control inControl)
        {
            try
            {
                aa0001 aa0001 = new aa0001
                {
                    aa0001c01 = inControl.controlID,
                    aa0001c07 = inControl.updateUser,
                    aa0001c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c51 = inControl.controlCode,
                    aa0001c52 = inControl.controlName,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/Controls")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0001>("controls", aa0001);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update control successful!";
                }
                else
                {
                    ViewData["Message"] = "Update control fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update control fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ControlController/Delete/5
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
                    BaseAddress = new Uri(baseURI + @"/aa0001/Controls")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("controls/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Delete control successful!";
                }
                else
                {
                    ViewData["Message"] = "Delete control fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete control fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
