using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotaryDatabaseDLL.Models;
using NotaryService.Business.Abstraction;

namespace NotaryDatabaseWebView.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ICrudInterface<Location> _service;
        private readonly ICrudInterface<City> _citiesService;

        public LocationsController(ICrudInterface<Location> service, ICrudInterface<City> citiesService)
        {
            _service = service;
            _citiesService = citiesService;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _service.GetByIdAsync((int)id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        //Get: Locations/GetByPrincipalId
        public async Task<IActionResult> GetByPrincipalId(int? id)
        {
            return View(await _service.GetEntitiesByPrincipalId((int)id));
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_citiesService.GetAllAsync().Result, "CityId", "CityName");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,Address,CityId")] Location location)
        {
            try
            {
                await _service.CreateEntityAsync(location);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["CityId"] = new SelectList(_citiesService.GetAllAsync().Result, "CityId", "CityName", location.CityId);
                return View(location);
            }
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _service.GetByIdAsync((int)id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_citiesService.GetAllAsync().Result, "CityId", "CityName", model.CityId);
            return View(model);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationId,Address,CityId")] Location location)
        {
            if (id != location.LocationId)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateEntity(location);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["CityId"] = new SelectList(_citiesService.GetAllAsync().Result, "CityId", "CityName", location.CityId);
                return View(location);
            }
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _service.GetByIdAsync((int)id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEntityByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
