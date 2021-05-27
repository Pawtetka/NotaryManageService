using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotaryDatabaseDLL.Models;

namespace NotaryDatabaseWebView.Controllers
{
    public class WorkerServicesController : Controller
    {
        private readonly NotaryOfficeContext _context;

        public WorkerServicesController(NotaryOfficeContext context)
        {
            _context = context;
        }

        // GET: WorkerServices
        public async Task<IActionResult> Index()
        {
            var notaryOfficeContext = _context.WorkerServices.Include(w => w.Service).Include(w => w.Worker);
            return View(await notaryOfficeContext.ToListAsync());
        }

        // GET: WorkerServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerService = await _context.WorkerServices
                .Include(w => w.Service)
                .Include(w => w.Worker)
                .FirstOrDefaultAsync(m => m.WorkerServiceId == id);
            if (workerService == null)
            {
                return NotFound();
            }

            return View(workerService);
        }

        // GET: WorkerServices/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "FirstName");
            return View();
        }

        // POST: WorkerServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerServiceId,WorkerId,ServiceId")] WorkerService workerService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workerService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", workerService.ServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "FirstName", workerService.WorkerId);
            return View(workerService);
        }

        // GET: WorkerServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerService = await _context.WorkerServices.FindAsync(id);
            if (workerService == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", workerService.ServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "FirstName", workerService.WorkerId);
            return View(workerService);
        }

        // POST: WorkerServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkerServiceId,WorkerId,ServiceId")] WorkerService workerService)
        {
            if (id != workerService.WorkerServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workerService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerServiceExists(workerService.WorkerServiceId))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", workerService.ServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "FirstName", workerService.WorkerId);
            return View(workerService);
        }

        // GET: WorkerServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerService = await _context.WorkerServices
                .Include(w => w.Service)
                .Include(w => w.Worker)
                .FirstOrDefaultAsync(m => m.WorkerServiceId == id);
            if (workerService == null)
            {
                return NotFound();
            }

            return View(workerService);
        }

        // POST: WorkerServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workerService = await _context.WorkerServices.FindAsync(id);
            _context.WorkerServices.Remove(workerService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerServiceExists(int id)
        {
            return _context.WorkerServices.Any(e => e.WorkerServiceId == id);
        }
    }
}
