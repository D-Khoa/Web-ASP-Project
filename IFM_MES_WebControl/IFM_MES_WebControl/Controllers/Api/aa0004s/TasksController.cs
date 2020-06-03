using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.SQL;

namespace IFM_ManufacturingExecutionSystems.Controllers.Api.aa0004s
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
        public IEnumerable<aa0004> Getaa0004_1()
        {
            return _context.aa0004_1;
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getaa0004([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0004 = await _context.aa0004_1.FindAsync(id);

            if (aa0004 == null)
            {
                return NotFound();
            }

            return Ok(aa0004);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaa0004([FromRoute] int id, [FromBody] aa0004 aa0004)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aa0004.aa0004c01)
            {
                return BadRequest();
            }

            _context.Entry(aa0004).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aa0004Exists(id))
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
        [HttpPost]
        public async Task<IActionResult> Postaa0004([FromBody] aa0004 aa0004)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.aa0004_1.Add(aa0004);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0004", new { id = aa0004.aa0004c01 }, aa0004);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaa0004([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0004 = await _context.aa0004_1.FindAsync(id);
            if (aa0004 == null)
            {
                return NotFound();
            }

            _context.aa0004_1.Remove(aa0004);
            await _context.SaveChangesAsync();

            return Ok(aa0004);
        }

        private bool aa0004Exists(int id)
        {
            return _context.aa0004_1.Any(e => e.aa0004c01 == id);
        }
    }
}