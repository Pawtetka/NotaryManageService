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
    public class WorkersService : ICrudInterface<Worker>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<Worker> _validator;
        public WorkersService(NotaryOfficeContext context, AbstractValidator<Worker> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(Worker model)
        {
            await _validator.ValidateAsync(model);
            await _context.Workers.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await _context.Workers.Include(w => w.Office).ToListAsync();
        }

        public async Task<Worker> GetByIdAsync(int id)
        {
            return await _context.Workers.Include(w => w.Office).FirstOrDefaultAsync(m => m.WorkerId == id);
        }

        public async Task<IEnumerable<Worker>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Workers.Include(w => w.Office).AsQueryable().Where(a => a.WorkerId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(Worker model)
        {
            var entity = await _context.Workers.FindAsync(model.WorkerId);
            await _validator.ValidateAsync(model);

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Age = model.Age;
            entity.PassportNumber = model.PassportNumber;
            entity.PhoneNumber = model.PhoneNumber;
            entity.HireDate = model.HireDate;
            entity.OfficeId = model.OfficeId;
            entity.Salary = model.Salary;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
