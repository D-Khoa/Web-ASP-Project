using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFM_ManufacturingExecutionSystems.Controllers.aa0001s
{
    [Route("aa0001/[controller]")]
    [ApiController]
    public class RoleGroupsController : ControllerBase
    {
        private readonly MESContext _context;

        public RoleGroupsController(MESContext context)
        {
            _context = context;
        }

        // GET: api/RoleGroups
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<aa0001>>> Getaa0001()
        {
            return await _context.aa0001.Where(x => !string.IsNullOrEmpty(x.aa0001c31)).ToListAsync();
        }

        // GET: api/RoleGroups/5
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

        // PUT: api/RoleGroups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Putaa0001(aa0001 aa0001)
        {
            _context.Entry(aa0001).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aa0001Exists(aa0001.aa0001c01))
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

        // POST: api/RoleGroups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<aa0001>> Postaa0001(aa0001 aa0001)
        {
            _context.aa0001.Add(aa0001);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0001", new { id = aa0001.aa0001c01 }, aa0001);
        }

        // DELETE: api/RoleGroups/5
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
    }
}
