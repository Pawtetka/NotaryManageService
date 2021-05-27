using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotaryDatabaseWebView.Models;

namespace NotaryDatabaseWebView.Controllers
{
    public class NotariesController : Controller
    {
        private readonly NotaryOfficeContext _context;

        public NotariesController(NotaryOfficeContext context)
        {
            _context = context;
        }

        // GET: Notaries
        public async Task<IActionResult> Index()
        {
            var notaryOfficeContext = _context.Notaries.Include(n => n.Worker);
            return View(await notaryOfficeContext.ToListAsync());
        }

        // GET: Notaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notary = await _context.Notaries
                .Include(n => n.Worker)
                .FirstOrDefaultAsync(m => m.NotaryId == id);
            if (notary == null)
            {
                return NotFound();
            }

            return View(notary);
        }

        // GET: Notaries/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "FirstName");
            return View();
        }

        // POST: Notaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotaryId,CertificateNumber,WorkerId")] Notary notary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "FirstName", notary.WorkerId);
            return View(notary);
        }

        // GET: Notaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notary = await _context.Notaries.FindAsync(id);
            if (notary == null)
            {
                return NotFound();
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "FirstName", notary.WorkerId);
            return View(notary);
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaryExists(notary.NotaryId))
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
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "FirstName", notary.WorkerId);
            return View(notary);
        }

        // GET: Notaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notary = await _context.Notaries
                .Include(n => n.Worker)
                .FirstOrDefaultAsync(m => m.NotaryId == id);
            if (notary == null)
            {
                return NotFound();
            }

            return View(notary);
        }

        // POST: Notaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notary = await _context.Notaries.FindAsync(id);
            _context.Notaries.Remove(notary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaryExists(int id)
        {
            return _context.Notaries.Any(e => e.NotaryId == id);
        }
    }
}
