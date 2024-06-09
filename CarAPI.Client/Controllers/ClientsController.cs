using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Client.Data;
using Models;
using CarAPI.Address.Controllers;

namespace CarAPI.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly CarAPIClientContext _context;
        private readonly AddressesController _addressController;

        public ClientsController(CarAPIClientContext context, AddressesController addressContext)
        {
            _context = context;
            _addressController = addressContext;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Client>>> GetClient()
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            return await _context.Client.Include(_addressController => _addressController.Address).ToListAsync();
               // e => e.Address).ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Client>> GetClient(string id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.Include(e => e.Address).Where(c => c.Document == id).SingleOrDefaultAsync();

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(string id, Models.Client client)
        {
            if (id != client.Document)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Models.Client>> PostCliente(Models.Client client)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'ProjAndreVeiculosAPIClienteContext.Cliente' is null.");
            }
            else
            {
                Models.Address address = new Models.Address { ZipCode = client.Address.ZipCode, Complement = client.Address.Complement, Number = client.Address.Number };

                var addressResult = await _addressController.PostAddress(address); 
                client.Address = addressResult;
                client.Address.Id = addressResult.Id;
                if (addressResult == null)
                {
                    return BadRequest("CEP invalido");
                }
                else
                {
                    _context.Client.Add(client);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        if (ClientExists(client.Document))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return CreatedAtAction("GetCliente", new { id = client.Document }, client);
                }
            }
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(string id)
        {
            return (_context.Client?.Any(e => e.Document == id)).GetValueOrDefault();
        }

    }
}
