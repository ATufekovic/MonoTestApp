using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonoTestApp.Data;
using MonoTestApp.Project.Models.ServiceModels;
using MonoTestApp.Project.Models.ViewModels;

namespace MonoTestApp.Project.Service.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly MonoTestAppContext _context;

        public VehicleMakesController(MonoTestAppContext context)
        {
            _context = context;
        }

        // READ: VehicleMakes
        public async Task<IEnumerable<VehicleMakeInfo>> GetVehicleMakes()
        {
            if (_context.VehicleMake == null)
            {
                throw new ArgumentNullException("There are no vehicle makers in the database");
            }

            var query = from m in _context.VehicleMake
                        select
                        new VehicleMakeInfo
                        {
                            Name = m.Name,
                            Abbr = m.Abbr
                        };
            return await query.ToListAsync();
        }

        // READ: VehicleMake
        public async Task<VehicleMakeInfo> GetVehicleMake(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("GetVehicleMake id must be a valid integer");
            }
            if (_context.VehicleMake == null)
            {
                throw new ArgumentNullException("There are no vehicle makers currently in the database");
            }

            var vehicleMake = await _context.VehicleMake
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                throw new ArgumentNullException("There is no vehicle with that id in the database");
            }

            return vehicleMake;
        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abbr")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleMake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abbr")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleMake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicleMake.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleMake == null)
            {
                return Problem("Entity set 'MonoTestAppContext.VehicleMake'  is null.");
            }
            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake != null)
            {
                _context.VehicleMake.Remove(vehicleMake);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return (_context.VehicleMake?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
