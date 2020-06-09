using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Authorization;

namespace IFM_ManufacturingExecutionSystems.Controllers.aa0001s
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

        // GET: api/Account
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<aa0001>>> Getaa0001()
        {
            return await _context.aa0001.ToListAsync();
        }

        // GET: api/Account/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<aa0001>> Getaa0001(int id)
        {
            var aa0001 = await _context.aa0001.FindAsync(id);

            if (aa0001 == null)
            {
                return NotFound();
            }

            return aa0001;
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaa0001(int id, aa0001 aa0001)
        {
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
                if (!aa0001Exists(id))
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

        // POST: api/Account
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<aa0001>> Postaa0001(aa0001 aa0001)
        {
            if(aa0001Exists(aa0001.aa0001c11, aa0001.aa0001c14))
            {
                return BadRequest("This account is really exists!");
            }

            _context.aa0001.Add(aa0001);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0001", new { id = aa0001.aa0001c01 }, aa0001);
        }

        // DELETE: api/Account/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<aa0001>> Deleteaa0001(int id)
        {
            var aa0001 = await _context.aa0001.FindAsync(id);
            if (aa0001 == null)
            {
                return NotFound();
            }

            _context.aa0001.Remove(aa0001);
            await _context.SaveChangesAsync();

            return aa0001;
        }

        private bool aa0001Exists(int id)
        {
            return _context.aa0001.Any(e => e.aa0001c01 == id);
        }

        private bool aa0001Exists(string username, string email)
        {
            return _context.aa0001.Any(e => e.aa0001c14 == email || e.aa0001c11 == username);
        }
    }
}
