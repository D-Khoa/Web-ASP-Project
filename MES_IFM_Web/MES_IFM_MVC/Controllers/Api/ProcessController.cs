using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MES_IFM_MVC.Models;
using MES_IFM_MVC.Models.Process;

namespace MES_IFM_MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly MESContext _context;

        public ProcessController(MESContext context)
        {
            _context = context;
        }

        // GET: api/Process
        [HttpGet]
        public IEnumerable<aa0003> Getaa0003()
        {
            return _context.aa0003;
        }

        // GET: api/Process/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getaa0003([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0003 = await _context.aa0003.FindAsync(id);

            if (aa0003 == null)
            {
                return NotFound();
            }

            return Ok(aa0003);
        }

        // PUT: api/Process/5
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

        // POST: api/Process
        [HttpPost]
        public async Task<IActionResult> Postaa0003([FromBody] aa0003 aa0003)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.aa0003.Add(aa0003);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0003", new { id = aa0003.aa0003c01 }, aa0003);
        }

        // DELETE: api/Process/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaa0003([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0003 = await _context.aa0003.FindAsync(id);
            if (aa0003 == null)
            {
                return NotFound();
            }

            _context.aa0003.Remove(aa0003);
            await _context.SaveChangesAsync();

            return Ok(aa0003);
        }

        private bool aa0003Exists(int id)
        {
            return _context.aa0003.Any(e => e.aa0003c01 == id);
        }
    }
}