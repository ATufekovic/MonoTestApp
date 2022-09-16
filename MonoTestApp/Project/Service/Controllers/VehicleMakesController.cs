using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using MonoTestApp.Data;
using MonoTestApp.Project.Models.ServiceModels;
using MonoTestApp.Project.Models.ViewModels;
using System.Data.Entity.Validation;

namespace MonoTestApp.Project.Service.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly MonoTestAppContext _context;
        private readonly Mapper _mapper;

        public VehicleMakesController(MonoTestAppContext context)
        {
            _context = context;
            _mapper = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<Mapper>();
            
        }

        // GET: VehicleMakes
        public async Task<ActionResult<IEnumerable<VehicleMakeView>>> GetVehicleMakesView()
        {
            if (_context.VehicleMake == null)
            {
                return NotFound();
            }

            var query = from m in _context.VehicleMake select m;
            var converted = query.ProjectTo<VehicleMakeView>(_mapper.ConfigurationProvider);
            
            return View(await converted.ToListAsync());
        }

        // GET: VehicleMake by id
        public async Task<ActionResult<VehicleMakeView>> GetVehicleMake(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var queryResult = await _context.VehicleMake.FirstOrDefaultAsync(m => m.Id == id);

            if (queryResult == null)
            {
                return NotFound();
            }
            
            var finalResult = _mapper.Map<VehicleMakeView>(queryResult);

            return View(finalResult);
        }

        // GET: VehicleMakeController/Create view
        public ActionResult Create()
        {
            //TODO: create new "CREATE view" for editing a new entry
            return View();
        }

        // POST: VehicleMakeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,Abbr")] VehicleMake vehicleMake)
        {
            //TODO: custom validation with OnModelCreating and try-catch block? Most of these cases are caught with the Data Annotations.
            if (ModelState.IsValid)
            {
                _context.Add(vehicleMake);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: VehicleMakeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var queryResult = await _context.VehicleMake.FirstOrDefaultAsync(m => m.Id == id);

            if (queryResult == null)
            {
                return NotFound();
            }

            var finalResult = _mapper.Map<VehicleMakeView>(queryResult);

            //TODO: view for editing car makes
            return View(finalResult);
        }

        // POST: VehicleMakeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Name,Abbr")] VehicleMake vehicleMake)
        {
            if(id != vehicleMake.Id)
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
                catch (DbUpdateConcurrencyException ex)
                {
                    if((_context.VehicleMake?.Any(vm=>vm.Id == vehicleMake.Id)).GetValueOrDefault())
                    {
                        return NotFound();
                    } else
                    {
                        throw ex;
                    }
                }

                return RedirectToAction(nameof(Index);
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if(id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake.FirstOrDefaultAsync(vm => vm.Id == id);
            if(vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // POST: VehicleMakeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if(id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }
            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if(vehicleMake != null)
            {
                _context.VehicleMake.Remove(vehicleMake);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
