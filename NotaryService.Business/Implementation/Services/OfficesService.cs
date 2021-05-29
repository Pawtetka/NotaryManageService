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
    public class OfficesService : ICrudInterface<Office>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<Office> _validator;
        public OfficesService(NotaryOfficeContext context, AbstractValidator<Office> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(Office model)
        {
            await _validator.ValidateAsync(model);
            await _context.Offices.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var office = await _context.Offices.FindAsync(id);
            _context.Offices.Remove(office);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Office>> GetAllAsync()
        {
            return await _context.Offices.Include(o => o.Location).ToListAsync();
        }

        public async Task<Office> GetByIdAsync(int id)
        {
            return await _context.Offices.Include(o => o.Location).FirstOrDefaultAsync(m => m.OfficeId == id);
        }

        public async Task<IEnumerable<Office>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Offices.Include(o => o.Location).AsQueryable().Where(a => a.OfficeId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(Office model)
        {
            var entity = await _context.Offices.FindAsync(model.OfficeId);
            await _validator.ValidateAsync(model);

            entity.OfficeName = model.OfficeName;
            entity.OfficeSize = model.OfficeSize;
            entity.OfficeStatus = model.OfficeStatus;
            entity.LocationId = model.LocationId;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
