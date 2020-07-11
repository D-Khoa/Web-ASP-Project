using IFM_ManufacturingExecutionSystems.Models.Security;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace IFM_ManufacturingExecutionSystems.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly MESContext _context;
        private readonly string baseURI;

        public DatabaseController(MESContext context, IConfiguration configuration)
        {
            _context = context;
            baseURI = configuration["BaseURL"];
        }

        // GET: DatabaseController
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(GlobalVariable.Token))
            {
                return RedirectToAction("Login", "Account");
            }
            List<TableInformation> tables = _context.AllInformationSchemaColumns.ToList();
            return View(tables);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string token)
        {
            string salt = "mesdatabase";
            string password = EncryptData.StringToHash(baseURI, salt, MD5.Create());
            if (token == password)
            {
                _context.Database.EnsureCreated();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string token)
        {
            string salt = "mesdatabase";
            string password = EncryptData.StringToHash(baseURI, salt, MD5.Create());
            if (token == password)
            {
                _context.Database.EnsureDeleted();
            }
            return RedirectToAction("Index");
        }
    }
}
