using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Address.Data;
using Newtonsoft.Json;
using Models.DTO;
using CarAPI.Address.Services;

namespace CarAPI.Address.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly CarAPIAddressContext _context;
        private readonly AddressService _addressService;

        public AddressesController(CarAPIAddressContext context, AddressService addressService)
        {
            _context = context;
            _addressService = addressService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Address>>> GetAddress()
        {
            if (_context.Address == null)
                return NotFound();
            return await _context.Address.ToListAsync();           
        }

        //GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Address>> GetAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }

            var address = await _context.Address.FindAsync(id);

            return address;
        }

        // PUT: api/Addresses/5
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
        public async Task<Models.Address> PostAddress(Models.Address newAddress)
        {
            var address = new Models.Address();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://viacep.com.br/");
                var response = await client.GetAsync($"ws/{newAddress.ZipCode}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                    address = JsonConvert.DeserializeObject<Models.Address>(stringResult);
                    address.TypeStreet = "";
                    address.Complement = newAddress.Complement;
                    address.Number = newAddress.Number;
                }
                else
                {
                    NotFound("Erro ao obter endereço do serviço via CEP");
                    return null;
                }
            }
            CreatedAtAction("GetAddress", new { id = address.Id }, address);
            _addressService.Create(address);
            return address;
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
        public async Task<ActionResult<Models.Address>> GetAddressZipCodeAsync(AddressDTO addressDTO)
        {
            string zipCode = addressDTO.ZipCode;
            int number = addressDTO.Number;
            string complement = addressDTO.Complement;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://viacep.com.br/");
                var response = await client.GetAsync($"ws/{zipCode}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var address = JsonConvert.DeserializeObject<Models.Address>(stringResult);
                    address.Complement = complement;
                    address.Number = number;
                    return address;
                }
                else
                {
                    return NotFound("Erro ao obter endereço do serviço via CEP");

                }
            }
        }
    }
}
