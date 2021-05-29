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
    public class WorkerServicesController : Controller
    {
        private readonly ICrudInterface<WorkerService> _service;
        private readonly ICrudInterface<Service> _servicesService;
        private readonly ICrudInterface<Worker> _workersService;

        public WorkerServicesController(ICrudInterface<WorkerService> service, ICrudInterface<Worker> workersService, ICrudInterface<Service> servicesService)
        {
            _service = service;
            _workersService = workersService;
            _servicesService = servicesService;
        }

        // GET: WorkerServices
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: WorkerServices/Details/5
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

        //Get: WorkerServices/GetByPrincipalId
        public async Task<IActionResult> GetByPrincipalId(int? id)
        {
            return View(await _service.GetEntitiesByPrincipalId((int)id));
        }

        // GET: WorkerServices/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_servicesService.GetAllAsync().Result, "ServiceId", "ServiceName");
            ViewData["WorkerId"] = new SelectList(_workersService.GetAllAsync().Result, "WorkerId", "FirstName");
            return View();
        }

        // POST: WorkerServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerServiceId,WorkerId,ServiceId")] WorkerService workerService)
        {
            try
            {
                await _service.CreateEntityAsync(workerService);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["ServiceId"] = new SelectList(_servicesService.GetAllAsync().Result, "ServiceId", "ServiceName", workerService.ServiceId);
                ViewData["WorkerId"] = new SelectList(_workersService.GetAllAsync().Result, "WorkerId", "FirstName", workerService.WorkerId);
                return View(workerService);
            }
        }

        // GET: WorkerServices/Edit/5
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
            ViewData["ServiceId"] = new SelectList(_servicesService.GetAllAsync().Result, "ServiceId", "ServiceName", model.ServiceId);
            ViewData["WorkerId"] = new SelectList(_workersService.GetAllAsync().Result, "WorkerId", "FirstName", model.WorkerId);
            return View(model);
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

            try
            {
                await _service.UpdateEntity(workerService);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["ServiceId"] = new SelectList(_servicesService.GetAllAsync().Result, "ServiceId", "ServiceName", workerService.ServiceId);
                ViewData["WorkerId"] = new SelectList(_workersService.GetAllAsync().Result, "WorkerId", "FirstName", workerService.WorkerId);
                return View(workerService);
            }
        }

        // GET: WorkerServices/Delete/5
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

        // POST: WorkerServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEntityByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
