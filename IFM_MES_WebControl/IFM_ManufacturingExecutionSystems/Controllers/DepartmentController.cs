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
    public class DepartmentController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;

        public DepartmentController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL"];
            baseURI = GlobalVariable.BaseURI;
        }

        // GET: DepartmentController
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(GlobalVariable.Token))
            {
                return RedirectToAction("Login", "Account");
            }
            string token = HttpContext.Session.GetString("token");
            GetDepartment(out List<Department> departments, baseURI, token);
            return View(departments);
        }

        public void GetDepartment(out List<Department> departments, string baseURI, string token)
        {
            IEnumerable<aa0002> aa0002s = Enumerable.Empty<aa0002>();
            departments = new List<Department>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0002/Departments");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("departments");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0002>>();
                    readTask.Wait();
                    aa0002s = readTask.Result;
                    foreach (aa0002 aa0002 in aa0002s)
                    {
                        departments.Add(new Department
                        {
                            deptID = aa0002.aa0002c01,
                            deptCode = aa0002.aa0002c61,
                            deptName = aa0002.aa0002c62,
                            updateUser = aa0002.aa0002c07,
                            updateTime = DateTime.TryParse(aa0002.aa0002c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0002.aa0002c05,
                            createTime = DateTime.TryParse(aa0002.aa0002c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department inDept)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c05 = inDept.updateUser,
                    aa0002c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c61 = inDept.deptCode,
                    aa0002c62 = inDept.deptName,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/Departments")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0002>("departments", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new department successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new department fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new department fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department inDept)
        {
            try
            {
                aa0002 aa0002 = new aa0002
                {
                    aa0002c01 = inDept.deptID,
                    aa0002c07 = inDept.updateUser,
                    aa0002c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0002c61 = inDept.deptCode,
                    aa0002c62 = inDept.deptName,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0002/Departments")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0002>("departments", aa0002);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update departments successful!";
                }
                else
                {
                    ViewData["Message"] = "Update departments fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update departments fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: DepartmentController/Delete/5
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
                    BaseAddress = new Uri(baseURI + @"/aa0002/Departments")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("departments/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update departments successful!";
                }
                else
                {
                    ViewData["Message"] = "Update departments fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete departments fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
