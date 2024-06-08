using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Data;
using Models;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuysController : ControllerBase
    {
        private readonly CarAPIContext _context;

        public BuysController(CarAPIContext context)
        {
            _context = context;
        }

        // GET: api/Buys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buy>>> GetBuy()
        {
          if (_context.Buy == null)
          {
              return NotFound();
          }
            return await _context.Buy.Include(e => e.Car).ToListAsync();
        }

        // GET: api/Buys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buy>> GetBuy(int id)
        {
          if (_context.Buy == null)
          {
              return NotFound();
          }
            var buy = await _context.Buy.Include(e => e.Car).FirstOrDefaultAsync();

            if (buy == null)
            {
                return NotFound();
            }

            return buy;
        }

        // PUT: api/Buys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuy(int id, Buy buy)
        {
            if (id != buy.Id)
            {
                return BadRequest();
            }

            _context.Entry(buy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyExists(id))
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

        // POST: api/Buys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Buy>> PostBuy(Buy buy)
        {
          if (_context.Buy == null)
          {
              return Problem("Entity set 'CarAPIContext.Buy'  is null.");
          }
            _context.Buy.Add(buy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuy", new { id = buy.Id }, buy);
        }

        // DELETE: api/Buys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuy(int id)
        {
            if (_context.Buy == null)
            {
                return NotFound();
            }
            var buy = await _context.Buy.FindAsync(id);
            if (buy == null)
            {
                return NotFound();
            }

            _context.Buy.Remove(buy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuyExists(int id)
        {
            return (_context.Buy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
