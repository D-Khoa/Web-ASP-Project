using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MES_IFM_MVC.Models;
using MES_IFM_MVC.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MES_IFM_MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IConfiguration _config;
        private readonly MESContext _db;

        public RolesController(MESContext db, IConfiguration configuration)
        {
            _db = db;
            _config = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<aa0002> allRoles = _db.aa0002.Select(x => x).ToList();
            if (allRoles.Count <= 0)
                return NotFound("No role was found");
            return Ok(allRoles);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] aa0002 role)
        {
            MD5 algorithm = MD5.Create();
            string salt = EncryptData.RandomSalt(12);
            string roleHash = EncryptData.StringToHash(role.aa0002c21, salt, algorithm);
            aa0002 checkRole = _db.aa0002
                .Where(a => a.aa0002c21 == roleHash)
                .Select(a => a).FirstOrDefault();
            if (checkRole != null)
                return BadRequest(string.Format("This role {0} is exists!", role.aa0002c21));
            aa0002 outRole = role;
            outRole.aa0002c20 = salt;
            outRole.aa0002c21 = roleHash;
            _db.aa0002.Add(outRole);
            await _db.SaveChangesAsync();
            return Ok(string.Format("This role {0} is created!", role.aa0002c21));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] aa0002 role)
        {
            aa0002 checkRole = _db.aa0002
                .Where(a => a.aa0002c01 == role.aa0002c01)
                .Select(a => a).FirstOrDefault();
            if (checkRole == null)
                return BadRequest("This role is not exists!");
            MD5 algorithm = MD5.Create();
            string salt = checkRole.aa0002c20;
            string roleHash = EncryptData.StringToHash(role.aa0002c21, salt, algorithm);
            aa0002 outRole = role;
            outRole.aa0002c20 = salt;
            outRole.aa0002c21 = roleHash;
            _db.aa0002.Update(outRole);
            await _db.SaveChangesAsync();
            return Ok(string.Format("This role {0} is updated!", role.aa0002c21));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            aa0002 checkRole = _db.aa0002
                .Where(a => a.aa0002c01 == id)
                .Select(a => a).FirstOrDefault();
            if (checkRole == null)
                return BadRequest("This role is not exists!");
            _db.aa0002.Remove(checkRole);
            await _db.SaveChangesAsync();
            return Ok(string.Format("This role id {0} is deleted!", id));
        }
    }
}
