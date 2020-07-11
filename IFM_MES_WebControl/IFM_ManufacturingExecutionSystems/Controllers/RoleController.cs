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
    public class RoleController : Controller
    {
        private readonly string baseURI;
        private readonly IConfiguration _config;
        public RoleController(IConfiguration configuration)
        {
            _config = configuration;
            //baseURI = _config["BaseURL:DefaultURL"];
            baseURI = _config["BaseURL"];
        }

        // GET: RoleController
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(GlobalVariable.Token))
            {
                return RedirectToAction("Login", "Account");
            }
            string token = HttpContext.Session.GetString("token");
            ControlController controlController = new ControlController(_config);
            controlController.GetControl(out List<Control> controls, baseURI, token);
            GetRole(out List<Role> roles, baseURI, token);
            GetRoleGroup(out List<RoleGroup> roleGroups, roles, baseURI, token);
            dynamic myRoles = new ExpandoObject();
            myRoles.controls = controls;
            myRoles.roles = roles;
            myRoles.roleGroups = roleGroups;
            return View(myRoles);
        }

        public JsonResult GetRole(out List<Role> roles, string baseURI, string token)
        {
            IEnumerable<aa0001> aa0001s = Enumerable.Empty<aa0001>();
            roles = new List<Role>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(baseURI + @"/aa0001/Roles");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        token);
                var respone = client.GetAsync("roles");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<aa0001>>();
                    readTask.Wait();
                    aa0001s = readTask.Result;
                    foreach (aa0001 aa0001 in aa0001s)
                    {
                        roles.Add(new Role
                        {
                            roleID = aa0001.aa0001c01,
                            roleCode = aa0001.aa0001c41,
                            roleName = aa0001.aa0001c42,
                            controlCode = aa0001.aa0001c43,
                            updateUser = aa0001.aa0001c07,
                            updateTime = DateTime.TryParse(aa0001.aa0001c08, out DateTime updatetime) ? updatetime : updatetime,
                            creator = aa0001.aa0001c50,
                            createTime = DateTime.TryParse(aa0001.aa0001c06, out DateTime createtime) ? createtime : createtime
                        });
                    }
                }
            }
            return this.Json(roles);
        }

        public JsonResult GetRoleGroup(out List<RoleGroup> roleGroups, List<Role> roles, string baseURI, string token)
        {
            IEnumerable<aa0001> aa0001s = Enumerable.Empty<aa0001>();
            roleGroups = new List<RoleGroup>();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using HttpClient client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(baseURI + @"/aa0001/RoleGroups")
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    token);
            var respone = client.GetAsync("rolegroups");
            respone.Wait();
            var result = respone.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<aa0001>>();
                readTask.Wait();
                aa0001s = readTask.Result;
                if (roles == null)
                {
                    GetRole(out roles, baseURI, token);
                }
                foreach (aa0001 aa0001 in aa0001s)
                {
                    roleGroups.Add(new RoleGroup
                    {
                        roleGroupID = aa0001.aa0001c01,
                        roleGroupCode = aa0001.aa0001c31,
                        roleGroupName = aa0001.aa0001c32,
                        //roles = roles.Where(x => aa0001.aa0001c33.Contains(x.roleCode)).ToList(),
                        roles = aa0001.aa0001c33,
                        updateUser = aa0001.aa0001c07,
                        updateTime = DateTime.TryParse(aa0001.aa0001c08, out DateTime updatetime) ? updatetime : updatetime,
                        creator = aa0001.aa0001c50,
                        createTime = DateTime.TryParse(aa0001.aa0001c06, out DateTime createtime) ? createtime : createtime
                    });
                }
            }
            return this.Json(roleGroups);
        }

        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleController/Create
        public ActionResult CreateRole()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(Role inRole)
        {
            try
            {
                aa0001 aa0001 = new aa0001
                {
                    aa0001c05 = inRole.creator,
                    aa0001c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c41 = inRole.roleCode,
                    aa0001c42 = inRole.roleName,
                    aa0001c43 = inRole.controlCode,
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/Roles")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0001>("roles", aa0001);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new role successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new role fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new role fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult CreateRoleGroup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRoleGroup(RoleGroup inRoleGroup)
        {
            try
            {
                aa0001 aa0001 = new aa0001
                {
                    aa0001c05 = inRoleGroup.creator,
                    aa0001c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c31 = inRoleGroup.roleGroupCode,
                    aa0001c32 = inRoleGroup.roleGroupName,
                    //aa0001c33 = string.Join(',', inRoleGroup.roles.Select(x => x.roleCode).ToArray()),
                    aa0001c33 = inRoleGroup.roles
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/RoleGroups")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PostAsJsonAsync<aa0001>("rolegroups", aa0001);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add new role group successful!";
                }
                else
                {
                    ViewData["Message"] = "Add new role group fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Add new role group fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: RoleController/Edit/5
        public ActionResult EditRole(int id)
        {
            return View();
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(Role inRole)
        {
            try
            {
                aa0001 aa0001 = new aa0001
                {
                    aa0001c01 = inRole.roleID,
                    aa0001c07 = inRole.updateUser,
                    aa0001c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c41 = inRole.controlCode,
                    aa0001c42 = inRole.roleName,
                    aa0001c43 = inRole.controlCode
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/Roles")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0001>("roles", aa0001);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update role successful!";
                }
                else
                {
                    ViewData["Message"] = "Update role fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update role fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult EditRoleGroup(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoleGroup(RoleGroup inRoleGroup)
        {
            try
            {
                aa0001 aa0001 = new aa0001
                {
                    aa0001c01 = inRoleGroup.roleGroupID,
                    aa0001c07 = inRoleGroup.updateUser,
                    aa0001c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c31 = inRoleGroup.roleGroupCode,
                    aa0001c32 = inRoleGroup.roleGroupName,
                    //aa0001c43 = string.Join(',', inRoleGroup.roles.Select(x => x.roleCode).ToArray()),
                    aa0001c33 = inRoleGroup.roles
                };
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/RoleGroups")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("token"));
                var respone = client.PutAsJsonAsync<aa0001>("rolegroups", aa0001);
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Update role successful!";
                }
                else
                {
                    ViewData["Message"] = "Update role fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Update role fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRole(int id)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/Roles")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("roles/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Delete role successful!";
                }
                else
                {
                    ViewData["Message"] = "Delete role fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete role fail!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleGroup(int id)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                using HttpClient client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri(baseURI + @"/aa0001/RoleGroups")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));
                var respone = client.DeleteAsync(string.Format("rolegroups/{0}", id));
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Delete role group successful!";
                }
                else
                {
                    ViewData["Message"] = "Delete role group fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Delete role group fail!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
