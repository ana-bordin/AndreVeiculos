using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.CarJob.Data;
using Models;
using Models.DTO;

namespace CarAPI.CarJob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarJobsController : ControllerBase
    {
        private readonly CarAPICarJobContext _context;

        public CarJobsController(CarAPICarJobContext context)
        {
            _context = context;
        }

        // GET: api/CarJobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.CarJob>>> GetCarJob(CarJobDTO carJobDTO)
        {
          if (_context.CarJob == null)
          {
              return NotFound();
          }
            Models.CarJob carJob = new Models.CarJob();

            carJob.Car = await _context.Car.FindAsync(carJobDTO.Car.LicensePlate);
            carJob.Job = await _context.Job.FindAsync(carJobDTO.Job.Id);

            _context.CarJob.Add(carJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarJob", new { id = carJob.Id }, carJob);

        }

        // GET: api/CarJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.CarJob>> GetCarJob(int id)
        {
          if (_context.CarJob == null)
          {
              return NotFound();
          }
            var carJob = await _context.CarJob.FindAsync(id);

            if (carJob == null)
            {
                return NotFound();
            }
            _context.CarJob.Remove(carJob);
            await _context.SaveChangesAsync();

            return carJob;
        }

        // PUT: api/CarJobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarJob(int id, Models.CarJob carJob)
        {
            if (id != carJob.Id)
            {
                return BadRequest();
            }

            _context.Entry(carJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarJobExists(id))
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

        // POST: api/CarJobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.CarJob>> PostCarJob(Models.CarJob carJob)
        {
          if (_context.CarJob == null)
          {
              return Problem("Entity set 'CarAPICarJobContext.CarJob'  is null.");
          }
            _context.CarJob.Add(carJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarJob", new { id = carJob.Id }, carJob);
        }

        // DELETE: api/CarJobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarJob(int id)
        {
            if (_context.CarJob == null)
            {
                return NotFound();
            }
            var carJob = await _context.CarJob.FindAsync(id);
            if (carJob == null)
            {
                return NotFound();
            }

            _context.CarJob.Remove(carJob);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarJobExists(int id)
        {
            return (_context.CarJob?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
