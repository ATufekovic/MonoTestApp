using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonoTestApp.Data;
using MonoTestApp.Project.Service;

namespace MonoTestApp.Project.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakesController : ControllerBase
    {
        private readonly MonoTestAppContext _context;

        public VehicleMakesController(MonoTestAppContext context)
        {
            _context = context;
        }

        // GET: api/VehicleMakes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMake>>> GetVehicleMake()
        {
          if (_context.VehicleMake == null)
          {
              return NotFound();
          }
            return await _context.VehicleMake.ToListAsync();
        }

        // GET: api/VehicleMakes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMake>> GetVehicleMake(int id)
        {
          if (_context.VehicleMake == null)
          {
              return NotFound();
          }
            var vehicleMake = await _context.VehicleMake.FindAsync(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            return vehicleMake;
        }

        // PUT: api/VehicleMakes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMake(int id, VehicleMake vehicleMake)
        {
            if (id != vehicleMake.id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleMake).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleMakeExists(id))
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

        // POST: api/VehicleMakes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleMake>> PostVehicleMake(VehicleMake vehicleMake)
        {
          if (_context.VehicleMake == null)
          {
              return Problem("Entity set 'MonoTestAppContext.VehicleMake'  is null.");
          }
            _context.VehicleMake.Add(vehicleMake);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleMake", new { id = vehicleMake.id }, vehicleMake);
        }

        // DELETE: api/VehicleMakes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMake(int id)
        {
            if (_context.VehicleMake == null)
            {
                return NotFound();
            }
            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            _context.VehicleMake.Remove(vehicleMake);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleMakeExists(int id)
        {
            return (_context.VehicleMake?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
