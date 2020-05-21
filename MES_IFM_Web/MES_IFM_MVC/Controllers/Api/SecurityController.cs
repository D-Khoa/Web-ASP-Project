using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MES_IFM_MVC.Models;
using MES_IFM_MVC.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MES_IFM_MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IConfiguration _config;
        private readonly MESContext _db;

        public SecurityController(MESContext db, IConfiguration configuration)
        {
            _db = db;
            _config = configuration;
        }

        private string GenerateJWT()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: expiry,
                                             signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        private bool ValidateUser(aa0001 loginDetails)
        {
            MD5 algorithm = MD5.Create();
            string pass = EncryptData.StringToHash(loginDetails.aa0001c21, loginDetails.aa0001c20, algorithm);
            string checkUser = _db.aa0001
                .Where(a => a.aa0001c13 == loginDetails.aa0001c13 && a.aa0001c21 == pass)
                .Select(a => a.aa0001c13).FirstOrDefault();
            if (string.IsNullOrEmpty(checkUser)) return false;
            return true;
        }

        [HttpPost]
        public IActionResult Login([FromBody] aa0001 user)
        {
            aa0001 checkUser = _db.aa0001
                .Where(a => a.aa0001c13 == user.aa0001c13)
                .Select(a => a).FirstOrDefault();
            if (checkUser == null)
                return BadRequest("Mail is not exist!");
            if (checkUser.aa0001c15 == "0")
                return BadRequest("User is not active!");
            user.aa0001c20 = checkUser.aa0001c20;
            bool result = ValidateUser(user);
            if (result)
            {
                var tokenstring = GenerateJWT();
                checkUser.aa0001c24 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                checkUser.aa0001c25 = user.aa0001c25;
                checkUser.aa0001c26 = tokenstring;
                _db.Update(checkUser);
                _db.SaveChanges();
                return Ok(new { token = tokenstring });
            }
            else
            {
                return Unauthorized();
            }
        }

        //[HttpPut]
        //public IActionResult UpdateRole([FromBody] aa0001 user)
        //{
        //    aa0001 checkUser = _db.aa0001
        //        .Where(a => a.aa0001c13 == user.aa0001c13)
        //        .Select(a => a).FirstOrDefault();
        //    if (checkUser == null)
        //        return BadRequest("Mail is not exist!");
        //}
    }
}
