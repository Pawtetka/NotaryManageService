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
    public class ReceptionsController : Controller
    {
        private readonly NotaryOfficeContext _context;

        public ReceptionsController(NotaryOfficeContext context)
        {
            _context = context;
        }

        // GET: Receptions
        public async Task<IActionResult> Index()
        {
            var notaryOfficeContext = _context.Receptions.Include(r => r.Client).Include(r => r.Document).Include(r => r.Notary);
            return View(await notaryOfficeContext.ToListAsync());
        }

        // GET: Receptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reception = await _context.Receptions
                .Include(r => r.Client)
                .Include(r => r.Document)
                .Include(r => r.Notary)
                .FirstOrDefaultAsync(m => m.ReceptionId == id);
            if (reception == null)
            {
                return NotFound();
            }

            return View(reception);
        }

        // GET: Receptions/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName");
            ViewData["DocumentId"] = new SelectList(_context.Documents, "DocumentId", "DocumentName");
            ViewData["NotaryId"] = new SelectList(_context.Notaries, "NotaryId", "CertificateNumber");
            return View();
        }

        // POST: Receptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceptionId,ReceptionDate,Price,NotaryId,ClientId,DocumentId")] Reception reception)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reception);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName", reception.ClientId);
            ViewData["DocumentId"] = new SelectList(_context.Documents, "DocumentId", "DocumentName", reception.DocumentId);
            ViewData["NotaryId"] = new SelectList(_context.Notaries, "NotaryId", "CertificateNumber", reception.NotaryId);
            return View(reception);
        }

        // GET: Receptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reception = await _context.Receptions.FindAsync(id);
            if (reception == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName", reception.ClientId);
            ViewData["DocumentId"] = new SelectList(_context.Documents, "DocumentId", "DocumentName", reception.DocumentId);
            ViewData["NotaryId"] = new SelectList(_context.Notaries, "NotaryId", "CertificateNumber", reception.NotaryId);
            return View(reception);
        }

        // POST: Receptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceptionId,ReceptionDate,Price,NotaryId,ClientId,DocumentId")] Reception reception)
        {
            if (id != reception.ReceptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reception);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionExists(reception.ReceptionId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstName", reception.ClientId);
            ViewData["DocumentId"] = new SelectList(_context.Documents, "DocumentId", "DocumentName", reception.DocumentId);
            ViewData["NotaryId"] = new SelectList(_context.Notaries, "NotaryId", "CertificateNumber", reception.NotaryId);
            return View(reception);
        }

        // GET: Receptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reception = await _context.Receptions
                .Include(r => r.Client)
                .Include(r => r.Document)
                .Include(r => r.Notary)
                .FirstOrDefaultAsync(m => m.ReceptionId == id);
            if (reception == null)
            {
                return NotFound();
            }

            return View(reception);
        }

        // POST: Receptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reception = await _context.Receptions.FindAsync(id);
            _context.Receptions.Remove(reception);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionExists(int id)
        {
            return _context.Receptions.Any(e => e.ReceptionId == id);
        }
    }
}
