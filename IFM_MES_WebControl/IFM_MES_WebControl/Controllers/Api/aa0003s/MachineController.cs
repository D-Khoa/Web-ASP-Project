using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.SQL;

namespace IFM_ManufacturingExecutionSystems.Controllers.Api.aa0003s
{
    [Route("aa0003/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly MESContext _context;

        public MachineController(MESContext context)
        {
            _context = context;
        }

        // GET: api/Machine
        [HttpGet]
        public IEnumerable<aa0003> Getaa0003_1()
        {
            return _context.aa0003_1;
        }

        // GET: api/Machine/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getaa0003([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0003 = await _context.aa0003_1.FindAsync(id);

            if (aa0003 == null)
            {
                return NotFound();
            }

            return Ok(aa0003);
        }

        // PUT: api/Machine/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaa0003([FromRoute] int id, [FromBody] aa0003 aa0003)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aa0003.aa0003c01)
            {
                return BadRequest();
            }

            _context.Entry(aa0003).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aa0003Exists(id))
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

        // POST: api/Machine
        [HttpPost]
        public async Task<IActionResult> Postaa0003([FromBody] aa0003 aa0003)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.aa0003_1.Add(aa0003);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0003", new { id = aa0003.aa0003c01 }, aa0003);
        }

        // DELETE: api/Machine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaa0003([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0003 = await _context.aa0003_1.FindAsync(id);
            if (aa0003 == null)
            {
                return NotFound();
            }

            _context.aa0003_1.Remove(aa0003);
            await _context.SaveChangesAsync();

            return Ok(aa0003);
        }

        private bool aa0003Exists(int id)
        {
            return _context.aa0003_1.Any(e => e.aa0003c01 == id);
        }
    }
}