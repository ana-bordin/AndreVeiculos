﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Data;
using Models;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PixesController : ControllerBase
    {
        private readonly CarAPIContext _context;

        public PixesController(CarAPIContext context)
        {
            _context = context;
        }

        // GET: api/Pixes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pix>>> GetPix()
        {
          if (_context.Pix == null)
          {
              return NotFound();
          }
            return await _context.Pix.Include(e => e.PixType).ToListAsync();
        }

        // GET: api/Pixes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pix>> GetPix(int id)
        {
          if (_context.Pix == null)
          {
              return NotFound();
          }
            var pix = await _context.Pix.Include(e => e.PixType).Where(e => e.Id == id).FirstOrDefaultAsync();

            if (pix == null)
            {
                return NotFound();
            }

            return pix;
        }

        // PUT: api/Pixes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPix(int id, Pix pix)
        {
            if (id != pix.Id)
            {
                return BadRequest();
            }

            _context.Entry(pix).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PixExists(id))
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

        // POST: api/Pixes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pix>> PostPix(Pix pix)
        {
          if (_context.Pix == null)
          {
              return Problem("Entity set 'CarAPIContext.Pix'  is null.");
          }
            _context.Pix.Add(pix);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPix", new { id = pix.Id }, pix);
        }

        // DELETE: api/Pixes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePix(int id)
        {
            if (_context.Pix == null)
            {
                return NotFound();
            }
            var pix = await _context.Pix.FindAsync(id);
            if (pix == null)
            {
                return NotFound();
            }

            _context.Pix.Remove(pix);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PixExists(int id)
        {
            return (_context.Pix?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
