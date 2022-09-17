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
using MonoTestApp.Project.Service.Models.ServiceModels;
using MonoTestApp.Project.Service.Models.ViewModels;
using MonoTestApp.Project.Views.DataManipulation;
using System.Data.Entity.Validation;
using System.Globalization;

namespace MonoTestApp.Project.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly MonoTestAppContext _context;
        private readonly IMapper _mapper;

        public VehicleModelsController(MonoTestAppContext context)
        {
            _context = context;
            _mapper = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<IMapper>();

        }

        // GET: VehicleModels
        public async Task<ActionResult<IEnumerable<VehicleModelView>>> GetVehicleModels(string? searchBy, string? sortBy, int? page, int? pageSize)
        {
            if (_context.VehicleModel == null)
            {
                return NotFound();
            }

            var query = from m in _context.VehicleModel select m;

            //deal with filtering and sorting
            if (!string.IsNullOrEmpty(searchBy))
            {
                query = VehicleFilter.SearchVehicleModel(query, searchBy);
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = VehicleSort.SortVehicleModel(query, sortBy);
            }

            var converted = query.ProjectTo<VehicleModelView>(_mapper.ConfigurationProvider);

            //if it exists, deal with pagination
            if (page <= 0 || page == null)
            {
                page = 1;
            }
            if (pageSize <= 0 || pageSize == null)
            {
                pageSize = 5;
            }
            var paginated = VehiclePagination.PageVehicleModelViewAsync(converted, (int)page, (int)pageSize);
            return View(await paginated);
        }

        // GET: VehicleModel/id
        public async Task<ActionResult<VehicleModelView>> GetVehicleModel(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var queryResult = await _context.VehicleModel.FirstOrDefaultAsync(m => m.Id == id);

            if (queryResult == null)
            {
                return NotFound();
            }

            var finalResult = _mapper.Map<VehicleModelView>(queryResult);

            return View(finalResult);
        }

        // GET: VehicleModel/Create view
        public ActionResult Create()
        {
            //TODO: create new "CREATE view" for editing a new entry
            return View();
        }

        // POST: VehicleModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,Abbr,VehicleMakeId")] VehicleModel vehicleModel)
        {
            //TODO: custom validation with OnModelCreating and try-catch block? Most of these cases are caught with the Data Annotations.
            if (ModelState.IsValid)
            {
                _context.Add(vehicleModel);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: VehicleModel/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var queryResult = await _context.VehicleModel.FirstOrDefaultAsync(m => m.Id == id);

            if (queryResult == null)
            {
                return NotFound();
            }

            var finalResult = _mapper.Map<VehicleModelView>(queryResult);

            //TODO: view for editing car models
            return View(finalResult);
        }

        // POST: VehicleModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Name,Abbr,VehicleMakeId")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if ((_context.VehicleModel?.Any(vm => vm.Id == vehicleModel.Id)).GetValueOrDefault())
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ex;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(vehicleModel);
        }

        // GET: VehicleModel/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModel.FirstOrDefaultAsync(vm => vm.Id == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }
            var vehicleModel = await _context.VehicleModel.FindAsync(id);
            if (vehicleModel != null)
            {
                _context.VehicleModel.Remove(vehicleModel);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
