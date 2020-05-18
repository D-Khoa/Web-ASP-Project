using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MES_IFM_MVC.Models;
using MES_IFM_MVC.Models.Account;
using MES_IFM_MVC.Models.MailContact;
using MES_IFM_MVC.Models.SQL;
using Microsoft.AspNetCore.Mvc;

namespace MES_IFM_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly MESContext _db;

        public AccountController(MESContext db)
        {
            _db = db;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(aa0001 user)
        {
            if (ModelState.IsValid)
            {
                MD5 algorithm = MD5.Create();
                string salt = EncryptData.RandomSalt(12);
                aa0001 outUser = new aa0001
                {
                    aa0001c07 = user.aa0001c13,
                    aa0001c08 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    aa0001c11 = user.aa0001c11,
                    aa0001c12 = user.aa0001c12,
                    aa0001c13 = user.aa0001c13,
                    aa0001c14 = user.aa0001c14,
                    aa0001c16 = user.aa0001c16,
                    aa0001c20 = salt,
                    aa0001c21 = EncryptData.StringToHash(user.aa0001c21, salt, algorithm),
                    aa0001c15 = "0",
                    aa0001c23 = "Not active",
                    aa0001c26 = EncryptData.StringToHash(user.aa0001c13, salt, algorithm),
                    aa0001c30 = "None"
                };
                string checkmail = _db.aa0001
                    .Where(a => a.aa0001c13 == outUser.aa0001c13)
                    .Select(a => a.aa0001c13).ToString();
                if (checkmail == outUser.aa0001c13)
                    return Content(string.Format("This mail {0} is aready exist!", outUser.aa0001c13));
                _db.aa0001.Add(outUser);
                await _db.SaveChangesAsync();
                MailInfo mailInfo = new MailInfo();
                mailInfo.mailTo = outUser.aa0001c13;
                mailInfo.mailSubject = "Active Account";
                mailInfo.mailMessage = "https://localhost:44304/Account/ActiveUser/outUser.aa0001c13";
                SendMail.SendMailAuto(mailInfo);
                return Redirect("/Account/Login");
            }
            else
            {
                return View(user);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(aa0001 user)
        {
            //if (ModelState.IsValid)
            //{
                MD5 algorithm = MD5.Create();
                aa0001 checkUser = _db.aa0001
                    .Where(a => a.aa0001c13 == user.aa0001c13)
                    .Select(a => a).FirstOrDefault();
                if(checkUser.aa0001c15 == "0")
                    return Content("User is not active!");
                string pass = EncryptData.StringToHash(user.aa0001c21, checkUser.aa0001c20, algorithm);
                if (checkUser.aa0001c21 != pass)
                    return Content("User and password are not match!");
                string token = checkUser.aa0001c13 + DateTime.Now.ToString("yyyyMMddHHmmss");
                token.StringToHash(checkUser.aa0001c20, algorithm);
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                checkUser.aa0001c22 = token;
                checkUser.aa0001c23 = "Online";
                checkUser.aa0001c24 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                checkUser.aa0001c25 = remoteIpAddress.MapToIPv4().ToString();
                user = checkUser;
                await _db.SaveChangesAsync();
                return Redirect("/Home/Index");
            //}
            //else
            //{
            //    return View(user);
            //}
        }

        public IActionResult ActiveUser(string userMail)
        {
            ViewBag.Mail = userMail;
            aa0001 checkUser = _db.aa0001
                .Where(a => a.aa0001c13 == userMail)
                .Select(a => a).FirstOrDefault();
            checkUser.aa0001c15 = "1";
            _db.SaveChangesAsync();
            return View();
        }
    }
}
