using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Payment.Data;
using Models;

namespace CarAPI.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankPaymentSlipsController : ControllerBase
    {
        private readonly CarAPIPaymentContext _context;

        public BankPaymentSlipsController(CarAPIPaymentContext context)
        {
            _context = context;
        }

        // GET: api/BankPaymentSlips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankPaymentSlip>>> GetBankPaymentSlip()
        {
          if (_context.BankPaymentSlip == null)
          {
              return NotFound();
          }
            return await _context.BankPaymentSlip.ToListAsync();
        }

        // GET: api/BankPaymentSlips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankPaymentSlip>> GetBankPaymentSlip(int id)
        {
          if (_context.BankPaymentSlip == null)
          {
              return NotFound();
          }
            var bankPaymentSlip = await _context.BankPaymentSlip.FindAsync(id);

            if (bankPaymentSlip == null)
            {
                return NotFound();
            }

            return bankPaymentSlip;
        }

        // PUT: api/BankPaymentSlips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankPaymentSlip(int id, BankPaymentSlip bankPaymentSlip)
        {
            if (id != bankPaymentSlip.Id)
            {
                return BadRequest();
            }

            _context.Entry(bankPaymentSlip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankPaymentSlipExists(id))
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

        // POST: api/BankPaymentSlips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BankPaymentSlip>> PostBankPaymentSlip(BankPaymentSlip bankPaymentSlip)
        {
          if (_context.BankPaymentSlip == null)
          {
              return Problem("Entity set 'CarAPIPaymentContext.BankPaymentSlip'  is null.");
          }
            _context.BankPaymentSlip.Add(bankPaymentSlip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankPaymentSlip", new { id = bankPaymentSlip.Id }, bankPaymentSlip);
        }

        // DELETE: api/BankPaymentSlips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankPaymentSlip(int id)
        {
            if (_context.BankPaymentSlip == null)
            {
                return NotFound();
            }
            var bankPaymentSlip = await _context.BankPaymentSlip.FindAsync(id);
            if (bankPaymentSlip == null)
            {
                return NotFound();
            }

            _context.BankPaymentSlip.Remove(bankPaymentSlip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankPaymentSlipExists(int id)
        {
            return (_context.BankPaymentSlip?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
