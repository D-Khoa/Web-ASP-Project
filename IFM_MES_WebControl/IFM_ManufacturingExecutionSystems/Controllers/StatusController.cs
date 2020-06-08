using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.MVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class StatusController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public StatusController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL:LocalURL"];
        }

        // GET: Status
        public ActionResult Index()
        {
            IEnumerable<aa0002> aa0002s = Enumerable.Empty<aa0002>();
            List<Status> statuses = new List<Status>();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0002/Status");
                var respone = client.GetAsync("Status");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0002>>();
                    readTask.Wait();
                    aa0002s = readTask.Result;
                    foreach (aa0002 aa0002 in aa0002s)
                    {
                        statuses.Add(new Status
                        {
                            statusID = aa0002.aa0002c01,
                            statusCode = aa0002.aa0002c21,
                            statusName = aa0002.aa0002c22,
                            color = aa0002.aa0002c23,
                            style = aa0002.aa0002c24,
                            updateUser = aa0002.aa0002c07,
                            updateTime = DateTime.TryParse(aa0002.aa0002c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0002.aa0002c05,
                            createTime = DateTime.TryParse(aa0002.aa0002c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
            return View(statuses);
        }

        // GET: Status/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Status inStatus)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c05 = inStatus.updateUser,
                    aa0002c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c21 = inStatus.statusCode,
                    aa0002c22 = inStatus.statusName,
                    aa0002c23 = inStatus.color,
                    aa0002c24 = inStatus.style
                };
                string jsonStatus = JsonConvert.SerializeObject(aa0002);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(baseURI + @"/aa0002/Status");
                    var respone = client.PostAsJsonAsync<aa0002>("status", aa0002);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewData["Message"] = "Add new status successful!";
                    }
                    else
                    {
                        ViewData["Message"] = "Add new status fail!";
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ViewData["Message"] = "Add new status fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Status/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Status/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Status inStatus)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c01 = inStatus.statusID,
                    aa0002c07 = inStatus.updateUser,
                    aa0002c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c21 = inStatus.statusCode,
                    aa0002c22 = inStatus.statusName,
                    aa0002c23 = inStatus.color,
                    aa0002c24 = inStatus.style
                };
                string jsonStatus = JsonConvert.SerializeObject(aa0002);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(baseURI + @"/aa0002/Status");
                    var respone = client.PutAsJsonAsync<aa0002>("status", aa0002);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewData["Message"] = "Update status successful!";
                    }
                    else
                    {
                        ViewData["Message"] = "Update status fail!";
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ViewData["Message"] = "Update status fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Status/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(baseURI + @"/aa0002/Status");
                    var respone = client.DeleteAsync(string.Format("status/{0}", id));
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewData["Message"] = "Update status successful!";
                    }
                    else
                    {
                        ViewData["Message"] = "Update status fail!";
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ViewData["Message"] = "Delete status fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}