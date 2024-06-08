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
            return await _context.Client.Include(e => e.Address).ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Client>> GetClient(string id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.Include(e => e.Address).Where(c => c.Document == id).FirstOrDefaultAsync();

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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Models.Client>> PostClient(Models.Client client)
        //{
        //    if (_context.Client == null)
        //    {
        //        return Problem("Entity set 'CarAPIClientContext.Client'  is null.");
        //    }
        //    _context.Client.Add(client);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ClientExists(client.Document))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetClient", new { id = client.Document }, client);
        //}

        [HttpPost]
        public async Task<ActionResult<Models.Client>> PostCliente(Models.Client client)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'ProjAndreVeiculosAPIClienteContext.Cliente' is null.");
            }
            var addressResult = await _addressController.GetAddressZipCodeAsync(client.Address.ZipCode);

            client.Address.State = addressResult.Value.State;
            //client.Endereco.Bairro = endereco.Bairro;
            //client.Endereco.Uf = endereco.Uf;
            //client.Endereco.Cidade = endereco.Cidade;
            // Preencha outros campos de endereço conforme necessário
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
