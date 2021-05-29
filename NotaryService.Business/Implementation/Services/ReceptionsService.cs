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
    public class ReceptionsService : ICrudInterface<Reception>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<Reception> _validator;
        public ReceptionsService(NotaryOfficeContext context, AbstractValidator<Reception> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(Reception model)
        {
            await _validator.ValidateAsync(model);
            await _context.Receptions.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var reception = await _context.Receptions.FindAsync(id);
            _context.Receptions.Remove(reception);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reception>> GetAllAsync()
        {
            return await _context.Receptions.Include(r => r.Client).Include(r => r.Document).Include(r => r.Notary).ToListAsync();
        }

        public async Task<Reception> GetByIdAsync(int id)
        {
            return await _context.Receptions
                .Include(r => r.Client)
                .Include(r => r.Document)
                .Include(r => r.Notary)
                .FirstOrDefaultAsync(m => m.ReceptionId == id);
        }

        public async Task<IEnumerable<Reception>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Receptions
                .Include(r => r.Client)
                .Include(r => r.Document)
                .Include(r => r.Notary)
                .AsQueryable().Where(a => a.ReceptionId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(Reception model)
        {
            var entity = await _context.Receptions.FindAsync(model.ReceptionId);
            await _validator.ValidateAsync(model);

            entity.Price = model.Price;
            entity.ReceptionDate = model.ReceptionDate;
            entity.NotaryId = model.NotaryId;
            entity.ClientId = model.ClientId;
            entity.DocumentId = model.DocumentId;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
