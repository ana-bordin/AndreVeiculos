using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Data;
using Models;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionCompaniesController : ControllerBase
    {
        private readonly CarAPIContext _context;

        public PositionCompaniesController(CarAPIContext context)
        {
            _context = context;
        }

        // GET: api/PositionCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionCompany>>> GetPositionCompany()
        {
          if (_context.PositionCompany == null)
          {
              return NotFound();
          }
            return await _context.PositionCompany.ToListAsync();
        }

        // GET: api/PositionCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionCompany>> GetPositionCompany(int id)
        {
          if (_context.PositionCompany == null)
          {
              return NotFound();
          }
            var positionCompany = await _context.PositionCompany.FindAsync(id);

            if (positionCompany == null)
            {
                return NotFound();
            }

            return positionCompany;
        }

        // PUT: api/PositionCompanies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionCompany(int id, PositionCompany positionCompany)
        {
            if (id != positionCompany.Id)
            {
                return BadRequest();
            }

            _context.Entry(positionCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionCompanyExists(id))
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

        // POST: api/PositionCompanies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PositionCompany>> PostPositionCompany(PositionCompany positionCompany)
        {
          if (_context.PositionCompany == null)
          {
              return Problem("Entity set 'CarAPIContext.PositionCompany'  is null.");
          }
            _context.PositionCompany.Add(positionCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositionCompany", new { id = positionCompany.Id }, positionCompany);
        }

        // DELETE: api/PositionCompanies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionCompany(int id)
        {
            if (_context.PositionCompany == null)
            {
                return NotFound();
            }
            var positionCompany = await _context.PositionCompany.FindAsync(id);
            if (positionCompany == null)
            {
                return NotFound();
            }

            _context.PositionCompany.Remove(positionCompany);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionCompanyExists(int id)
        {
            return (_context.PositionCompany?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
