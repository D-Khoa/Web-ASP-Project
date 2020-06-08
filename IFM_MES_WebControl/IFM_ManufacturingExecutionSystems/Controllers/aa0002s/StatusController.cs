using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFM_ManufacturingExecutionSystems.Controllers.aa0002s
{
    [Route("aa0002/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly MESContext _context;

        public StatusController(MESContext context)
        {
            _context = context;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<aa0002>>> Getaa0002()
        {
            return await _context.aa0002.Where(x => !string.IsNullOrEmpty(x.aa0002c21)).ToListAsync();
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<aa0002>> Getaa0002(int id)
        {
            var aa0002 = await _context.aa0002.FindAsync(id);

            if (aa0002 == null)
            {
                return NotFound();
            }

            return aa0002;
        }

        // PUT: api/Status/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> Putaa0002(aa0002 aa0002)
        {
            //if (id != aa0002.aa0002c01)
            //{
            //    return BadRequest();
            //}

            _context.Entry(aa0002).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aa0002Exists(aa0002.aa0002c01))
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

        // POST: api/Status
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<aa0002>> Postaa0002(aa0002 aa0002)
        {
            if (aa0002Exists(aa0002.aa0002c21))
            {
                return BadRequest("This Status Code Is Exists!");
            }
            _context.aa0002.Add(aa0002);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0002", new { id = aa0002.aa0002c01 }, aa0002);
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<aa0002>> Deleteaa0002(int id)
        {
            var aa0002 = await _context.aa0002.FindAsync(id);
            if (aa0002 == null)
            {
                return NotFound();
            }

            _context.aa0002.Remove(aa0002);
            await _context.SaveChangesAsync();

            return aa0002;
        }

        private bool aa0002Exists(int id)
        {
            return _context.aa0002.Any(e => e.aa0002c01 == id);
        }

        private bool aa0002Exists(string statusCode)
        {
            return _context.aa0002.Any(e => e.aa0002c21 == statusCode);
        }
    }
}
