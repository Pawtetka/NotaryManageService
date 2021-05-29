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
    public class OfficesController : Controller
    {
        private readonly ICrudInterface<Office> _service;
        private readonly ICrudInterface<Location> _locationsService;

        public OfficesController(ICrudInterface<Office> service, ICrudInterface<Location> locationsService)
        {
            _service = service;
            _locationsService = locationsService;
        }

        // GET: Offices
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Offices/Details/5
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

        //Get: Offices/GetByPrincipalId
        public async Task<IActionResult> GetByPrincipalId(int? id)
        {
            return View(await _service.GetEntitiesByPrincipalId((int)id));
        }

        // GET: Offices/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_locationsService.GetAllAsync().Result, "LocationId", "Address");
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfficeId,OfficeName,OfficeStatus,OfficeSize,LocationId")] Office office)
        {
            try
            {
                await _service.CreateEntityAsync(office);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["LocationId"] = new SelectList(_locationsService.GetAllAsync().Result, "LocationId", "Address", office.LocationId);
                return View(office);
            }
        }

        // GET: Offices/Edit/5
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
            ViewData["LocationId"] = new SelectList(_locationsService.GetAllAsync().Result, "LocationId", "Address", model.LocationId);
            return View(model);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfficeId,OfficeName,OfficeStatus,OfficeSize,LocationId")] Office office)
        {
            if (id != office.OfficeId)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateEntity(office);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["LocationId"] = new SelectList(_locationsService.GetAllAsync().Result, "LocationId", "Address", office.LocationId);
                return View(office);
            }
        }

        // GET: Offices/Delete/5
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

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEntityByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
