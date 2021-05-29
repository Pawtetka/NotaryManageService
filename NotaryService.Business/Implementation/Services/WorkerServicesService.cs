using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NotaryDatabaseDLL.Models;
using NotaryService.Business.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Services
{
    public class WorkerServicesService : ICrudInterface<WorkerService>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<WorkerService> _validator;
        public WorkerServicesService(NotaryOfficeContext context, AbstractValidator<WorkerService> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(WorkerService model)
        {
            await _validator.ValidateAsync(model);
            await _context.WorkerServices.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var workerService = await _context.WorkerServices.FindAsync(id);
            _context.WorkerServices.Remove(workerService);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkerService>> GetAllAsync()
        {
            return await _context.WorkerServices.Include(w => w.Service).Include(w => w.Worker).ToListAsync();
        }

        public async Task<WorkerService> GetByIdAsync(int id)
        {
            return await _context.WorkerServices
                .Include(w => w.Service)
                .Include(w => w.Worker)
                .FirstOrDefaultAsync(m => m.WorkerServiceId == id);
        }

        public async Task<IEnumerable<WorkerService>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.WorkerServices
                .Include(w => w.Service)
                .Include(w => w.Worker)
                .AsQueryable().Where(a => a.WorkerServiceId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(WorkerService model)
        {
            var entity = await _context.WorkerServices.FindAsync(model.WorkerServiceId);
            await _validator.ValidateAsync(model);

            entity.ServiceId = model.ServiceId;
            entity.WorkerId = model.WorkerId;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
