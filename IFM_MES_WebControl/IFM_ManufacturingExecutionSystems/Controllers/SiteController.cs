using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.MVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class SiteController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public SiteController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            //baseURI = _config["BaseURL"];
            baseURI = GlobalVariable.BaseURI;
        }

        // GET: Site
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(GlobalVariable.Token))
            {
                return RedirectToAction("Login", "Account");
            }
            string token = HttpContext.Session.GetString("token");
            GetSite(out List<Site> siteses, baseURI, token);
            return View(siteses);
        }

        public void GetSite(out List<Site> siteses, string baseURI, string token)
        {
            IEnumerable<aa0002> aa0002s = Enumerable.Empty<aa0002>();
            siteses = new List<Site>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0002/Sites");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("sites");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0002>>();
                    readTask.Wait();
                    aa0002s = readTask.Result;
                    foreach (aa0002 aa0002 in aa0002s)
                    {
                        siteses.Add(new Site
                        {
                            siteID = aa0002.aa0002c01,
                            siteCode = aa0002.aa0002c41,
                            siteName = aa0002.aa0002c42,
                            location = aa0002.aa0002c43,
                            country = aa0002.aa0002c44,
                            updateUser = aa0002.aa0002c07,
                            updateTime = DateTime.TryParse(aa0002.aa0002c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0002.aa0002c05,
                            createTime = DateTime.TryParse(aa0002.aa0002c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
        }

        // GET: Site/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Site/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Site/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Site inSite)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c05 = inSite.updateUser,
                    aa0002c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c41 = inSite.siteCode,
                    aa0002c42 = inSite.siteName,
                    aa0002c43 = inSite.location,
                    aa0002c44 = inSite.country,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/Sites")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0002>("sites", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new site successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new site fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new site fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Site/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Site/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Site inSite)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c01 = inSite.siteID,
                    aa0002c07 = inSite.updateUser,
                    aa0002c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c41 = inSite.siteCode,
                    aa0002c42 = inSite.siteName,
                    aa0002c43 = inSite.location,
                    aa0002c44 = inSite.country,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/Sites")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0002>("sites", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update site successful!";
                }
                else
                {
                    ViewData["Message"] = "Update site fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update site fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Site/Delete/5
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
                    BaseAddress = new Uri(baseURI + @"/aa0002/Sites")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("sites/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update site successful!";
                }
                else
                {
                    ViewData["Message"] = "Update site fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete site fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}