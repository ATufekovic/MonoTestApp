using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonoTestApp.Data;
using MonoTestApp.Project.Service;

using X.PagedList;

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

        // GET: api/VehicleMakes, supports query strings, returns full list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMake>>> GetVehicleMake(string? sortBy, string? searchString)
        {
            if (_context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMakesQuery = from m in _context.VehicleMake select m;
            vehicleMakesQuery = SetSortAndFilter(vehicleMakesQuery, sortBy, searchString);

            return await vehicleMakesQuery.ToListAsync();
        }

        // GET: api/VehicleMakes/paged, supports query string and gives back a paged list
        // Dependency injection is kind of weird here, all packages use the same names (IPagedList, PagedList, etc.) so i don't mind leaving it like this, i just hope this is a good way
        [HttpGet("paged")]
        public async Task<IPagedList<VehicleMake>> GetVehicleMake(string? sortBy, string? searchString, int page = 1, int pageSize = 5)
        {
            if (_context.VehicleMake == null)
            {
                throw new InvalidOperationException("An invalid operation happened, the database seems empty!");
            }

            var vehicleMakesQuery = from m in _context.VehicleMake select m;
            vehicleMakesQuery = SetSortAndFilter(vehicleMakesQuery, sortBy, searchString);

            if(page <= 0)
            {
                throw new ArgumentOutOfRangeException("Page number must be a whole integer larger than 0.");
            }

            if(pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("Page size must be a whole integer larger than 0.");
            }

            return await vehicleMakesQuery.ToPagedListAsync(page, pageSize);
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

        //Sets the sorting and filtering for the given queryable Vehicle Make. Note: can't use IMake since it doesn't have the required members
        private IQueryable<VehicleMake> SetSortAndFilter(IQueryable<VehicleMake> vehicleMakesQuery, string sortBy, string searchString)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                //Set sorting to preset parameters, enforced by using a switch case block and a default case
                switch (sortBy)
                {
                    case "name_desc":
                        vehicleMakesQuery = vehicleMakesQuery.OrderByDescending(m => m.name);
                        break;
                    case "abbr":
                        vehicleMakesQuery = vehicleMakesQuery.OrderBy(m => m.abbr);
                        break;
                    case "abbr_desc":
                        vehicleMakesQuery = vehicleMakesQuery.OrderByDescending(m => m.abbr);
                        break;
                    default:
                        vehicleMakesQuery = vehicleMakesQuery.OrderBy(m => m.name);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                vehicleMakesQuery = vehicleMakesQuery.Where(m => m.name.Contains(searchString) || m.abbr.Contains(searchString));
            }

            return vehicleMakesQuery;
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
