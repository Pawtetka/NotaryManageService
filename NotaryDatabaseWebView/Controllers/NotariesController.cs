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
    public class NotariesController : Controller
    {
        private readonly ICrudInterface<Notary> _service;
        private readonly ICrudInterface<Worker> _workersService;

        public NotariesController(ICrudInterface<Notary> service, ICrudInterface<Worker> workersService)
        {
            _service = service;
            _workersService = workersService;
        }

        // GET: Notaries
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Notaries/Details/5
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

        //Get: Notaries/GetByPrincipalId
        public async Task<IActionResult> GetByPrincipalId(int? id)
        {
            return View(await _service.GetEntitiesByPrincipalId((int)id));
        }

        // GET: Notaries/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_workersService.GetAllAsync().Result, "WorkerId", "FirstName");
            return View();
        }

        // POST: Notaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotaryId,CertificateNumber,WorkerId")] Notary notary)
        {
            try
            {
                await _service.CreateEntityAsync(notary);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["WorkerId"] = new SelectList(_workersService.GetAllAsync().Result, "WorkerId", "FirstName", notary.WorkerId);
                return View(notary);
            }
        }

        // GET: Notaries/Edit/5
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
            ViewData["WorkerId"] = new SelectList(_workersService.GetAllAsync().Result, "WorkerId", "FirstName", model.WorkerId);
            return View(model);
        }

        // POST: Notaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotaryId,CertificateNumber,WorkerId")] Notary notary)
        {
            if (id != notary.NotaryId)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateEntity(notary);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["WorkerId"] = new SelectList(_workersService.GetAllAsync().Result, "WorkerId", "FirstName", notary.WorkerId);
                return View(notary);
            }
        }

        // GET: Notaries/Delete/5
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

        // POST: Notaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEntityByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
