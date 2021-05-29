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
    public class CitiesController : Controller
    {
        private readonly ICrudInterface<City> _service;

        public CitiesController(ICrudInterface<City> service)
        {
            _service = service;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Cities/Details/5
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

        //Get: Cities/GetByPrincipalId
        public async Task<IActionResult> GetByPrincipalId(int? id)
        {
            return View(await _service.GetEntitiesByPrincipalId((int)id));
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,CityName,CityType")] City city)
        {
            try
            {
                await _service.CreateEntityAsync(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(city);
            }
        }

        // GET: Cities/Edit/5
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
            return View(model);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,CityName,CityType")] City city)
        {
            if (id != city.CityId)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateEntity(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(city);
            }
        }

        // GET: Cities/Delete/5
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

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEntityByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
