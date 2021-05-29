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
    public class CitiesService : ICrudInterface<City>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<City> _validator;
        public CitiesService(NotaryOfficeContext context, AbstractValidator<City> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(City model)
        {
            await _validator.ValidateAsync(model);
            await _context.Cities.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _context.Cities.FirstOrDefaultAsync(m => m.CityId == id);
        }

        public async Task<IEnumerable<City>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Cities.AsQueryable().Where(a => a.CityId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(City model)
        {
            var entity = await _context.Cities.FindAsync(model.CityId);
            await _validator.ValidateAsync(model);

            entity.CityName = model.CityName;
            entity.CityType = model.CityType;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
