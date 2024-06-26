﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Employee.Data;
using CarAPI.Address.Controllers;

namespace CarAPI.Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CarAPIEmployeeContext _context;
        private readonly AddressesController _addressController;

        public EmployeesController(CarAPIEmployeeContext context, AddressesController addressController)
        {
            _context = context;
            _addressController = addressController;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Employee>>> GetEmployee()
        {
          if (_context.Employee == null)
          {
              return NotFound();
          }
            return await _context.Employee.Include(e => e.Address).Include(c => c.PositionCompany).ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Employee>> GetEmployee(string id)
        {
          if (_context.Employee == null)
          {
              return NotFound();
          }
            var employee = await _context.Employee.Include(e => e.Address).Include(c => c.PositionCompany).Where(d => d.Document == id).SingleOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(string id, Models.Employee employee)
        {
            if (id != employee.Document)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Employee>> PostEmployee(Models.Employee employee)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'ProjAndreVeiculosAPIClienteContext.Employee' is null.");
            }
            else
            {
                Models.Address address = new Models.Address { ZipCode = employee.Address.ZipCode, Complement = employee.Address.Complement, Number = employee.Address.Number };

                var addressResult = await _addressController.PostAddress(address);
                employee.Address = addressResult;
                employee.Address.Id = addressResult.Id;
                if (addressResult == null)
                {
                    return BadRequest("CEP invalido");
                }
                else
                {
                    _context.Employee.Add(employee);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        if (EmployeeExists(employee.Document))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return CreatedAtAction("GetEmployee", new { id = employee.Document }, employee);
                }
            }
            //if (_context.Employee == null)
            //{
            //    return Problem("Entity set 'CarAPIEmployeeContext.Employee'  is null.");
            //}
            //  _context.Employee.Add(employee);
            //  try
            //  {
            //      await _context.SaveChangesAsync();
            //  }
            //  catch (DbUpdateException)
            //  {
            //      if (EmployeeExists(employee.Document))
            //      {
            //          return Conflict();
            //      }
            //      else
            //      {
            //          throw;
            //      }
            //  }

            //  return CreatedAtAction("GetEmployee", new { id = employee.Document }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string id)
        {
            return (_context.Employee?.Any(e => e.Document == id)).GetValueOrDefault();
        }
    }
}
