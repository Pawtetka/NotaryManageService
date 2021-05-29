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
    public class ServicesService : ICrudInterface<Service>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<Service> _validator;
        public ServicesService(NotaryOfficeContext context, AbstractValidator<Service> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(Service model)
        {
            await _validator.ValidateAsync(model);
            await _context.Services.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(m => m.ServiceId == id);
        }

        public async Task<IEnumerable<Service>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Services.AsQueryable().Where(a => a.ServiceId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(Service model)
        {
            var entity = await _context.Services.FindAsync(model.ServiceId);
            await _validator.ValidateAsync(model);

            entity.Complexity = model.Complexity;
            entity.Importance = model.Importance;
            entity.ServiceName = model.ServiceName;
            

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
