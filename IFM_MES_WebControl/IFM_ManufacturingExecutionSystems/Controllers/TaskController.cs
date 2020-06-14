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
    public class TaskController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public TaskController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL:LocalURL"];
        }

        // GET: TaskController
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            GetTask(out List<Task> Tasks, baseURI, token);
            StatusController statusController = new StatusController(_config);
            statusController.GetStatus(out List<Status> statuses, baseURI, token);
            MachineController machineController = new MachineController(_config);
            machineController.GetMachines(out List<Machine> machines, baseURI, token);
            ProcessController processController = new ProcessController(_config);
            processController.GetProcesses(out List<Process> processes, baseURI, token);
            WorkShiftController workShiftController = new WorkShiftController(_config);
            workShiftController.GetShifts(out List<WorkShift> shifts, baseURI, token);
            dynamic myTasks = new ExpandoObject();
            myTasks.tasks = Tasks;
            myTasks.shifts = shifts;
            myTasks.statuses = statuses;
            myTasks.machines = machines;
            myTasks.processes = processes;
            return View(myTasks);
        }

        public void GetTask(out List<Task> Tasks, string baseURI, string token)
        {
            IEnumerable<aa0004> aa0004s = Enumerable.Empty<aa0004>();
            Tasks = new List<Task>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0004/Tasks");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("tasks");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0004>>();
                    readTask.Wait();
                    aa0004s = readTask.Result;
                    foreach (aa0004 aa0004 in aa0004s)
                    {
                        Tasks.Add(new Task
                        {
                            taskID = aa0004.aa0004c01,
                            taskCode = aa0004.aa0004c11,
                            taskName = aa0004.aa0004c12,
                            machineCode = aa0004.aa0004c13,
                            processCode = aa0004.aa0004c14,
                            shiftCode = aa0004.aa0004c15,
                            planStatus = aa0004.aa0004c16,
                            planStart = DateTime.Parse(aa0004.aa0004c17),
                            planStop = DateTime.Parse(aa0004.aa0004c18),
                            planQuantity = double.Parse(aa0004.aa0004c19),
                            actualStatus = aa0004.aa0004c20,
                            actualStart = DateTime.Parse(aa0004.aa0004c21),
                            actualStop = DateTime.Parse(aa0004.aa0004c22),
                            actualQuantity = double.Parse(aa0004.aa0004c23),
                            updateUser = aa0004.aa0004c07,
                            updateTime = DateTime.TryParse(aa0004.aa0004c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0004.aa0004c05,
                            createTime = DateTime.TryParse(aa0004.aa0004c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
        }

        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task inTask)
        {
            try
            {
                aa0004 aa0004 = new aa0004
                {
                    aa0004c05 = inTask.updateUser,
                    aa0004c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c11 = inTask.taskCode,
                    aa0004c12 = inTask.taskName,
                    aa0004c13 = inTask.machineCode,
                    aa0004c14 = inTask.processCode,
                    aa0004c15 = inTask.shiftCode,
                    aa0004c16 = inTask.planStatus,
                    aa0004c17 = inTask.planStart.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c18 = inTask.planStop.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c19 = inTask.planQuantity.ToString("0.##"),
                    aa0004c20 = inTask.actualStatus,
                    aa0004c21 = inTask.actualStart.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c22 = inTask.actualStop.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c23 = inTask.actualQuantity.ToString("0.##"),
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0004/Tasks")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0004>("tasks", aa0004);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new task successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new task fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new task fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: TaskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task inTask)
        {
            try
            {
                aa0004 aa0004 = new aa0004
                {
                    aa0004c01 = inTask.taskID,
                    aa0004c07 = inTask.updateUser,
                    aa0004c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c11 = inTask.taskCode,
                    aa0004c12 = inTask.taskName,
                    aa0004c13 = inTask.machineCode,
                    aa0004c14 = inTask.processCode,
                    aa0004c15 = inTask.shiftCode,
                    aa0004c16 = inTask.planStatus,
                    aa0004c17 = inTask.planStart.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c18 = inTask.planStop.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c19 = inTask.planQuantity.ToString("0.##"),
                    aa0004c20 = inTask.actualStatus,
                    aa0004c21 = inTask.actualStart.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c22 = inTask.actualStop.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0004c23 = inTask.actualQuantity.ToString("0.##"),
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0004/Tasks")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0004>("tasks", aa0004);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update task successful!";
                }
                else
                {
                    ViewData["Message"] = "Update task fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update task fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: TaskController/Delete/5
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
                    BaseAddress = new Uri(baseURI + @"/aa0004/Tasks")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("tasks/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update task successful!";
                }
                else
                {
                    ViewData["Message"] = "Update task fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete task fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
