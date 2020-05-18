using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MES_IFM_Web.Models;
using MES_IFM_Web.Models.Account;
using System.Reflection;

namespace MES_IFM_Web.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class aa0001Controller : ControllerBase
    {
        private readonly MESContext _context;

        public aa0001Controller(MESContext context)
        {
            _context = context;
        }

        // GET: api/aa0001
        [HttpGet]
        public async Task<ActionResult<IEnumerable<aa0001>>> GetAll()
        {
            return await _context.Aa0001.ToListAsync();
        }

        // GET: api/aa0001/5
        [HttpGet]
        public async Task<ActionResult<aa0001>> Get(aa0001 aa)
        {
            var properties = aa.GetType().GetProperties();
            var param = (properties.Cast<PropertyInfo>()
                .Where(x => x.GetValue(aa, null) != null && x.Name != "aa0001c01")
                .Select(x => x.GetValue(aa, null))).ToArray();
            var aa0001 = await _context.Aa0001.FindAsync(param);

            if (aa0001 == null)
            {
                return NotFound("Not found");
            }

            return aa0001;
        }

        // PUT: api/aa0001/5
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
            catch (DbUpdateConcurrencyException ex)
            {
                if (!aa0001Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/aa0001
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<aa0001>> Postaa0001(aa0001 aa0001)
        {
            _context.Aa0001.Add(aa0001);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0001", new { id = aa0001.aa0001c01 }, aa0001);
        }

        // DELETE: api/aa0001/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<aa0001>> Deleteaa0001(int id)
        {
            var aa0001 = await _context.Aa0001.FindAsync(id);
            if (aa0001 == null)
            {
                return NotFound();
            }

            _context.Aa0001.Remove(aa0001);
            await _context.SaveChangesAsync();

            return aa0001;
        }

        private bool aa0001Exists(int id)
        {
            return _context.Aa0001.Any(e => e.aa0001c01 == id);
        }
    }
}
