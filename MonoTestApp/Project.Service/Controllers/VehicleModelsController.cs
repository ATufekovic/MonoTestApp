using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class VehicleModelsController : ControllerBase
    {
        private readonly MonoTestAppContext _context;

        public VehicleModelsController(MonoTestAppContext context)
        {
            _context = context;
        }

        // GET: api/VehicleModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleModel>>> GetVehicleModel(string? sortBy, string? searchString)
        {
          if (_context.VehicleModel == null)
          {
              return NotFound();
          }

            var vehicleModelsQuery = from m in _context.VehicleModel select m;
            vehicleModelsQuery = SetSortAndFilter(vehicleModelsQuery, sortBy, searchString);

            return await vehicleModelsQuery.ToListAsync();
        }

        [HttpGet("paged")]
        public async Task<IPagedList<VehicleModel>> GetVehicleMake(string? sortBy, string? searchString, int page = 1, int pageSize = 5)
        {
            if (_context.VehicleMake == null)
            {
                throw new InvalidOperationException("An invalid operation happened, the database seems empty!");
            }

            var vehicleModelsQuery = from m in _context.VehicleModel select m;
            vehicleModelsQuery = SetSortAndFilter(vehicleModelsQuery, sortBy, searchString);

            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException("Page number must be a whole integer larger than 0.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("Page size must be a whole integer larger than 0.");
            }

            return await vehicleModelsQuery.ToPagedListAsync(page, pageSize);
        }

        private IQueryable<VehicleModel> SetSortAndFilter(IQueryable<VehicleModel> vehicleModelQuery, string sortBy, string searchString)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                //Set sorting to preset parameters, enforced by using a switch case block and a default case
                switch (sortBy)
                {
                    case "name_desc":
                        vehicleModelQuery = vehicleModelQuery.OrderByDescending(m => m.name);
                        break;
                    case "abbr":
                        vehicleModelQuery = vehicleModelQuery.OrderBy(m => m.abbr);
                        break;
                    case "abbr_desc":
                        vehicleModelQuery = vehicleModelQuery.OrderByDescending(m => m.abbr);
                        break;
                    default:
                        vehicleModelQuery = vehicleModelQuery.OrderBy(m => m.name);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                vehicleModelQuery = vehicleModelQuery.Where(m => m.name.Contains(searchString) || m.abbr.Contains(searchString));
            }

            return vehicleModelQuery;
        }

        // GET: api/VehicleModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModel>> GetVehicleModel(int id)
        {
          if (_context.VehicleModel == null)
          {
              return NotFound();
          }
            var vehicleModel = await _context.VehicleModel.FindAsync(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return vehicleModel;
        }

        // PUT: api/VehicleModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleModel(int id, VehicleModel vehicleModel)
        {
            if (id != vehicleModel.id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleModelExists(id))
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

        // POST: api/VehicleModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleModel>> PostVehicleModel(VehicleModel vehicleModel)
        {
          if (_context.VehicleModel == null)
          {
              return Problem("Entity set 'MonoTestAppContext.VehicleModel'  is null.");
          }
            _context.VehicleModel.Add(vehicleModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleModel", new { id = vehicleModel.id }, vehicleModel);
        }

        // DELETE: api/VehicleModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            if (_context.VehicleModel == null)
            {
                return NotFound();
            }
            var vehicleModel = await _context.VehicleModel.FindAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            _context.VehicleModel.Remove(vehicleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleModelExists(int id)
        {
            return (_context.VehicleModel?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
