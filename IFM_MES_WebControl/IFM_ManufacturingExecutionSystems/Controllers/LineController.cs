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
    public class LineController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public LineController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL:LocalURL"];
        }

        // GET: LineController
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            GetLine(out List<Line> lines, baseURI, token);
            return View(lines);
        }

        public void GetLine(out List<Line> lines, string baseURI, string token)
        {
            IEnumerable<aa0002> aa0002s = Enumerable.Empty<aa0002>();
            lines = new List<Line>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0002/Lines");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("lines");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0002>>();
                    readTask.Wait();
                    aa0002s = readTask.Result;
                    foreach (aa0002 aa0002 in aa0002s)
                    {
                        lines.Add(new Line
                        {
                            lineID = aa0002.aa0002c01,
                            lineCode = aa0002.aa0002c51,
                            lineName = aa0002.aa0002c52,
                            updateUser = aa0002.aa0002c07,
                            updateTime = DateTime.TryParse(aa0002.aa0002c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0002.aa0002c05,
                            createTime = DateTime.TryParse(aa0002.aa0002c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
        }

        // GET: LineController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LineController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LineController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Line inLine)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c05 = inLine.updateUser,
                    aa0002c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c51 = inLine.lineCode,
                    aa0002c52 = inLine.lineName,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/Lines")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0002>("lines", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new line successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new line fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new line fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LineController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LineController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Line inLine)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c01 = inLine.lineID,
                    aa0002c07 = inLine.updateUser,
                    aa0002c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c51 = inLine.lineCode,
                    aa0002c52 = inLine.lineName,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/Lines")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0002>("lines", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update line successful!";
                }
                else
                {
                    ViewData["Message"] = "Update line fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update line fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: LineController/Delete/5
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
                    BaseAddress = new Uri(baseURI + @"/aa0002/Lines")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("lines/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update lines successful!";
                }
                else
                {
                    ViewData["Message"] = "Update lines fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete lines fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
