using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFM_ManufacturingExecutionSystems.Controllers.aa0004s
{
    [Route("aa0004/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly MESContext _context;

        public TasksController(MESContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<aa0004>>> Getaa0004()
        {
            return await _context.aa0004.Where(x => !string.IsNullOrEmpty(x.aa0004c11)).ToListAsync();
        }

        // GET: api/Tasks/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<aa0004>> Getaa0004(int id)
        {
            var aa0004 = await _context.aa0004.FindAsync(id);

            if (aa0004 == null)
            {
                return NotFound();
            }

            return aa0004;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Putaa0004(aa0004 aa0004)
        {
            _context.Entry(aa0004).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aa0004Exists(aa0004.aa0004c01))
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

        // POST: api/Tasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<aa0004>> Postaa0004(aa0004 aa0004)
        {
            if (aa0004Exists(aa0004.aa0004c11))
            {
                return BadRequest("This Task Is Exists!");
            }
            _context.aa0004.Add(aa0004);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0004", new { id = aa0004.aa0004c01 }, aa0004);
        }

        // DELETE: api/Tasks/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<aa0004>> Deleteaa0004(int id)
        {
            var aa0004 = await _context.aa0004.FindAsync(id);
            if (aa0004 == null)
            {
                return NotFound();
            }

            _context.aa0004.Remove(aa0004);
            await _context.SaveChangesAsync();

            return aa0004;
        }

        private bool aa0004Exists(int id)
        {
            return _context.aa0004.Any(e => e.aa0004c01 == id);
        }

        private bool aa0004Exists(string taskcode)
        {
            return _context.aa0004.Any(e => e.aa0004c11 == taskcode);
        }
    }
}
