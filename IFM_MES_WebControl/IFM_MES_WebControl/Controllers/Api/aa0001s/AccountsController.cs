using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.Security;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IFM_ManufacturingExecutionSystems.Controllers.Api.aa0001s
{
    [Route("aa0001/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly MESContext _context;

        public AccountsController(MESContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<aa0001> GetAll()
        {
            return _context.aa0001;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0001 = await _context.aa0001.FindAsync(id);

            if (aa0001 == null)
            {
                return NotFound();
            }

            return Ok(aa0001);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] aa0001 aa0001)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aa0001.aa0001c01)
            {
                return BadRequest();
            }

            _context.Entry(aa0001).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] aa0001 aa0001)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IsExists(aa0001.aa0001c11, aa0001.aa0001c14))
            {
                return BadRequest("This username or email is exists!");
            }
            string salt = EncryptData.RandomSalt(12);
            aa0001.aa0001c05 = aa0001.aa0001c11;
            aa0001.aa0001c06 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            aa0001.aa0001c16 = "0";
            aa0001.aa0001c17 = "None";
            aa0001.aa0001c23 = EncryptData.StringToHash(aa0001.aa0001c23, salt, MD5.Create());
            _context.aa0001.Add(aa0001);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = aa0001.aa0001c01 }, aa0001);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0001 = await _context.aa0001.FindAsync(id);
            if (aa0001 == null)
            {
                return NotFound();
            }

            _context.aa0001.Remove(aa0001);
            await _context.SaveChangesAsync();

            return Ok(aa0001);
        }

        private bool IsExists(int id)
        {
            return _context.aa0001.Any(e => e.aa0001c01 == id);
        }

        private bool IsExists(string username, string email)
        {
            return _context.aa0001.Any(e => e.aa0001c14 == email || e.aa0001c11 == username);
        }

        private bool IsExistsRoleGroup(string roleGroupName)
        {
            return _context.aa0001.Any(e => e.aa0001c32 == roleGroupName);
        }
    }
}