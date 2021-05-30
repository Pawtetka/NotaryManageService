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
    public class ClientsService : ICrudInterface<Client>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<Client> _validator;
        public ClientsService(NotaryOfficeContext context, AbstractValidator<Client> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(Client model)
        {
            await _validator.ValidateAsync(model);
            await _context.Clients.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAllAsync(Expression<Func<Client, bool>> filter = null)
        {
            var entities = _context.Clients.AsQueryable();
            if (filter != null) entities = entities.Where(filter);

            return await entities.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(m => m.ClientId == id);
        }

        public async Task<IEnumerable<Client>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Clients.AsQueryable().Where(a => a.ClientId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(Client model)
        {
            var entity = await _context.Clients.FindAsync(model.ClientId);
            await _validator.ValidateAsync(model);

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Age = model.Age;
            entity.PassportNumber = model.PassportNumber;
            entity.PhoneNumber = model.PhoneNumber;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
