using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Address.Data;
using Newtonsoft.Json;

namespace CarAPI.Address.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly CarAPIAddressContext _context;

        public AddressesController(CarAPIAddressContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Address>>> GetAddress()
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            return await _context.Address.ToListAsync();
        }

        // GET: api/Addresses/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Models.Address>> GetAddress(int id)
        //{
        //    if (_context.Address == null)
        //    {
        //        return NotFound();
        //    }

        //    var address = await _context.Address.FindAsync(id);
            
        //    return address;
        //}

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Models.Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Address>> PostAddress(Models.Address address)
        {
            if (_context.Address == null)
            {
                return Problem("Entity set 'CarAPIAddressContext.Address'  is null.");
            }
            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("zipCode/{zipCode}")]
        public async Task<ActionResult<Models.Address>> GetAddressZipCodeAsync(string zipCode)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://viacep.com.br/");
                var response = await client.GetAsync($"ws/{zipCode}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var address = JsonConvert.DeserializeObject<Models.Address>(stringResult);
                    return address;
                }
                else
                {
                    return NotFound("Erro ao obter endereço do serviço ViaCEP");

                }
            }
        }
    }
}
