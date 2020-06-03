using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class WorkShiftController : Controller
    {
        // GET: WorkShift
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkShift/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkShift/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}