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
    public class ReceptionsController : Controller
    {
        private readonly ICrudInterface<Reception> _service;
        private readonly ICrudInterface<Client> _clientsService;
        private readonly ICrudInterface<Document> _documentsService;
        private readonly ICrudInterface<Notary> _notariesService;

        public ReceptionsController(ICrudInterface<Reception> service, ICrudInterface<Client> clientsService, ICrudInterface<Document> documentsService,
            ICrudInterface<Notary> notariesService)
        {
            _service = service;
            _clientsService = clientsService;
            _documentsService = documentsService;
            _notariesService = notariesService;
        }

        // GET: Receptions
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Receptions/Details/5
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

        //Get: Receptions/GetByPrincipalId
        public async Task<IActionResult> GetByPrincipalId(int? id)
        {
            return View(await _service.GetEntitiesByPrincipalId((int)id));
        }

        // GET: Receptions/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_clientsService.GetAllAsync().Result, "ClientId", "FirstName");
            ViewData["DocumentId"] = new SelectList(_documentsService.GetAllAsync().Result, "DocumentId", "DocumentName");
            ViewData["NotaryId"] = new SelectList(_notariesService.GetAllAsync().Result, "NotaryId", "CertificateNumber");
            return View();
        }

        // POST: Receptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceptionId,ReceptionDate,Price,NotaryId,ClientId,DocumentId")] Reception reception)
        {
            try
            {
                await _service.CreateEntityAsync(reception);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["ClientId"] = new SelectList(_clientsService.GetAllAsync().Result, "ClientId", "FirstName", reception.ClientId);
                ViewData["DocumentId"] = new SelectList(_documentsService.GetAllAsync().Result, "DocumentId", "DocumentName", reception.DocumentId);
                ViewData["NotaryId"] = new SelectList(_notariesService.GetAllAsync().Result, "NotaryId", "CertificateNumber", reception.NotaryId);
                return View(reception);
            }
        }

        // GET: Receptions/Edit/5
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
            ViewData["ClientId"] = new SelectList(_clientsService.GetAllAsync().Result, "ClientId", "FirstName", model.ClientId);
            ViewData["DocumentId"] = new SelectList(_documentsService.GetAllAsync().Result, "DocumentId", "DocumentName", model.DocumentId);
            ViewData["NotaryId"] = new SelectList(_notariesService.GetAllAsync().Result, "NotaryId", "CertificateNumber", model.NotaryId);
            return View(model);
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

            try
            {
                await _service.UpdateEntity(reception);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["ClientId"] = new SelectList(_clientsService.GetAllAsync().Result, "ClientId", "FirstName", reception.ClientId);
                ViewData["DocumentId"] = new SelectList(_documentsService.GetAllAsync().Result, "DocumentId", "DocumentName", reception.DocumentId);
                ViewData["NotaryId"] = new SelectList(_notariesService.GetAllAsync().Result, "NotaryId", "CertificateNumber", reception.NotaryId);
                return View(reception);
            }
        }

        // GET: Receptions/Delete/5
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

        // POST: Receptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEntityByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
