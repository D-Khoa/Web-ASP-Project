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
    public class ProcessController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public ProcessController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL:LocalURL"];
        }

        // GET: ProcessController
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            GetProcesses(out List<Process> processes, baseURI, token);
            SiteController siteController = new SiteController(_config);
            siteController.GetSite(out List<Site> siteses, baseURI, token);
            LineController lineController = new LineController(_config);
            lineController.GetLine(out List<Line> lines, baseURI, token);
            DepartmentController departmentController = new DepartmentController(_config);
            departmentController.GetDepartment(out List<Department> departments, baseURI, token);
            dynamic myProcesses = new ExpandoObject();
            myProcesses.processes = processes;
            myProcesses.depts = departments;
            myProcesses.sites = siteses;
            myProcesses.lines = lines;
            return View(myProcesses);
        }

        public void GetProcesses(out List<Process> processes, string baseURI, string token)
        {
            IEnumerable<aa0002> aa0002s = Enumerable.Empty<aa0002>();
            processes = new List<Process>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0002/Processes");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("processes");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0002>>();
                    readTask.Wait();
                    aa0002s = readTask.Result;
                    foreach (aa0002 aa0002 in aa0002s)
                    {
                        processes.Add(new Process
                        {
                            processID = aa0002.aa0002c01,
                            processCode = aa0002.aa0002c31,
                            processName = aa0002.aa0002c32,
                            lineCode = aa0002.aa0002c33,
                            deptCode = aa0002.aa0002c34,
                            siteCode = aa0002.aa0002c35,
                            updateUser = aa0002.aa0002c07,
                            updateTime = DateTime.TryParse(aa0002.aa0002c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0002.aa0002c05,
                            createTime = DateTime.TryParse(aa0002.aa0002c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
        }

        // GET: ProcessController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProcessController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Process inProcess)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c05 = inProcess.updateUser,
                    aa0002c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c31 = inProcess.processCode,
                    aa0002c32 = inProcess.processName,
                    aa0002c33 = inProcess.lineCode,
                    aa0002c34 = inProcess.deptCode,
                    aa0002c35 = inProcess.siteCode,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/Processes")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0002>("processes", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new process successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new process fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new process fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProcessController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProcessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Process inProcess)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c01 = inProcess.processID,
                    aa0002c07 = inProcess.updateUser,
                    aa0002c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c31 = inProcess.siteCode,
                    aa0002c32 = inProcess.processName,
                    aa0002c33 = inProcess.lineCode,
                    aa0002c34 = inProcess.deptCode,
                    aa0002c35 = inProcess.siteCode,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/Processes")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0002>("processes", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update process successful!";
                }
                else
                {
                    ViewData["Message"] = "Update process fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update process fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ProcessController/Delete/5
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
                    BaseAddress = new Uri(baseURI + @"/aa0002/Processes")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("processes/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update process successful!";
                }
                else
                {
                    ViewData["Message"] = "Update process fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete process fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
