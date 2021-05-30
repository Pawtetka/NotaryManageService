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
    public class WorkersController : Controller
    {
        private readonly ICrudInterface<Worker> _service;
        private readonly ICrudInterface<Office> _officesService;

        public WorkersController(ICrudInterface<Worker> service, ICrudInterface<Office> officesService)
        {
            _service = service;
            _officesService = officesService;
        }

        // GET: Workers
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return View(await _service.GetAllAsync());
            }

            return View(await _service.GetAllAsync(w => w.OfficeId == id));
        }

        // GET: Workers/Details/5
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

        //Get: Workers/GetByPrincipalId
        public async Task<IActionResult> GetByPrincipalId(int? id)
        {
            return View(await _service.GetEntitiesByPrincipalId((int)id));
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            ViewData["OfficeId"] = new SelectList(_officesService.GetAllAsync().Result, "OfficeId", "OfficeName");
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerId,FirstName,LastName,Age,PassportNumber,Salary,PhoneNumber,HireDate,OfficeId")] Worker worker)
        {
            try
            {
                await _service.CreateEntityAsync(worker);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["OfficeId"] = new SelectList(_officesService.GetAllAsync().Result, "OfficeId", "OfficeName", worker.OfficeId);
                return View(worker);
            }
        }

        // GET: Workers/Edit/5
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
            ViewData["OfficeId"] = new SelectList(_officesService.GetAllAsync().Result, "OfficeId", "OfficeName", model.OfficeId);
            return View(model);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkerId,FirstName,LastName,Age,PassportNumber,Salary,PhoneNumber,HireDate,OfficeId")] Worker worker)
        {
            if (id != worker.WorkerId)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateEntity(worker);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(worker);
            }
        }

        // GET: Workers/Delete/5
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

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEntityByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
