using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.Security;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IFM_ManufacturingExecutionSystems.Controllers.Api.aa0001s
{
    [Route("aa0001/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IConfiguration _config;
        private readonly MESContext _context;

        public SecurityController(MESContext db, IConfiguration configuration)
        {
            _context = db;
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

        private bool ValidateUser(aa0001 aa0001)
        {
            aa0001.aa0001c23 = EncryptData.StringToHash(aa0001.aa0001c23, aa0001.aa0001c21, MD5.Create());
            return _context.aa0001.Any(a => a.aa0001c11 == aa0001.aa0001c11 && a.aa0001c23 == aa0001.aa0001c23);
        }

        private bool isExists(string username)
        {
            return _context.aa0001.Any(e => e.aa0001c11 == username);
        }

        private string getSalt(string username)
        {
            return _context.aa0001.Where(a => a.aa0001c11 == username).Select(x => x.aa0001c21).FirstOrDefault();
        }

        private bool isActive(string username)
        {
            return _context.aa0001.Any(e => e.aa0001c11 == username && e.aa0001c16 != "0");
        }

        [HttpPut]
        public IActionResult Login([FromBody] aa0001 aa0001)
        {
            if (isExists(aa0001.aa0001c11))
                return BadRequest("Username is not exist!");
            if (isActive(aa0001.aa0001c11))
                return BadRequest("Username is not active!");
            aa0001.aa0001c21 = getSalt(aa0001.aa0001c11);
            string ipAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (ValidateUser(aa0001))
            {
                var tokenstring = GenerateJWT();
                aa0001.aa0001c22 = ipAdress;
                aa0001.aa0001c24 = tokenstring;
                aa0001.aa0001c25 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _context.Update(aa0001);
                _context.SaveChanges();
                return Ok(new { token = tokenstring });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
