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
    public class WorkShiftController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public WorkShiftController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL:LocalURL"];
        }

        // GET: WorkShift
        public ActionResult Index()
        {
            IEnumerable<aa0002> aa0002s = Enumerable.Empty<aa0002>();
            List<WorkShift> WorkShiftses = new List<WorkShift>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0002/WorkShifts");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.GetAsync("workShifts");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0002>>();
                    readTask.Wait();
                    aa0002s = readTask.Result;
                    foreach (aa0002 aa0002 in aa0002s)
                    {
                        WorkShiftses.Add(new WorkShift
                        {
                            shiftID = aa0002.aa0002c01,
                            shiftCode = aa0002.aa0002c11,
                            shiftName = aa0002.aa0002c12,
                            beginTime = DateTime.Parse(aa0002.aa0002c13),
                            endTime = DateTime.Parse(aa0002.aa0002c14),
                            updateUser = aa0002.aa0002c07,
                            updateTime = DateTime.TryParse(aa0002.aa0002c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0002.aa0002c05,
                            createTime = DateTime.TryParse(aa0002.aa0002c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
            return View(WorkShiftses);
        }

        // GET: WorkShift/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkShift/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkShift/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkShift inWorkShift)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c05 = inWorkShift.updateUser,
                    aa0002c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c11 = inWorkShift.shiftCode,
                    aa0002c12 = inWorkShift.shiftName,
                    aa0002c13 = inWorkShift.beginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c14 = inWorkShift.endTime.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/WorkShifts")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0002>("workShifts", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new work shift successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new work shift fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new work shift fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: WorkShift/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkShift/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkShift inWorkShift)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c01 = inWorkShift.shiftID,
                    aa0002c07 = inWorkShift.updateUser,
                    aa0002c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c11 = inWorkShift.shiftCode,
                    aa0002c12 = inWorkShift.shiftName,
                    aa0002c13 = inWorkShift.beginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c14 = inWorkShift.endTime.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/WorkShifts")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0002>("workShifts", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update work shift successful!";
                }
                else
                {
                    ViewData["Message"] = "Update work shift fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update work shift fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: WorkShift/Delete/5
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
                    BaseAddress = new Uri(baseURI + @"/aa0002/WorkShifts")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("workShifts/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update work shift successful!";
                }
                else
                {
                    ViewData["Message"] = "Update work shift fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete work shift fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}