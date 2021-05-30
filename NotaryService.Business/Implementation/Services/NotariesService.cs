using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NotaryDatabaseDLL.Models;
using NotaryService.Business.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Services
{
    public class NotariesService : ICrudInterface<Notary>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<Notary> _validator;
        public NotariesService(NotaryOfficeContext context, AbstractValidator<Notary> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(Notary model)
        {
            await _validator.ValidateAsync(model);
            await _context.Notaries.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var notaries = await _context.Notaries.FindAsync(id);
            _context.Notaries.Remove(notaries);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notary>> GetAllAsync(Expression<Func<Notary, bool>> filter = null)
        {
            var entities = _context.Notaries.AsQueryable();
            if (filter != null) entities = entities.Where(filter);

            return await entities.Include(n => n.Worker).ToListAsync();
        }

        public async Task<Notary> GetByIdAsync(int id)
        {
            return await _context.Notaries.Include(n => n.Worker).FirstOrDefaultAsync(m => m.NotaryId == id);
        }

        public async Task<IEnumerable<Notary>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Notaries.Include(n => n.Worker).AsQueryable().Where(a => a.NotaryId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(Notary model)
        {
            var entity = await _context.Notaries.FindAsync(model.NotaryId);
            await _validator.ValidateAsync(model);

            entity.CertificateNumber = model.CertificateNumber;
            entity.NotaryId = model.NotaryId;
            entity.WorkerId = model.WorkerId;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
