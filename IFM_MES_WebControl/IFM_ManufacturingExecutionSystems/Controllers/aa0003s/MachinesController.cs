using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFM_ManufacturingExecutionSystems.Controllers.aa0003s
{
    [Route("aa0003/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly MESContext _context;

        public MachinesController(MESContext context)
        {
            _context = context;
        }

        // GET: api/Machines
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<aa0003>>> Getaa0003()
        {
            return await _context.aa0003.Where(x => !string.IsNullOrEmpty(x.aa0003c11)).ToListAsync();
        }

        // GET: api/Machines/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<aa0003>> Getaa0003(int id)
        {
            var aa0003 = await _context.aa0003.FindAsync(id);

            if (aa0003 == null)
            {
                return NotFound();
            }

            return aa0003;
        }

        // PUT: api/Machines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Putaa0003(aa0003 aa0003)
        {
            _context.Entry(aa0003).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aa0003Exists(aa0003.aa0003c01))
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

        // POST: api/Machines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<aa0003>> Postaa0003(aa0003 aa0003)
        {
            if (aa0003Exists(aa0003.aa0003c11))
            {
                return BadRequest("This Machine Is Exists!");
            }
            _context.aa0003.Add(aa0003);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0003", new { id = aa0003.aa0003c01 }, aa0003);
        }

        // DELETE: api/Machines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<aa0003>> Deleteaa0003(int id)
        {
            var aa0003 = await _context.aa0003.FindAsync(id);
            if (aa0003 == null)
            {
                return NotFound();
            }

            _context.aa0003.Remove(aa0003);
            await _context.SaveChangesAsync();

            return aa0003;
        }

        private bool aa0003Exists(int id)
        {
            return _context.aa0003.Any(e => e.aa0003c01 == id);
        }

        private bool aa0003Exists(string machinecode)
        {
            return _context.aa0003.Any(e => e.aa0003c11 == machinecode);
        }
    }
}
