using System;
using System.Linq;
using System.Security.Cryptography;
using MES_IFM_MVC.Models;
using MES_IFM_MVC.Models.Account;
using MES_IFM_MVC.Models.MailContact;
using Microsoft.AspNetCore.Mvc;

namespace MES_IFM_MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MESContext _db;
        public AccountController(MESContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Register([FromBody] aa0001 user)
        {
            string checkUser = _db.aa0001
                .Where(a => a.aa0001c13 == user.aa0001c13)
                .Select(a => a.aa0001c13).FirstOrDefault();
            if (!string.IsNullOrEmpty(checkUser))
                return BadRequest(string.Format("This mail {0} is aready exist!", checkUser));
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
                aa0001c30 = "None"
            };
            _db.aa0001.Add(outUser);
            _db.SaveChangesAsync();
            string baseURL = "https://localhost:44304";
            MailInfo mailInfo = new MailInfo();
            mailInfo.mailTo = outUser.aa0001c13;
            mailInfo.mailSubject = string.Format("Active Account From {0}", baseURL);
            mailInfo.mailMessage = string.Format("https://localhost:44304/Api/Account/ActiveEmail/?email={0}", outUser.aa0001c13);
            SendMail.SendMailAuto(mailInfo);
            return Ok("Your are registed! Please check mail and active your account!");
        }

        [HttpGet]
        public IActionResult ActiveEmail([FromQuery] string email)
        {
            aa0001 checkUser = _db.aa0001
                .Where(a => a.aa0001c13 == email)
                .Select(a => a).FirstOrDefault();
            if(checkUser == null)
            {
                //return BadRequest("Invalid email");
                return Redirect("/Account/ActiveUser");
            }
            checkUser.aa0001c15 = "1";
            _db.Update(checkUser);
            _db.SaveChangesAsync();
            return Ok(string.Format("Email {0} is actived!",email));
        }
    }
}
