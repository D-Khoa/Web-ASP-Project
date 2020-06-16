using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class ControlController : Controller
    {
        // GET: ControlController
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ControlController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ControlController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
