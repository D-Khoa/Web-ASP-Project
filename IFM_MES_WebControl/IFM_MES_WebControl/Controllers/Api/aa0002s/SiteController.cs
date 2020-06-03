﻿using IFM_ManufacturingExecutionSystems.Models.Database;
using IFM_ManufacturingExecutionSystems.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFM_ManufacturingExecutionSystems.Controllers.Api.aa0002s
{
    [Route("aa0002/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly MESContext _context;

        public SiteController(MESContext context)
        {
            _context = context;
        }

        // GET: api/Site
        [HttpGet]
        public IEnumerable<aa0002> Getaa0002()
        {
            return _context.aa0002;
        }

        // GET: api/Site/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getaa0002([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0002 = await _context.aa0002.FindAsync(id);

            if (aa0002 == null)
            {
                return NotFound();
            }

            return Ok(aa0002);
        }

        // PUT: api/Site/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaa0002([FromRoute] int id, [FromBody] aa0002 aa0002)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aa0002.aa002c01)
            {
                return BadRequest();
            }

            _context.Entry(aa0002).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aa0002Exists(id))
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

        // POST: api/Site
        [HttpPost]
        public async Task<IActionResult> Postaa0002([FromBody] aa0002 aa0002)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.aa0002.Add(aa0002);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaa0002", new { id = aa0002.aa002c01 }, aa0002);
        }

        // DELETE: api/Site/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaa0002([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aa0002 = await _context.aa0002.FindAsync(id);
            if (aa0002 == null)
            {
                return NotFound();
            }

            _context.aa0002.Remove(aa0002);
            await _context.SaveChangesAsync();

            return Ok(aa0002);
        }

        private bool aa0002Exists(int id)
        {
            return _context.aa0002.Any(e => e.aa002c01 == id);
        }
    }
}