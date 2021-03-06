﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
        private readonly MESContext _context;
        //string baseUrL = "https://localhost:44304";
        private readonly string baseUrl = "https://192.168.1.68:8080";

        public AccountController(MESContext db)
        {
            _context = db;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] aa0001 user)
        {
            string checkUser = _context.aa0001
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
                aa0001c15 = "0",
                aa0001c16 = user.aa0001c16,
                aa0001c20 = salt,
                aa0001c21 = EncryptData.StringToHash(user.aa0001c21, salt, algorithm),
                aa0001c23 = "Not active",
                aa0001c26 = EncryptData.StringToHash(user.aa0001c13, salt, algorithm),
            };
            _context.aa0001.Add(outUser);
            await _context.SaveChangesAsync();
            MailInfo mailInfo = new MailInfo
            {
                mailTo = outUser.aa0001c13,
                mailSubject = string.Format("Active Account From {0}", baseUrl),
                mailMessage = string.Format("{0}/Account/ActiveUser/?email={1}", baseUrl, outUser.aa0001c13)
            };
            SendMail.SendMailAuto(mailInfo);
            return Ok("Your are registed! Please check mail and active your account!");
        }

        //[HttpGet]
        //public IActionResult ActiveEmail([FromQuery]string email)
        //{
        //    aa0001 checkUser = _db.aa0001
        //        .Where(a => a.aa0001c13 == email)
        //        .Select(a => a).FirstOrDefault();
        //    if(checkUser == null)
        //    {
        //        return BadRequest("Invalid email");
        //    }
        //    checkUser.aa0001c15 = "1";
        //    _db.Update(checkUser);
        //    _db.SaveChangesAsync();
        //    return Ok(string.Format("Email {0} is actived!",email));
        //}

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            if (_context.aa0001.Count() > 0)
                return Ok(_context.aa0001);
            else
                return NotFound("Don't have any users!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.aa0001.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
