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
    public class LocationsService : ICrudInterface<Location>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<Location> _validator;
        public LocationsService(NotaryOfficeContext context, AbstractValidator<Location> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(Location model)
        {
            await _validator.ValidateAsync(model);
            await _context.Locations.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Location>> GetAllAsync(Expression<Func<Location, bool>> filter = null)
        {
            var entities = _context.Locations.AsQueryable();
            if (filter != null) entities = entities.Where(filter);

            return await entities.Include(l => l.City).ToListAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await _context.Locations.Include(l => l.City).FirstOrDefaultAsync(m => m.LocationId == id);
        }

        public async Task<IEnumerable<Location>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Locations.Include(l => l.City).AsQueryable().Where(a => a.LocationId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(Location model)
        {
            var entity = await _context.Locations.FindAsync(model.LocationId);
            await _validator.ValidateAsync(model);

            entity.Address = model.Address;
            entity.CityId = model.CityId;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
