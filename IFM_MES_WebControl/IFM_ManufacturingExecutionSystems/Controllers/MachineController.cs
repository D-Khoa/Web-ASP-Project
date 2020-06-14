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
    public class MachineController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;
        public MachineController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL:LocalURL"];
        }

        // GET: MachineController
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            GetMachines(out List<Machine> machines, baseURI, token);
            StatusController statusController = new StatusController(_config);
            statusController.GetStatus(out List<Status> statuses, baseURI, token);
            ProcessController processController = new ProcessController(_config);
            processController.GetProcesses(out List<Process> processes, baseURI, token);
            dynamic myMachines = new ExpandoObject();
            myMachines.machines = machines;
            myMachines.statuses = statuses;
            myMachines.processes = processes;
            return View(myMachines);
        }

        public void GetMachines(out List<Machine> machines, string baseURI, string token)
        {
            IEnumerable<aa0003> aa0003s = Enumerable.Empty<aa0003>();
            machines = new List<Machine>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0003/Machines");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("machines");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0003>>();
                    readTask.Wait();
                    aa0003s = readTask.Result;
                    foreach (aa0003 aa0003 in aa0003s)
                    {
                        machines.Add(new Machine
                        {
                            machineID = aa0003.aa0003c01,
                            machineCode = aa0003.aa0003c11,
                            machineName = aa0003.aa0003c12,
                            supplier = aa0003.aa0003c13,
                            width = aa0003.aa0003c14,
                            length = aa0003.aa0003c15,
                            hight = aa0003.aa0003c16,
                            acquisitionCost = double.Parse(aa0003.aa0003c17),
                            acquisitionDate = DateTime.Parse(aa0003.aa0003c18),
                            life = double.Parse(aa0003.aa0003c19),
                            depreciationStart = DateTime.Parse(aa0003.aa0003c20),
                            depreciationEnd = DateTime.Parse(aa0003.aa0003c21),
                            depreciationMonthly = double.Parse(aa0003.aa0003c22),
                            accumCost = double.Parse(aa0003.aa0003c23),
                            processCode = aa0003.aa0003c31,
                            statusCode = aa0003.aa0003c32,
                            workTime = double.Parse(aa0003.aa0003c33),
                            maintainTime = double.Parse(aa0003.aa0003c34),
                            stopTime = double.Parse(aa0003.aa0003c35),
                            updateUser = aa0003.aa0003c07,
                            updateTime = DateTime.TryParse(aa0003.aa0003c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0003.aa0003c05,
                            createTime = DateTime.TryParse(aa0003.aa0003c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
        }

        // GET: MachineController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MachineController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MachineController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Machine inMachine)
        {
            try
            {
                aa0003 aa0003 = new aa0003
                {
                    aa0003c01 = inMachine.machineID,
                    aa0003c05 = inMachine.creator,
                    aa0003c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0003c11 = inMachine.machineCode,
                    aa0003c12 = inMachine.machineName,
                    aa0003c13 = inMachine.supplier,
                    aa0003c14 = inMachine.width,
                    aa0003c15 = inMachine.length,
                    aa0003c16 = inMachine.hight,
                    aa0003c17 = inMachine.acquisitionCost.ToString("0.##"),
                    aa0003c18 = inMachine.acquisitionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0003c19 = inMachine.life.ToString("0.##"),
                    aa0003c20 = inMachine.depreciationStart.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0003c21 = inMachine.depreciationEnd.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0003c22 = inMachine.depreciationMonthly.ToString("0.##"),
                    aa0003c23 = inMachine.accumCost.ToString("0.##"),
                    aa0003c31 = inMachine.processCode,
                    aa0003c32 = inMachine.statusCode,
                    aa0003c33 = inMachine.workTime.ToString("0.##"),
                    aa0003c34 = inMachine.maintainTime.ToString("0.##"),
                    aa0003c35 = inMachine.stopTime.ToString("0.##"),
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0003/Machines")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0003>("machines", aa0003);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new machine successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new machine fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new machine fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: MachineController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MachineController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Machine inMachine)
        {
            try
            {
                aa0003 aa0003 = new aa0003
                {
                    aa0003c01 = inMachine.machineID,
                    aa0003c07 = inMachine.updateUser,
                    aa0003c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0003c11 = inMachine.machineCode,
                    aa0003c12 = inMachine.machineName,
                    aa0003c13 = inMachine.supplier,
                    aa0003c14 = inMachine.width,
                    aa0003c15 = inMachine.length,
                    aa0003c16 = inMachine.hight,
                    aa0003c17 = inMachine.acquisitionCost.ToString("0.##"),
                    aa0003c18 = inMachine.acquisitionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0003c19 = inMachine.life.ToString("0.##"),
                    aa0003c20 = inMachine.depreciationStart.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0003c21 = inMachine.depreciationEnd.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0003c22 = inMachine.depreciationMonthly.ToString("0.##"),
                    aa0003c23 = inMachine.accumCost.ToString("0.##"),
                    aa0003c31 = inMachine.processCode,
                    aa0003c32 = inMachine.statusCode,
                    aa0003c33 = inMachine.workTime.ToString("0.##"),
                    aa0003c34 = inMachine.maintainTime.ToString("0.##"),
                    aa0003c35 = inMachine.stopTime.ToString("0.##"),
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0003/Machines")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0003>("machines", aa0003);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update machine successful!";
                }
                else
                {
                    ViewData["Message"] = "Update machine fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update machine fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: MachineController/Delete/5
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
                    BaseAddress = new Uri(baseURI + @"/aa0003/Machines")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("machines/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update machine successful!";
                }
                else
                {
                    ViewData["Message"] = "Update machine fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete machine fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
